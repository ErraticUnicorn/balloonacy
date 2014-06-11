using UnityEngine;
using System.Collections;

public class GreenBalloon : MonoBehaviour {

    public Sprite greenSprite;

    public GameObject greenBalloonPresets(GameObject balloon) {
        BalloonAppearance BalloonMod = balloon.GetComponent<BalloonAppearance>();
        Float BalloonFloat = balloon.GetComponent<Float>();
        Deflate BalloonDeflate = balloon.GetComponent<Deflate>();

        BalloonMod.isGreen = true;
        BalloonMod.curSprite = greenSprite;
        BalloonFloat.speed = new Vector2(0, 30f);
        BalloonFloat.direction = new Vector2(0, 1);
        BalloonFloat.accel = 2f;
        BalloonDeflate.deflateRate = .002f;
        BalloonFloat.accel = 2f;
        BalloonMod.isGreen = true;
        balloon.transform.parent = this.transform.parent;

        return balloon;
    }
}
