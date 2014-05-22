using UnityEngine;
using System.Collections;

public class greenballoon : MonoBehaviour
{
    //speed and movement direction
    public Vector2 speed = new Vector2(0, 4);

    public Vector2 direction = new Vector2(0, 1);

    public Vector2 movement;


    //deflationrate
    public float deflaterate = .008f;
    public float accel = 8;

    public int floatingconst = 4;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y / floatingconst);
        deflate();
        speed += new Vector2(
            0,
            .01f);

        this.transform.Translate(Vector3.up * accel * Time.deltaTime);
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
    }

    void reset()
    {
        Vector3 originalscale = new Vector3(1, 1, 1);
        transform.localScale = originalscale;

    }

    void deflate()
    {
        float xcheck = transform.localScale.x;
        if (xcheck >= 0)
        {
            Vector3 reduce = new Vector3(deflaterate, deflaterate, 0);
            transform.localScale -= reduce;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public float getDeflateRate()
    {
        return deflaterate;
    }

    public void setDeflateRate(float newrate)
    {
        deflaterate = newrate;
    }
}
