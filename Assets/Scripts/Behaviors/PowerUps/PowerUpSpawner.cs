using UnityEngine;
using System.Collections;
//arraylist & lists
using System.Collections.Generic;

public class PowerUpSpawner : MonoBehaviour {

    public GameObject powerUp;
    public int powerUpPool = 5;
    public int timer;
    public int spawntime = 25;
    private List<Vector2> PowerUpCoord;
    private GameObject camera;
    private GameObject player;
    private int lastPowerUp = -1;
    private GameObject[] powerUps;
    private float topBorder;
    private int curPowerup = 0;

	// Use this for initialization
	void Start () {
        timer = 30;
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("player");
	}

    void Awake() {
        powerUps = new GameObject[powerUpPool];
        for (int i = 0; i < powerUps.Length; i++) {
            powerUps[i] = Instantiate(powerUp) as GameObject;
            powerUps[i].SetActive(false);
            powerUps[i].transform.parent = transform.parent;
        }
    }

    public GameObject getNextPowerUp() {
        lastPowerUp++;
        if (lastPowerUp > powerUpPool - 1) {
            lastPowerUp = 0;
        }
        return powerUps[lastPowerUp];
    }
	
	// Update is called once per frame
	void Update () {
      
        var dist = (player.transform.position - Camera.main.transform.position).z;
        topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        transform.position = new Vector3(camera.transform.position.x, topBorder + 5, dist);
        //spawnPowerUps();
	}


    void spawnPowerUps() {
        GameObject powerUp;
        powerUp = getNextPowerUp();
        powerUp.SetActive(true);
    }
}
