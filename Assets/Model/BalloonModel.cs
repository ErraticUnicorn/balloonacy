using UnityEngine;
using System.Collections;

 public class balloon : MonoBehaviour {

    public Vector2 speed;
    public Vector2 direction;
    public Vector2 movement;
    public float deflateRate;
    public float accel;
    public int floatingConst;
    public bool isVisible = false;
    public bool hasBecomeVisible = false;

    protected Vector3 originalScale;
    protected float originalDeflateRate;

    protected GameObject Player;

    void Start()
    {
        originalScale = new Vector3(1.2f, 1.2f, 1);
        Player = GameObject.Find("Player");
        originalDeflateRate = deflateRate;
    }

    protected void Update()
    {
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y / floatingConst);
        this.deflate();
        speed += new Vector2(0, .01f);
        this.transform.Translate(Vector3.up * accel * Time.deltaTime);

        if (!isVisible && this.transform.localScale.x < .5 && !hasBecomeVisible)
        {
            Invoke("Destroy", .1f);
        }

        //var dist = (Player.transform.position - Camera.main.transform.position).z;
        var dist = 0;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist)).y;
        if (this.transform.position.y > topBorder)
        {
            Invoke("Destroy", 1f);
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

    public void OnBecameVisible()
    {
        isVisible = true;
        hasBecomeVisible = true;
    }

    public void OnBecameInvisible()
    {
        Invoke("Destroy", 0f);
        isVisible = false;
    }

    public void resetScale()
    {
        this.transform.localScale = originalScale;
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
            Invoke("Destroy", 1f);
        }

    }

    public float getDeflateRate()
    {
        return deflateRate;
    }

    public void setDeflateRate(float newrate)
    {
        deflateRate = newrate;
    }

    public void onCollisionEnter2D(Collision2D point)
    {
        var contact = point.contacts[0];
        if (contact.point.y < this.transform.position.y)
        {
            Invoke("Destroy", 1f);
        }
    }
}
