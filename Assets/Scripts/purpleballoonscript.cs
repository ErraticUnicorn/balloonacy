using UnityEngine;
using System.Collections;

public class purpleballoonscript : MonoBehaviour {
    //speed and movement direction
    public Vector2 speed = new Vector2(0, .5f);

    public Vector2 direction = new Vector2(0, 1);

    public Vector2 movement;


    //deflationrate
    public float deflaterate = .003f;
    public float accel = 2f;

    public int floatingconst = 4;

    public int spawnconst = 11;

    //camera
    private GameObject camera;
    private GameObject player;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("player");
        Vector3 curCamPos = camera.transform.position;
        curCamPos.x = player.transform.position.x;
        curCamPos.y -= spawnconst;
        curCamPos.z = 10;
        this.transform.position = curCamPos;
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
