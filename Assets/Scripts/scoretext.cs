using UnityEngine;
using System.Collections;

public class scoretext : MonoBehaviour {

    private altimeter altimeter;
    public int score;

	// Use this for initialization
	void Start () {
        altimeter = GameObject.Find("Floor").GetComponent<altimeter>();

	}
	
	// Update is called once per frame
	void Update () {
        score = altimeter.getScore();
        this.guiText.text = "Score: " + score;
	
	}

    public int getScore()
    {
        score = altimeter.getScore();
        return score;
    }
}
