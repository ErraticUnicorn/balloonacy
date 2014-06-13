using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

    public Vector2 speed;
    public Vector2 direction;
    public int floatingConst = 4;
    protected Vector2 movement;
    public float accel;


    public Vector2 getSpeed() {
        return speed;
    }

    public float getAcceleration() {
        return accel;
    }

    public Vector2 getDirection() {
        return direction;
    }

    public void setSpeed(Vector2 newSpeed) {
        this.speed = newSpeed;
    }

    public void setAcceleration(float accel) {
        Debug.Log(this.accel);
        this.accel = accel;
    }

    public void setDirection(Vector2 newDirection) {
        this.direction = newDirection;
    }

	// Use this for initialization
	void Start () {
        floatingConst = 4;
	}
	
	// Update is called once per frame
	void Update () {
        handleMovement();
	}

    protected void FixedUpdate() {
        rigidbody2D.velocity = movement;
    }

    protected void handleMovement() {
        movement = new Vector2(speed.x * direction.x, speed.y * direction.y / floatingConst);
        speed += new Vector2(0, .01f);
        this.transform.Translate(Vector3.up * accel * Time.deltaTime);
        var dist = 0;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        bool check = this.GetComponent<BalloonAppearance>().hasBecomeVisible;
        if (this.transform.position.y > topBorder || (this.transform.position.y < bottomBorder - 50 && !check)) {
            this.GetComponent<BalloonAppearance>().Destroy();
        }
    }
}
