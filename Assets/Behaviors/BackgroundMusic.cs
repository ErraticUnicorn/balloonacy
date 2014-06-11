using UnityEngine;
using System.Collections;

public class BackgroundMusic : MonoBehaviour {
    
    public AudioClip backgroundTunes;

    void Awake()
    {
        if (!audio.isPlaying)
        {
            audio.clip = backgroundTunes;
            audio.Play();
        }
    }
}
