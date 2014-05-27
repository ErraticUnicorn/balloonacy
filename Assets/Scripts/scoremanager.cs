using UnityEngine;
using System.Collections;

public class scoremanager : MonoBehaviour {

    private static scoremanager scoreManager = null;

    private int score = 0;

    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public static scoremanager getInstance()
    {
        if (scoreManager == null)
        {
            scoreManager = new scoremanager();
        }

        return scoreManager;
    }

    public int getScore()
    {
        return score;
    }

    public int setScore(int score)
    {
        this.score = score;
        return score;
    }
}
