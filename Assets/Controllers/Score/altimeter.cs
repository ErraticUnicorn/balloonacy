using UnityEngine;
using System.Collections;

//gets height of player
public class altimeter : MonoBehaviour {
    
    private GameObject Player;
    scoremanager scorer;
    public int height;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		Player = GameObject.Find("player");
		scorer = GameObject.Find("Scorer").GetComponent<scoremanager>();
		if (Player) {
			height = (int)(Player.transform.position.y - this.transform.position.y) + 100;
			scorer.setScore (height + 1);
		}
	}

    public int getHeight()
    {
        return height;
    }
}
