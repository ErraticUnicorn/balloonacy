using UnityEngine;
using System.Collections;

public class balloonGetter : MonoBehaviour {

    public int balloonPool = 50;
    public GameObject redballoon;
    private int lastBalloon = -1;
    private GameObject[] balloons;
    

	// Use this for initialization
	void Awake () {
        balloons = new GameObject[balloonPool];
        for(int i = 0; i < balloons.Length; i++) {
            balloons[i] = Instantiate(redballoon) as GameObject;
            balloons[i].SetActive(false);
            balloons[i].transform.parent = transform.parent;
        }
	}

    public GameObject getNextBalloon() {
        lastBalloon++;
        if (lastBalloon > balloonPool - 1) {
            lastBalloon = 0;
        }
        if (balloons[lastBalloon].activeInHierarchy) {
            lastBalloon = findInactiveBalloon();
        }

        return balloons[lastBalloon];
    }

    public int findInactiveBalloon() {
        for (int i = 0; i < balloons.Length; i++) {
            if (!balloons[i].activeInHierarchy) {
                return i;
            }
        }

        return 0;
    }
}
