﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//This class handles parallax
public class CameraController : MonoBehaviour {


    public Vector2 speed = new Vector2(2, 2);
	public Vector2 direction = new Vector2(0, -1);
	public bool isLinkedToCamera = false;
	public bool isLooping = false;
    public bool isParallaxing = false;
    public List<Sprite> backgrounds;
    private int expectedFrame = 0;
    private int currentFrame = 0;
	private List<Transform> backgroundPart;
	private GameObject Cam;
	private GameObject Player;
    private Vector3 lastCameraPos;

	private int loopCount;

    private void LoadImages(List<Sprite> backgrounds) {
        for (int i = 1; i < 09; i++) {
            string texture = "Assets/Resources/Textures/backgrounds/0" + i +".png";
            Sprite texTmp = (Sprite)Resources.LoadAssetAtPath(texture, typeof(Sprite));
            backgrounds.Add(texTmp);
        }
    }

	void Start() {

        Cam = GameObject.Find("Main Camera");
        Player = GameObject.Find("player");

        if (isLooping) {
			this.loopCount = 0;

            backgroundPart = new List<Transform>();
            backgrounds = new List<Sprite>();
            LoadImages(backgrounds);

            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                child.GetComponent<SpriteRenderer>().sprite = backgrounds[0];
                if (child.renderer != null) {
                    backgroundPart.Add(child);
                }
            }
        }
	}

	void Update () {
        Parallax();
        looping();
	}

    void looping() {
        if (isLooping) {
            if (this.loopCount >= -1) {
                Cam.transform.position = new Vector3(Cam.transform.position.x, Player.transform.position.y, Cam.transform.position.z);
            }

            //needs work -- cleaner code

            backgroundPart = backgroundPart.OrderBy(t => t.position.y).ToList();
            Transform firstChild = backgroundPart.FirstOrDefault();
            Transform secondChild = backgroundPart.LastOrDefault();
            Vector3 lastPosition = secondChild.transform.position;
            Vector3 lastSize = secondChild.renderer.bounds.max - secondChild.renderer.bounds.min;

            checkIfFirstSceneIsOutOfBounds(firstChild, lastPosition, lastSize);
            checkIfSecondSceneIsOutOfBounds(secondChild, firstChild, lastPosition, lastSize);

        }

        lastCameraPos = Cam.transform.position;
    }

    void checkIfFirstSceneIsOutOfBounds(Transform firstChild, Vector3 lastPosition, Vector3 lastSize) {
        if (!firstChild.renderer.IsVisibleFrom(Camera.main) && isCameraRising()) {
            firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);
            backgroundPart.Remove(firstChild);
            backgroundPart.Add(firstChild);
            //add a trigger?
            this.loopCount++;
            this.expectedFrame++;
            if (expectedFrame == currentFrame) {
                expectedFrame++;
            }
            currentFrame = expectedFrame - 1;
            backgroundCheck();
            firstChild.GetComponent<SpriteRenderer>().sprite = backgrounds[expectedFrame];
        }
    }

    void checkIfSecondSceneIsOutOfBounds(Transform secondChild, Transform firstChild, Vector3 lastPosition, Vector3 lastSize) {
        if (!secondChild.renderer.IsVisibleFrom(Camera.main) && !isCameraRising())
        {
            this.loopCount--;
            this.expectedFrame--;
            if (expectedFrame == currentFrame)
            {
                expectedFrame--;
            }
            currentFrame = expectedFrame + 1;
            backgroundCheck();
            secondChild.GetComponent<SpriteRenderer>().sprite = backgrounds[expectedFrame];
            if (this.loopCount >= -1)
            {
                secondChild.position = new Vector3(secondChild.position.x, firstChild.position.y - lastSize.y, secondChild.position.z);
                backgroundPart.Remove(secondChild);
                backgroundPart.Add(secondChild);
            }
        }
    }

    bool isCameraRising() {
        return lastCameraPos.y < Cam.transform.position.y;
    }

    void backgroundCheck() {
        if (expectedFrame <= 0) {
            expectedFrame = 0;
        }
        if (expectedFrame >= backgrounds.Count) {
            expectedFrame = backgrounds.Count;
        }
    }

    void Parallax() {
        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * -direction.y, 0);
        if (Player.rigidbody2D.velocity.y < 0) {
            movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
        }
        if (Player.rigidbody2D.velocity.y >= 0) {
            //direction.y = -direction.y;
        }

        if (isParallaxing)
        {
            movement += new Vector3(movement.x, Player.rigidbody2D.velocity.y, 0);
        }
        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera) {
            Camera.main.transform.Translate(movement);
        }
    }
}

