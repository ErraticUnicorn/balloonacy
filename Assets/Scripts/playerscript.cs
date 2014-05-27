using UnityEngine;
using System.Collections;

public class playerscript : MonoBehaviour {


    public Vector2 movement;
    //controls movementspeed
    public float movementSpeed = 5;

    //jump controls
    private int jumpHeight = 500;
    private bool isGrounded = false;

    //starting position
    public Vector3 startpos = new Vector3(0, 0, 0);

    //losing
    private bool losing = false;

    private GameObject CameraCheck;

	// Use this for initialization
	void Start () {
        CameraCheck = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame 
	void Update () {
        //section deals with input
        //Keyboard input helper function
        KeyboardControls();

        //MouseControls
        //MouseControls();

        //iOS
        iOSControls();

        //Bounday Checks
        CheckBounds();
      
        //checks if lost
        Lose();
	}

    void FixedUpdate()
    {
        
    }
    //handles keyboard input
    void KeyboardControls()
    {
        //allows left and right movement only when jumping
        if (Input.GetKey("left") && !isGrounded)
        {
            XAxisMvmtLeft();
        }

        if (Input.GetKey("right") && !isGrounded)
        {
            XAxisMvmtRight();
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

    }

    void MouseControls()
    {
        if (Input.GetMouseButtonDown(0))//&& !isGrounded)
        {
            Jump();
        }

        if (Input.GetAxis("Mouse X") <= -1)
        {
            XAxisMvmtLeft();
        }

        //ray for the purpose of converting a click on screen to points in the game world
        var coordRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (coordRay.origin.x < transform.position.x && !isGrounded)
        {
            XAxisMvmtLeft();

        }

        if (coordRay.origin.x > transform.position.x && !isGrounded)
        {
            XAxisMvmtRight();
        }
    }

    void iOSControls()
    {
        foreach (Touch touch in Input.touches){
            if (touch.phase == TouchPhase.Began)
            {
                Jump();
            }

            //ray for the purpose of converting a touch on screen to points in the game world
            var coordRay = Camera.main.ScreenPointToRay(touch.position);
            if (coordRay.origin.x < transform.position.x && !isGrounded)
            {
                XAxisMvmtLeft();

            }

            if (coordRay.origin.x > transform.position.x && !isGrounded)
            {
                XAxisMvmtRight();
            }
        }

        Vector3 dir = Vector3.zero;

        dir.x = -Input.acceleration.y;
        dir.y = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }

        dir *= Time.deltaTime;
        transform.position += dir * movementSpeed;
    }

    void Jump()
    {
        if (!isGrounded)
        {
            return;
        }
        isGrounded = false;
        rigidbody2D.AddForce(new Vector2(0, jumpHeight));

    }

    void XAxisMvmtRight()
    {
        transform.position += Vector3.right * movementSpeed * Time.deltaTime;

    }
    void XAxisMvmtLeft()
    {
        transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
    }


    void CheckBounds()
    {
        //make sure the player doesn't leave the upper y, and two x values
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;

        //needs work
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 2, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, transform.position.y, topBorder),
            transform.position.z

            );
    }
    //lose state
    void Lose()
    {
        //lose state
        if (checkIfOutOfBounds())
        {
            losing = true;
        }

        if (losing)
        {
            Application.LoadLevel("losescreen");
        }

    }


    //checks to see if player has landed on balloon as well as triggering events for balloons
    void OnCollisionEnter2D(Collision2D info)
    {
        if (rigidbody2D.velocity.magnitude <= 3  )
        {
            isGrounded = true;
        }
        //increase deflaterate of balloon once player steps on it
        if (info.gameObject.tag == "redplatform")
        {
            
            var curballoon = info.gameObject.GetComponent<redballoonscript>();
            curballoon.setDeflateRate(.006f);
        }

        
    }

    // Maybe allow balloons to deflate slower once you jump off?
    /*
    void OnCollisionExit2D(Collision2D info)
    {
        if (info.gameObject.tag == "redplatform")
        {

            var curballoon = info.gameObject.GetComponent<redballoonscript>();
            curballoon.setDeflateRate(.002f);
        }

    }
    */

    //checks if player has fallen out of bounds.
    bool checkIfOutOfBounds()
    {
        if (transform.position.y <= CameraCheck.transform.position.y - 10 || transform.position.x >= 10 || transform.position.x <= -10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //gives functionality to 2D rigidbodies to have different types of forces
    Vector2 ApplyForceMode(Vector2 force, ForceMode forceMode)
    {
        switch (forceMode)
        {
            case ForceMode.Force:
                return force;
            case ForceMode.Impulse:
                return force / Time.fixedDeltaTime;
            case ForceMode.Acceleration:
                return force * rigidbody2D.mass;
            case ForceMode.VelocityChange:
                return force * rigidbody2D.mass / Time.fixedDeltaTime;

        }

        return force;
    }
}

