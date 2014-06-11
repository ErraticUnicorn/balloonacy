using UnityEngine;
using System.Collections;

public class RedBalloon : MonoBehaviour {
    
    public Sprite redSprite;

	public GameObject redBalloonPresets(GameObject balloon){
        
        BalloonAppearance BalloonMod = balloon.GetComponent<BalloonAppearance>();
        Float BalloonFloat = balloon.GetComponent<Float>();
        Deflate BalloonDeflate = balloon.GetComponent<Deflate>();

        BalloonMod.curSprite = redSprite;
        BalloonFloat.speed = new Vector2(0, 25f);
        BalloonFloat.direction = new Vector2(0, 1);
        BalloonFloat.accel = 1f;
        BalloonDeflate.deflateRate = .001f;
        balloon.transform.parent = this.transform.parent;

        return balloon;
    }
}
