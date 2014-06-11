using UnityEngine;
using System.Collections;

public class CollisionController : MonoBehaviour {

    public float newDeflateRate = .006f;
    private bool lose = false;
    private GameObject cam;
    private AchievementController scorer;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        scorer = GameObject.Find("Scorer").GetComponent<AchievementController>();
        DontDestroyOnLoad(this);

    }

    void Update()
    {
        this.checkForLoss();
    }

    bool check = true;
    void FixedUpdate()
    {
        checkIfOutOfBounds();
        checkIfOnBalloon();
    }

    Notification collision = new Notification(NotificationType.OnBalloonPlayerCollision, "Balloon Collided!");
    void checkIfOnBalloon()
    {
        float playerSize = this.renderer.bounds.size.y;
        Vector3 position1 = transform.position;
        Vector3 position2 = transform.position;
        position1.x = position1.x - playerSize;
        position1.y = position1.y;
        position2.x = position2.x + playerSize;
        position2.y = position2.y - 2 * playerSize;

        Collider2D[] hits = Physics2D.OverlapAreaAll(new Vector2(position1.x, position1.y), new Vector2(position2.x, position2.y));

        int i = 0;
        bool temp = false;
        while (i < hits.Length)
        {
            Collider2D hit = hits[i];
            if (hit != null)
            {
                if (hit.tag == "platform")
                {
                    NotificationCenter.defaultCenter.postNotification(collision);
                    Float balloonFloat = hit.gameObject.GetComponent<Float>();
                    Deflate balloonDeflate = hit.gameObject.GetComponent<Deflate>();
                    BalloonAppearance balloonApp = hit.gameObject.GetComponent<BalloonAppearance>();
                    if (!balloonApp.isGreen)
                    {
                        balloonFloat.setSpeed(new Vector2(0, 20f));
                    }
                    else
                    {
                        balloonFloat.setSpeed(new Vector2(0, 25f));
                        scorer.setMessageVisible();
                    }
                    temp = true;
                }
            }
            i++;
        }
        this.GetComponent<Jump>().setGrounded(check);
    }

    void checkIfOutOfBounds()
    {
        if (transform.position.x >= 10.01) { transform.position = new Vector3(10.01f, transform.position.y, transform.position.z); }
        if (transform.position.x < -10) { transform.position = new Vector3(-10, transform.position.y, transform.position.z); }

        if (rigidbody2D.velocity.y < -3 && check)
        {
            rigidbody2D.velocity -= new Vector2(rigidbody2D.velocity.x, .1f);
        }
        if (rigidbody2D.velocity.y < -30)
        {
            rigidbody.velocity = new Vector2(rigidbody2D.velocity.x, -30);
            check = false;
        }
    }

    void checkForLoss()
    {
        if (transform.position.y <= cam.transform.position.y - 10)
        {
            Application.LoadLevel("losescreen");
        }
    }
}
