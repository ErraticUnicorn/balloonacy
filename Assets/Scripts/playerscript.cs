using UnityEngine;
using System.Collections;

public class playerscript : MonoBehaviour {

	public Vector2 movement;
    public float movementSpeed = 5;
	public Vector3 startpos = new Vector3(0, 0, 0);
	public float newdeflaterate = .006f;
	private int jumpHeight = 500;
    private bool isGrounded = false;
	private bool losing = false;
	private GameObject CameraCheck;
    private scoremanager scorer;

	void Start () {
        CameraCheck = GameObject.Find("Main Camera");
        scorer = GameObject.Find("Scorer").GetComponent<scoremanager>();
        DontDestroyOnLoad(this);
	}
	
	void Update () {
		this.keyboardControls();
		//this.iOSControls();
	    //this.mouseControls();
		this.checkBounds();
		this.checkForDidLose();
        Debug.Log(isGrounded);
	}

	void FixedUpdate() {
		RaycastHit2D[] hits;
		Vector3 vect = transform.position;
		vect.y = vect.y - 1f;
		hits = Physics2D.RaycastAll(vect, Vector2.zero);
		int i = 0;
		bool temp = false;
		while (i < hits.Length) {
			RaycastHit2D hit = hits[i];
			if (hit.collider != null) {
				if(hit.collider.tag == "redplatform"){
					var curballoon = hit.collider.gameObject.GetComponent<redballoonscript>();
				    curballoon.setDeflateRate(newdeflaterate);
					temp = true;
				}
				if(hit.collider.tag == "greenplatform"){
					var curballoon = hit.collider.gameObject.GetComponent<greenballoon>();
					curballoon.setDeflateRate(newdeflaterate);
					temp = true;
				}
			}  
			i++;
		}
		isGrounded = temp;
	}

	void mouseControls() {
		var screenPoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && isGrounded) {
            playerJump();
        }

		if (Input.GetAxis("Mouse X") <= -1) {
	         xAxisMvmtLeft();
		}

		if (screenPoint.origin.x < transform.position.x && !isGrounded) {
            xAxisMvmtLeft();
        }

		if (screenPoint.origin.x > transform.position.x && !isGrounded) {
            xAxisMvmtRight();
        }
    }

	void iOSControls() {

        foreach (Touch touch in Input.touches) {
            
			if (touch.phase == TouchPhase.Began) {
                playerJump();
            }

			var screenPoint = Camera.main.ScreenPointToRay(touch.position);

			if (screenPoint.origin.x < transform.position.x && !isGrounded) {
                xAxisMvmtLeft();
            }

			if (screenPoint.origin.x > transform.position.x && !isGrounded) {
                xAxisMvmtRight();
            }
        }

        Vector3 dir = Vector3.zero;

        dir.x = Input.acceleration.x;
        //dir.y = -Input.acceleration.y;

        if (dir.sqrMagnitude > 1){
            dir.Normalize();
        }

        dir *= Time.deltaTime;
        transform.position += dir * movementSpeed;
    }
	
	void keyboardControls(){
		if (Input.GetKey("left")){
			xAxisMvmtLeft();
		}
		
		if (Input.GetKey("right")){
			xAxisMvmtRight();
		}
		
		if (Input.GetButtonDown("Jump")){
			playerJump();
			Debug.Log("*****");
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			Debug.Log( sr.sprite.bounds.center.normalized );
			Debug.Log("*****");
		}
		
	}

    void playerJump() {
		if (isGrounded) {
			rigidbody2D.AddForce(new Vector2(0, jumpHeight));
		}
    }

	void xAxisMvmtRight() {
		transform.position += Vector3.right * movementSpeed * Time.deltaTime;
	}
	
	void xAxisMvmtLeft() {
		transform.position -= Vector3.right * movementSpeed * Time.deltaTime;
	}
	
	void checkBounds() {
		/*
		var dist = (transform.position - Camera.main.transform.position).z;
		var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
		var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
		var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 2, dist)).y;
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
			Mathf.Clamp(transform.position.y, transform.position.y, topBorder),
			transform.position.z);
		*/
	}
	
	bool checkIfOutOfBounds() {
		return (transform.position.y <= CameraCheck.transform.position.y - 10 || transform.position.x >= 10 || transform.position.x <= -10);
	}
	
	void checkForDidLose() {
		if (checkIfOutOfBounds()){
            Application.LoadLevel("losescreen");
        }

    }

/*
	void OnCollisionEnter2D(Collision2D info) {
        if (rigidbody2D.velocity.magnitude <= 3  ) {
            isGrounded = true;
        }
        //increase deflaterate of balloon once player steps on it

       if (info.gameObject.tag == "redplatform") {
            var curballoon = info.gameObject.GetComponent<redballoonscript>();
            curballoon.setDeflateRate(newdeflaterate);
        }

        if (info.gameObject.tag == "greenplatform") {
            var curballoon = info.gameObject.GetComponent<greenballoon>();
            curballoon.setDeflateRate(newdeflaterate);
            scorer.newScoreRate(10);
        }
    }
*/    

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

//gives functionality to 2D rigidbodies to have different types of forces
/*
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
    */

