using UnityEngine;
using System.Collections;

//arraylist & lists
using System.Collections.Generic;

public class redballoonspawnscript : MonoBehaviour {

    //balloon prefabs
    public Transform redballoon;
    public Transform greenballoon;


    //keeps track of total balloons spawned
    public int totalBalloons;

    //timer to buffer when balloons spawn and the timing between balloons
    public int timer;
    public int spawntime = 25;

    //check for ArrayList -- see Balloon spawning method
    private int curBalloon = 0;
    //Array with the coordinates of next five balloons
    private List<Vector2> BalloonCoord;

    //camera
    private GameObject camera;

    //allows manipulation of difficulty via score
    private scoretext scorer;

    //distance from camera spawner is set at
    public int distance = 13;

 
    //first trigger to increase difficulty
    public int threshold1 = 250;



	// Use this for initialization, inits camera, score, timer, and total balloons
	void Start () {
        totalBalloons = 0;
        timer = 0;
        camera = GameObject.Find("Main Camera");
        scorer = GameObject.Find("Score").GetComponent<scoretext>();
            
	}
	
	// Update is called once per frame
	void Update () {

        SpawnBalloons();
        //keeps spawner near camera (rubber banded?)
        transform.position = (transform.position - camera.transform.position).normalized * distance + camera.transform.position;
	}

    /// <summary>
    /// Uses array list of four balloons to spawn balloons based on a timer. curBalloon is an index checking where in the array we are at
    /// the boolean reverse, reverses the spawning order of the balloons to add some more complexity
    /// </summary>

    //how do I avoid this global variable? C# doesn't allow static variables
    bool reverse = true;
    void SpawnBalloons()
    {
        
        if (curBalloon >= 4)
        {
            curBalloon = 0;
            reverse = !reverse;
        }
        if (curBalloon == 0)
        {
            BalloonCoord = getBalloonPoints();
        }
        if (timer == spawntime && reverse ==true)
        {
            spawnBalloon(BalloonCoord[curBalloon]);
            curBalloon++;
        }
        if (timer == spawntime && reverse == false)
        {
            spawnBalloon(BalloonCoord[BalloonCoord.Capacity - curBalloon - 1]);
            curBalloon++;
        }
        if (timer >= spawntime)
        {
            timer = 0;
        }

        timer++;

    }

    /// <summary>
    /// spawns a balloon based on a coordinate and other variables such as score and randomizers to spawn different 
    /// types of balloons. My first instinct, and the easier and more crazy way is just to randomize positions on balloons each 
    /// run through
    /// </summary>
    /// <param name="coordinate"> Takes in a coordinate to spawn the balloon at said location</param>

    void spawnBalloon(Vector2 coordinate){

        //(once score is 250?) 75% chance of spawning red balloon, 25% chance of spawning green balloon
        int score = scorer.getScore();
        Transform balloon;

        //cases
        var balloonrandomizer = Random.Range(0, 100);

        //new point with an offset behind the spawner
        Vector3 curPos = new Vector3(
        this.transform.position.x + coordinate.x,
        this.transform.position.y + coordinate.y - 5,
        0
        );

        if (balloonrandomizer > 75 && score < threshold1)
        {
            balloon = Instantiate(greenballoon) as Transform;
            balloon.transform.parent = transform.parent;
            balloon.position = curPos;
            totalBalloons++;
        }

        if (balloonrandomizer >= 50 && score >= threshold1)
        {
            balloon = Instantiate(greenballoon) as Transform;
            balloon.transform.parent = transform.parent;
            balloon.position = curPos;
            totalBalloons++;
        }
        else
        {
            balloon = Instantiate(redballoon) as Transform;
            balloon.transform.parent = transform.parent;
            balloon.position = curPos;
            totalBalloons++;
        }
        
    }

    /// <summary>
    /// Subdivides the area of the spawner into four equals parts. Then randomly places balloons in these five parts
    /// This is in order to later implement smarter algorithms to decide how the balloons should be placed, and allows us to track
    /// previous balloons coordinates and can tie in to the players movements as well.
    /// </summary>
    /// <returns> List of 4 coordinates representing the next 4 balloon locations</returns>

    public List<Vector2> getBalloonPoints()
    {
        List<Vector2> BalloonPoints = new List<Vector2>();
        List<Vector2> Sections = new List<Vector2>();


        int spawnwidth = 20;
        int spawnheight = 4;
        int offset = -10;
        float curX = 0, curY = 0;
        for (int i = 0; i < 4; i++)
        {
            int section = spawnwidth / 4;
            Sections.Add(new Vector2(section + offset, spawnheight));
            offset += 5;
        }
        for (int i = 0; i < Sections.Capacity; i++)
        {
            Vector2 curRange = Sections[i];

            curX = Random.Range(curRange.x - 5, curRange.x);
            curY = Random.Range(curRange.y - 4, curRange.y);
            BalloonPoints.Add(new Vector2(curX, curY));
        }

        return BalloonPoints;
    }
}
