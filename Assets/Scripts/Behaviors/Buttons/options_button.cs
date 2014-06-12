using UnityEngine;
using System.Collections;

public class options_button : MonoBehaviour {

    public Texture2D optionsimage;
    public GUISkin GUIskin = null;

    void OnGUI() {
        GUI.skin = GUIskin;
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 250, optionsimage.width + 100, optionsimage.height + 100), optionsimage)) {

        }
    }
}
