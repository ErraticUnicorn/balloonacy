using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {

        Notification powerUp = new Notification(NotificationType.OnPowerUp, "Powerup");

        if (other.gameObject.tag == "Player") {
            //send Notification
            NotificationCenter.defaultCenter.postNotification(powerUp);
            Destroy(this.gameObject);
        }
    }
}
