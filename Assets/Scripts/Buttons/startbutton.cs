using UnityEngine;
using System.Collections;

public class startbutton : MonoBehaviour {

    void OnGUI()
    {
        Debug.Log("Hi!");
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 100, 50), "Click to Begin!")) //for adding a texture just replace the string witht he image
        {
            Application.LoadLevel("stage1");
        }
    }
}
