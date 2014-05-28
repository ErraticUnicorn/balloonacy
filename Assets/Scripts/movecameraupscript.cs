using UnityEngine;
using System.Collections;

//when player moves up beyond the screen the camera keeps up and moves up with him. 

public class movecameraupscript : MonoBehaviour {

    private GameObject Camera;
    private GameObject Player;
	// Use this for initialization

    //scrolling speed
    public Vector2 speed = new Vector2(2, 2);
    //moving direction
    public Vector2 direction = new Vector2(0, -1);

    bool check = false;
	void Start () {
        Camera = GameObject.Find("Main Camera");
        Player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
      
        if (check == true) {
            float speed = 10;
            var step = speed * Time.deltaTime;
            Vector3 newPos = new Vector3(
                Camera.transform.position.x,
                Player.transform.position.y,
                Camera.transform.position.z
                );
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, newPos, step);
        }
        if (Camera.transform.position.y + 3 >= Player.transform.position.y) {
            check = false;
        }

        Vector3 screenPos = camera.WorldToScreenPoint(Player.transform.position);
        if (screenPos.y >= 915) {
            check = true;
        }
	}

    void OnTriggerEnter2D(Collider2D otherCollision) {
        
    }

    void OnTriggerExit2D(Collider2D otherCollision) {
        check = true; 
    }
}
