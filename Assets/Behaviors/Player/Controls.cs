using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        this.keyboardControls();
        this.iOSControls();
        this.mouseControls();
	}

    void mouseControls()
    {
        var screenPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && this.GetComponent<Jump>().isGrounded()) { this.GetComponent<Jump>().playerJump(); }
        if (screenPoint.origin.x < transform.position.x) { this.GetComponent<Jump>().xAxisMvmtLeft(); }
        if (screenPoint.origin.x > transform.position.x) { this.GetComponent<Jump>().xAxisMvmtRight(); }
    }

    void iOSControls()
    {
        foreach (Touch touch in Input.touches)
        {
            var screenPoint = Camera.main.ScreenPointToRay(touch.position);
            if (touch.phase == TouchPhase.Began) { this.GetComponent<Jump>().playerJump(); }
            if (screenPoint.origin.x < transform.position.x) { this.GetComponent<Jump>().xAxisMvmtLeft(); }
            if (screenPoint.origin.x > transform.position.x) { this.GetComponent<Jump>().xAxisMvmtRight(); }
        }

        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        //dir.y = -Input.acceleration.y;

        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }

        dir *= Time.deltaTime;
        transform.position += dir * this.GetComponent<Jump>().playerSpeed;
    }

    void keyboardControls()
    {
        if (Input.GetButtonDown("Jump")) { this.GetComponent<Jump>().playerJump(); }
        if (Input.GetKey("left")) { this.GetComponent<Jump>().xAxisMvmtLeft(); }
        if (Input.GetKey("right")) { this.GetComponent<Jump>().xAxisMvmtRight(); }
    }
}
