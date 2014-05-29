using UnityEngine;
using System.Collections;

public class balloonGetter : MonoBehaviour {

    public int balloonPool = 25;
    public Transform redballoon;
    private int lastBalloon = -1;
    private Transform[] balloons;
    

	// Use this for initialization
	void Awake () {
        balloons = new Transform[balloonPool];
        for(int i = 0; i < balloons.Length; i++){
            balloons[i] = Instantiate(redballoon) as Transform;
            balloons[i].transform.parent = transform.parent;
        }
	}

    public Transform getNextBalloon()
    {
        lastBalloon++;
        if (lastBalloon > balloonPool - 1)
        {
            lastBalloon = 0;
        }
        return balloons[lastBalloon];
    }
}
