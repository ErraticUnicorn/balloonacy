using UnityEngine;
using System.Collections;

public class MainMenuClouds : MonoBehaviour {

    public Vector2 speed;
    Vector2 direction = new Vector2(1, 0);
    bool check = true;
    protected GameObject background;

	// Use this for initialization
	void Start () {
	    background = GameObject.Find("Background");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * -direction.y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        if(!this.renderer.IsVisibleFrom(Camera.main) && check){
            Vector3 curPos = this.transform.position;
            curPos.x = background.transform.position.x - 10;
            this.transform.position = curPos;
            check = false;
        }
	}
}
