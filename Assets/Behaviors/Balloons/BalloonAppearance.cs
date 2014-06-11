using UnityEngine;
using System.Collections;

public class BalloonAppearance : MonoBehaviour {

    public bool isVisible = false;
    public bool hasBecomeVisible = false;
    public Sprite curSprite;
    public bool isGreen = false;

    protected Vector3 originalScale;
    protected GameObject Player;

    private BalloonSpawner spawner;


    public void setSprite(Sprite image) {
        this.GetComponent<SpriteRenderer>().sprite = image;
    }

	// Use this for initialization
	void Start () {
        originalScale = this.transform.localScale;
        this.GetComponent<SpriteRenderer>().sprite = curSprite;
        Player = GameObject.Find("Player");
        spawner = GameObject.Find("spawner").GetComponent<BalloonSpawner>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Destroy()
    {
        spawner.totalBalloons--;
        this.GetComponent<Audio>().Reset();
        this.transform.localScale = originalScale;
        this.gameObject.SetActive(false);
    }

    protected void OnDisable() {
        CancelInvoke();
    }

    public void OnBecameInvisible() {
        //Invoke("Destroy", 0f);

        isVisible = false;
    }

    public void OnBecameVisible() {
        isVisible = true;
    }

    public void OnCollisionEnter2D(Collision2D other) {
        var contact = other.contacts[0];
        if (contact.point.y < this.transform.position.y - .5f && other.gameObject.tag == "Player") {
            Invoke("Destroy", 0f);
        }
        if (other.gameObject.tag == "platform" && !isVisible) {
            Invoke("Destroy", 0f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "platform" && !isVisible && !isGreen) {
            Invoke("Destroy", 0f);
        }
    }
}
