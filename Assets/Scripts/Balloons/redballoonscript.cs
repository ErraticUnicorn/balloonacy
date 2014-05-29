using UnityEngine;
using System.Collections;

public class redballoonscript : MonoBehaviour {
    
	public Vector2 speed = new Vector2(0, 2.5f);
	public Vector2 direction = new Vector2(0, 1);
	public Vector2 movement;
	public float deflaterate = .001f;
    public float accel = 4.5f;
	public int floatingconst = 4;
    public bool isVisible = false;
    public bool hasBecomeVisible = false;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = this.transform.localScale;
    }
	
	void Update () {
        movement = new Vector2( speed.x * direction.x, speed.y * direction.y / floatingconst);
        this.deflate();
        speed += new Vector2( 0, .01f);
        this.transform.Translate(Vector3.up * accel * Time.deltaTime);

        if (!isVisible && transform.localScale.x < .5 && !hasBecomeVisible)
        {
            Invoke("Destroy", 1f);
        }
	}

    void FixedUpdate() {
        rigidbody2D.velocity = movement;
    }

    void reset() {
        Vector3 originalscale = new Vector3(1, 1, 1);
        transform.localScale = originalscale;
    }

    void deflate() {
        float xcheck = transform.localScale.x;
        if (xcheck >= 0) {
            Vector3 reduce = new Vector3(deflaterate, deflaterate, 0);
            transform.localScale -= reduce;
        } else {
            Invoke("Destroy", 1f);
        }

    }

    public float getDeflateRate() {
        return deflaterate;
    }

    public void setDeflateRate(float newrate) {

		deflaterate = newrate;
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

    public void resetScale(){
        this.transform.localScale = originalScale;
    }

    void Destroy()
    {
        this.gameObject.SetActive(false);
        resetScale();
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
