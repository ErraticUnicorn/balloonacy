using UnityEngine;
using System.Collections;

public class GUI_Button : MonoBehaviour {

    //purple balloon
    public Transform purpleballoon;
    public int purpcount = 1;

    private GameObject Player;
    private scoremanager scorer;

    void Start() {
        Player = GameObject.Find("player");
        scorer = GameObject.Find("Scorer").GetComponent<scoremanager>();
    }
    //GUI specific actions
    void OnGUI() {

        if (GUI.Button(new Rect(0, 0, 100, 50), "Safety Balloon Count: " + purpcount)) //for adding a texture just replace the string witht he image
        {
            spawnPurpleBalloon();
        }
    }

    public void setPurpcount(int count) {
        purpcount = count;
    }

    void spawnPurpleBalloon() {
        if (purpcount > 0) {
            Transform balloon;
            balloon = Instantiate(purpleballoon) as Transform;
            Vector3 pos = balloon.transform.position;
            pos.x = Player.transform.position.x;
            balloon.position = pos;
            balloon.transform.parent = transform.parent;
            purpcount--;
        }
    }

    void controlPurpCount() {
        if (scorer.getScore() % 500 == 0) {
            purpcount++;
        }
    }
}
