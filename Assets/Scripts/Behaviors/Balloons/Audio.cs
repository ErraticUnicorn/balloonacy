using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
    
    public AudioClip pop;
    protected bool soundPlayed = false;
    
    public void Sound() {
        if (!soundPlayed) {
            audio.PlayOneShot(pop);
        }
         soundPlayed = true;
    }

    public void Reset() {
        soundPlayed = false;
    }
}
