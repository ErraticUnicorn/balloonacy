using UnityEngine;
using System.Collections;

//when player moves up beyond the screen the camera keeps up and moves up with him. Still needs work

public class movecameraupscript : MonoBehaviour {

    private GameObject Camera;
    private GameObject Player;
	// Use this for initialization

    bool check = false;
	void Start () {
        Camera = GameObject.Find("Main Camera");
        Player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
        var posX = Camera.transform.position.x;
        var posY = Camera.transform.position.y + 7;
        this.transform.position = new Vector3(
            posX, posY, transform.position.z
            );
        
	}

    void OnTriggerEnter2D(Collider2D otherCollision)
    {

        if (otherCollision.CompareTag("Player"))
        {
            if (!check)
            {
                Camera.transform.position += new Vector3(0, 2, 0);
                //check = true;
            }
            

        }
    }

    void OnTriggerExit2D(Collider2D otherCollision)
    {
        check = false;
    }
}
