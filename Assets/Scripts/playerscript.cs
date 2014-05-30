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
	private GameObject cameraObject;
   
	void Start () {
        cameraObject = GameObject.Find("Main Camera");
        DontDestroyOnLoad(this);
	}
	
	void Update () {
		this.keyboardControls();
		//this.iOSControls();
		this.mouseControls();
		this.checkForDidLose();
	}

	void FixedUpdate() {
		RaycastHit2D[] hits;
		Vector3 vect = transform.position;
		vect.y = vect.y - 0.5f;
		Debug.Log (vect);

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

	bool checkIfOutOfBounds() {
		return (transform.position.y <= cameraObject.transform.position.y - 10 || transform.position.x >= 10 || transform.position.x <= -10);
	}
	
	void checkForDidLose() {
		if (transform.position.y <= cameraObject.transform.position.y - 10){
            Application.LoadLevel("losescreen");
        }

    }

}