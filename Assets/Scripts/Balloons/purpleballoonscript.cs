using UnityEngine;
using System.Collections;

public class purpleballoonscript : balloon {
  
    public int spawnconst = 11;

    //camera
    private GameObject camera;
    private GameObject player;

	// Use this for initialization
	void Start () {
        speed = new Vector2(0, .5f);
        direction = new Vector2(0, 1);
        deflateRate = .003f;
        accel = 2f;
        floatingConst = 4;


        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("player");
        Vector3 curCamPos = camera.transform.position;
        curCamPos.x = player.transform.position.x;
        curCamPos.y -= spawnconst;
        curCamPos.z = 10;
        this.transform.position = curCamPos;
	}
}
