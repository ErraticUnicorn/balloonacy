using UnityEngine;
using System.Collections;

public class scoretext : MonoBehaviour {

    private scoremanager scorer;
    public static int score;

	// Use this for initialization
	void Start () {
        scorer = GameObject.Find("Scorer").GetComponent<scoremanager>();

	}
	
	// Update is called once per frame
	void Update () {
        score = scorer.getScore();
        this.guiText.text = "Score: " + score;
	
	}

    public int getScore()
    {
        return score;
    }


}
