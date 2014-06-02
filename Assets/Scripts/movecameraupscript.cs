using UnityEngine;
using System.Collections;

//when player moves up beyond the screen the camera keeps up and moves up with him. 

public class movecameraupscript : MonoBehaviour {

    private GameObject Cam;
    private GameObject Player;
	// Use this for initialization

    //scrolling speed
    public Vector2 speed = new Vector2(2, 2);
    //moving direction
    public Vector2 direction = new Vector2(0, -1);

    bool check = false;
	void Start () {
        Cam = GameObject.Find("Main Camera");
        Player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
      
        if (check == true) {
            float speed = 10;
            var step = speed * Time.deltaTime;
            Vector3 newPos = new Vector3(
                Cam.transform.position.x,
                Player.transform.position.y,
                Cam.transform.position.z
                );
            Cam.transform.position = Vector3.MoveTowards(Cam.transform.position, newPos, step);
        }
        if (Cam.transform.position.y + 3 >= Player.transform.position.y) {
            check = false;
        }

        var dist = (Player.transform.position - Camera.main.transform.position).z;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist)).y;
        if (Player.transform.position.y >= topBorder - 1)
        {
            check = true;
        }
	}

    void OnTriggerEnter2D(Collider2D otherCollision) {
        
    }

    void OnTriggerExit2D(Collider2D otherCollision) {
        check = true; 
    }
}
