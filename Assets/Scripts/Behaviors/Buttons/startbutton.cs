using UnityEngine;
using System.Collections;

public class startbutton : MonoBehaviour {

    public Texture2D startbuttonimage;
    public GUISkin GUIskin = null; 

    void OnGUI() {
        GUI.skin = GUIskin;
        if (GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 250, startbuttonimage.width +100, startbuttonimage.height +100), startbuttonimage)) {
            Application.LoadLevel("stage1");
        }
    }
}
