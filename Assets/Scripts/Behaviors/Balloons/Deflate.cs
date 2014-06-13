using UnityEngine;
using System.Collections;

public class Deflate : MonoBehaviour {

    public float deflateRate;
    protected float originalDeflateRate;
    protected float collisionDeflateRate = .006f;
    public bool playerCheck = false;

    public float getDeflateRate() {
        return deflateRate;
    }

    public void setDeflateRate(float newrate) {
        this.deflateRate = newrate;
    }
	// Use this for initialization
	void Start () {
        originalDeflateRate = this.deflateRate;
        NotificationCenter.defaultCenter.addListener(playerCollision, NotificationType.OnBalloonPlayerCollision);
	}
	
	// Update is called once per frame
	void Update () {
        deflate();
	}

    public void Reset() {
        this.deflateRate = originalDeflateRate;
        playerCheck = false;
    }

    protected void deflate() {
        float xcheck = transform.localScale.x;
        if (xcheck >= 0) {
            Vector3 reduce = new Vector3(deflateRate, deflateRate, 0);
            transform.localScale -= reduce;
        }

        if (this.transform.localScale.y <= .2) {
            this.GetComponent<Audio>().Sound();
        }
        if(xcheck <= 0) {
            this.GetComponent<BalloonAppearance>().Destroy();
            this.deflateRate = originalDeflateRate;
        }
    }

    void playerCollision(Notification Note){
        if (playerCheck) {
            this.deflateRate = collisionDeflateRate;
            Float balloonFloat = this.gameObject.GetComponent<Float>();
            Deflate balloonDeflate = this.gameObject.GetComponent<Deflate>();
            BalloonAppearance balloonApp = this.gameObject.GetComponent<BalloonAppearance>();
            if (!balloonApp.isGreen) {
                balloonFloat.setSpeed(new Vector2(0, 20f));
            }
            else {
                Notification achievement = new Notification(NotificationType.OnAchievableEvent, "On Green Balloon!");
                balloonFloat.setSpeed(new Vector2(0, 25f));
                NotificationCenter.defaultCenter.postNotification(achievement);
            }
            NotificationCenter.defaultCenter.removeListener(playerCollision, NotificationType.OnBalloonPlayerCollision);
        }


    }
}
