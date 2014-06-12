using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "player") {
            Debug.Log("Hi!");
        }
    }
}
