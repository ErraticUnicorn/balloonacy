using UnityEngine;
using System.Collections;

public class finalscore : MonoBehaviour {

    public scoretext scoretext;

	// Use this for initialization
	void Start () {
        scoretext = GameObject.Find("Score").GetComponent<scoretext>();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(scoretext.getScore());
	}
}
