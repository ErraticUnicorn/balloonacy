using UnityEngine;
using System.Collections;

 public class balloon : MonoBehaviour {

    public Vector2 speed;
    public Vector2 direction;
    public Vector2 movement;
    public float deflaterate;
    public float accel;
    public int floatingconst;
    public bool isVisible = false;
    public bool hasBecomeVisible = false;

    protected Vector3 originalScale;

    void Start()
    {
        originalScale = this.transform.localScale;
    }

    protected void Update()
    {
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y / floatingconst);
        this.deflate();
        speed += new Vector2(0, .01f);
        this.transform.Translate(Vector3.up * accel * Time.deltaTime);

        if (!isVisible && transform.localScale.x < .5 && !hasBecomeVisible)
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
        this.gameObject.SetActive(false);
        resetScale();
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

    protected void reset()
    {
        Vector3 originalscale = new Vector3(1, 1, 1);
        transform.localScale = originalscale;
    }

    protected void deflate()
    {
        float xcheck = transform.localScale.x;
        if (xcheck >= 0)
        {
            Vector3 reduce = new Vector3(deflaterate, deflaterate, 0);
            transform.localScale -= reduce;
        }
        else
        {
            Invoke("Destroy", 1f);
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
