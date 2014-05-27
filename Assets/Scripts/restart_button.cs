using UnityEngine;
using System.Collections;

public class restart_button : MonoBehaviour {

    void OnGUI()
    {

        if (GUI.Button(new Rect(Screen.width/2, Screen.height/2, 100, 50), "Restart!")) //for adding a texture just replace the string witht he image
        {
            Application.LoadLevel("stage1");
        }
    }
}
