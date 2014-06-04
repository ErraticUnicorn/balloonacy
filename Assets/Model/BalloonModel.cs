using UnityEngine;
using System.Collections;


namespace Models
{
    public class BalloonModel : MonoBehaviour
    {

        public Vector2 speed;
        public Vector2 direction;
        public float deflateRate;
        public float accel;
        public int floatingConst = 4;
        public bool isVisible = false;
        public bool hasBecomeVisible = false;
        public Sprite curSprite;
        public bool isGreen = false;

        protected Vector3 originalScale;
        protected Vector2 movement;
        protected float originalDeflateRate;
        protected GameObject Player;
        protected float speedOffset;

        

        public float getDeflateRate()
        {
            return deflateRate;
        }

        public Vector2 getSpeed()
        {
            return speed;
        }

        public float getAcceleration()
        {
            return accel;
        }

        public Vector2 getDirection()
        {
            return direction;
        }

        public void setSprite(Sprite image)
        {
            this.GetComponent<SpriteRenderer>().sprite = image;
        }

        public void setDeflateRate(float newrate)
        {
            this.deflateRate = newrate;
        }

        public void setSpeed(Vector2 newSpeed)
        {
            this.speed = newSpeed;
        }

        public void setAcceleration(float accel)
        {
            Debug.Log(this.accel);
            this.accel = accel;
        }

        public void setDirection(Vector2 newDirection)
        {
            this.direction = newDirection;
        }


        // Use this for initialization
        void Start()
        {
            floatingConst = 4;
            speedOffset = 0;
            originalScale = this.transform.localScale;
            originalDeflateRate = this.deflateRate;
            Player = GameObject.Find("Player");
            this.GetComponent<SpriteRenderer>().sprite = curSprite;
        }

        public void init(Vector2 speed, float deflaterate, float acceleration)
        {
            this.speed = speed;
            direction = new Vector2(0, 1);
            this.deflateRate = deflaterate;
            this.accel = acceleration;
            floatingConst = 4;
        }

        // Update is called once per frame
        protected void Update()
        {
            movement = new Vector2(speed.x * direction.x, speed.y * direction.y / floatingConst);
            this.deflate();
            speed += new Vector2(0, .01f);
            this.transform.Translate(Vector3.up * accel * Time.deltaTime);
            var dist = 0;
            var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist)).y;
            var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
            if (this.transform.position.y > topBorder || this.transform.position.y < bottomBorder - 10)
            {
                Invoke("Destroy", .1f);
            }
           
        }

        protected void FixedUpdate()
        {
            rigidbody2D.velocity = movement;

        }

        protected void Destroy()
        {
            this.transform.localScale = originalScale;
            this.deflateRate = originalDeflateRate;
            this.gameObject.SetActive(false);
        }

        protected void OnDisable()
        {
            CancelInvoke();
        }

        public void OnBecameInvisible()
        {
            //Invoke("Destroy", 0f);
            isVisible = false;
        }

        public void OnBecameVisible()
        {
            isVisible = true;
        }

        protected void deflate()
        {
            float xcheck = transform.localScale.x;
            if (xcheck >= 0)
            {
                Vector3 reduce = new Vector3(deflateRate, deflateRate, 0);
                transform.localScale -= reduce;
            }
            else
            {
                Invoke("Destroy", 0f);
            }

        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            var contact = other.contacts[0];
            if (contact.point.y < this.transform.position.y - .5f && other.gameObject.tag == "Player")
            {
                Invoke("Destroy", 0f);
            }
            if (other.gameObject.tag == "platform" && !isVisible)
            {
                Invoke("Destroy", 0f);
            }
            
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "platform" && !isVisible)
            {
                Invoke("Destroy", 0f);                
            }
        }
    }
}
