using UnityEngine;
using System.Collections;

public class GUI_Button : MonoBehaviour {

    //purple balloon
    public Transform purpleballoon;

    public int purpcount = 1;

    //GUI specific actions
    void OnGUI()
    {

        if (GUI.Button(new Rect(0, 0, 100, 50), "Safety Balloon!")) //for adding a texture just replace the string witht he image
        {
            spawnPurpleBalloon();
        }
    }

    public void setPurpcount(int count)
    {
        purpcount = count;
    }

    void spawnPurpleBalloon()
    {
        if (purpcount > 0)
        {
            Transform balloon;
            balloon = Instantiate(purpleballoon) as Transform;
            balloon.transform.parent = transform.parent;
            purpcount--;
        }
    }
}
