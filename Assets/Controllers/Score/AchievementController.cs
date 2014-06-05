using UnityEngine;
using System.Collections;

public class AchievementController : MonoBehaviour {

    public Texture2D image = null;

    private static AchievementController scoreManager = null;
    private int score = 0;
    private int messageTimer;
    private bool messageIsVisible;

    void Start() {
        DontDestroyOnLoad(this);
        messageTimer = 0;
    }

    void Update() {
        messageTimer++;
        if (messageTimer > 120) {
            messageTimer = 0;
            setMessageInvisible();
        }
    }

    public static AchievementController getInstance() {
        if (scoreManager == null) {
            scoreManager = new AchievementController();
        }
        return scoreManager;
    }

    void OnGUI() {
        if (messageIsVisible) {
            GUI.backgroundColor = new Color(0, 0, 0, 0);
            GUI.Button(new Rect(Screen.width / 2, Screen.height / 2, 200, 200), image);
        }
    }

    public int getScore() {
        return score;
    }

    public int setScore(int score) {
        this.score = score;
        return score;
    }

    public void newScoreRate(int score) {
        this.score += score;
    }

    public void setMessageVisible() {
        messageIsVisible = true;
    }

    public void setMessageInvisible() {
        messageIsVisible = false;
    }
}

/* 
 *     void OnCollisionEnter2D(Collision2D info) {
        if (info.gameObject.tag == "Player") {
            messageIsVisible = true;
        }
    }
*/
