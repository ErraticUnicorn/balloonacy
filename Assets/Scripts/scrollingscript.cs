using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//This class handles parallax
public class scrollingscript : MonoBehaviour {
    //scrolling speed

    public Vector2 speed = new Vector2(2, 2);

    //moving direction
    public Vector2 direction = new Vector2(0, -1);

    //is movement applied to camera
    public bool isLinkedToCamera = false;

    //is background infinite
    public bool isLooping = false;

    //List of children with a renderer
    private List<Transform> backgroundPart;
    
	// Use this for initialization
	void Start () {
        if (isLooping)
        {
            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.renderer != null)
                {
                    backgroundPart.Add(child);
                }
            }

            //sort by position
            backgroundPart = backgroundPart.OrderBy(
                t => t.position.x
                    ).ToList();

        }
	}
	
	// Update is called once per frame
	void Update () {
	    Vector3 movement = new Vector3(
            speed.x * direction.x,
            speed.y * direction.y,
            0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if(isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        //begin looping element
        if (isLooping)
        {
            //get first child
            Transform firstChild = backgroundPart.FirstOrDefault();

            //if child is below camera then it will be recycled
            if (firstChild.position.y < Camera.main.transform.position.y)
            {
                if (firstChild.renderer.IsVisibleFrom(Camera.main) == false)
                {
                    //get last child position
                    Transform lastChild = backgroundPart.LastOrDefault();
                    Vector3 lastPosition = lastChild.transform.position;
                    Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

                    //Set pos
                    firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);

                    backgroundPart.Remove(firstChild);
                    backgroundPart.Add(firstChild);
                }
            }
        }
	}
}
