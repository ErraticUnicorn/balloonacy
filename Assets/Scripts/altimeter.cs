using UnityEngine;
using System.Collections;

//gets height of player
public class altimeter : MonoBehaviour {
    
    private GameObject Player;
    public int Score;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
        Score = (int)(Player.transform.position.y - this.transform.position.y) + 100;
	}

    public int getScore()
    {
        return Score;
    }
}
