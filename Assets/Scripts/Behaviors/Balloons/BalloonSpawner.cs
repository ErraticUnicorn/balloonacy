using UnityEngine;
using System.Collections;

//arraylist & lists
using System.Collections.Generic;

public class BalloonSpawner : MonoBehaviour {

    public GameObject balloon;

    public int balloonPool = 50;
    private int lastBalloon = -1;
    private GameObject[] balloons;
    public int totalBalloons;
    public int timer;
    public int spawntime = 25;
    private int curBalloon = 0;
    private List<Vector2> BalloonCoord;
    private List<GameObject> SpawningBalloons;
    private GameObject camera;
    private GameObject player;
    private scoretext scorer;
    public int distance = 10;
    private float bottomBorder;

    void Start() {
        totalBalloons = 4;
        timer = 0;
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("player");
        scorer = GameObject.Find("Score").GetComponent<scoretext>();
        SpawningBalloons = new List<GameObject>();
        NotificationCenter.defaultCenter.addListener(onReceiveNotificationBalloonPop, NotificationType.BalloonPop);

    }

    void Awake() {
        balloons = new GameObject[balloonPool];
        for (int i = 0; i < balloons.Length; i++) {
            balloons[i] = Instantiate(balloon) as GameObject;
            balloons[i].SetActive(false);
            balloons[i].transform.parent = transform.parent;
        }
    }

    public GameObject getNextBalloon() {
        lastBalloon++;
        if (lastBalloon > balloonPool - 1) {
            lastBalloon = 0;
        }
        return balloons[lastBalloon];
    }

    void Update() {

        SpawnBalloons();
        var dist = (player.transform.position - Camera.main.transform.position).z;
        bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        transform.position = new Vector3(camera.transform.position.x, bottomBorder, dist);
    }

    bool reverse = true;

    void SpawnBalloons() {

        if (curBalloon >= 4) {
            curBalloon = 0;
            reverse = !reverse;
        }
        if (curBalloon == 0) {
            BalloonCoord = getBalloonPoints();
        }
        if (timer == spawntime && reverse == true) {
            spawnBalloon(BalloonCoord[curBalloon]);
            curBalloon++;
        }
        if (timer == spawntime && reverse == false) {
            spawnBalloon(BalloonCoord[BalloonCoord.Capacity - curBalloon - 1]);
            curBalloon++;
        }
        if (timer >= spawntime) {
            timer = 0;
        }
        timer++;
    }

    void spawnBalloon(Vector2 coordinate) {

        int score = scorer.getScore();
        GameObject balloon;
        int greenThreshold = 75;
        if (score >= 250) {
            greenThreshold = 50;
        }

        var balloonrandomizer = Random.Range(0, 100);

        Vector3 curPos = new Vector3(
        this.transform.position.x + coordinate.x,
        this.transform.position.y + coordinate.y - 5,
        0
        );
        if (balloonrandomizer > greenThreshold) {
            balloon = spawnGreenBalloon(curPos);
        }
        else {
            balloon = spawnRedBalloon(curPos);
        }

    }

    /// <summary>
    /// Subdivides the area of the spawner into four equals parts. Then randomly places balloons in these five parts
    /// This is in order to later implement smarter algorithms to decide how the balloons should be placed, and allows us to track
    /// previous balloons coordinates and can tie in to the players movements as well.
    /// </summary>
    /// <returns> List of 4 coordinates representing the next 4 balloon locations</returns>

    public List<Vector2> getBalloonPoints() {
        List<Vector2> BalloonPoints = new List<Vector2>();
        List<Vector2> Sections = new List<Vector2>();


        int spawnwidth = 20;
        int spawnheight = 4;
        int offset = -10;
        float curX = 0, curY = 0;
        for (int i = 0; i < 4; i++) {
            int section = spawnwidth / 4;
            Sections.Add(new Vector2(section + offset, spawnheight));
            offset += 5;
        }
        for (int i = 0; i < Sections.Capacity; i++) {
            Vector2 curRange = Sections[i];

            curX = Random.Range(curRange.x - 5, curRange.x);
            curY = Random.Range(curRange.y - 4, curRange.y);
            BalloonPoints.Add(new Vector2(curX, curY));
        }

        return BalloonPoints;
    }

    public GameObject spawnGreenBalloon(Vector3 curPos) {
        GameObject balloon;
        balloon = getNextBalloon();
        balloon = this.GetComponent<GreenBalloon>().greenBalloonPresets(balloon);
        balloon.transform.position = curPos;
        SpawningBalloons.Add(balloon);
        balloon.SetActive(true);
        totalBalloons++;
        return balloon;
    }

    public GameObject spawnRedBalloon(Vector3 curPos) {
        GameObject balloon;
        balloon = getNextBalloon();
        balloon = this.GetComponent<RedBalloon>().redBalloonPresets(balloon);
        balloon.transform.position = curPos;
        SpawningBalloons.Add(balloon);
        balloon.SetActive(true);
        totalBalloons++;
        return balloon;
    }

    void onReceiveNotificationBalloonPop(Notification note) {
        totalBalloons--;
    }

    
}

