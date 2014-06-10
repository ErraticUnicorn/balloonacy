using UnityEngine;
using System.Collections;

public class startbutton : MonoBehaviour {

    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 50), "Click to Begin!")) {
            Application.LoadLevel("stage1");
        }
    }
}
