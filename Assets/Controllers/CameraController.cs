using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//This class handles parallax
public class CameraController : MonoBehaviour {
    public Vector2 speed = new Vector2(2, 2);
	public Vector2 direction = new Vector2(0, -1);
	public bool isLinkedToCamera = false;
	public bool isLooping = false;
	private List<Transform> backgroundPart;
	private GameObject Cam;
	private GameObject Player;
	

	void Start () {
        if (isLooping)
        {
			Cam = GameObject.Find("Main Camera");
			Player = GameObject.Find("player");

            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                if (child.renderer != null) {
                    backgroundPart.Add(child);
                }
            }
        }
	}

	void Update () {

		Vector3 movement = new Vector3( speed.x * direction.x, speed.y * direction.y, 0);
        movement *= Time.deltaTime;
        transform.Translate(movement);

        if(isLinkedToCamera) {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping) {
			
			Cam.transform.position = new Vector3(Cam.transform.position.x,Player.transform.position.y,Cam.transform.position.z) ;
			backgroundPart = backgroundPart.OrderBy( t => t.position.y ).ToList();
			Transform firstChild = backgroundPart.FirstOrDefault();
			Transform secondChild = backgroundPart.LastOrDefault();
			Vector3 lastPosition = secondChild.transform.position;
			Vector3 lastSize = secondChild.renderer.bounds.max - secondChild.renderer.bounds.min ;
            
            if (!firstChild.renderer.IsVisibleFrom(Camera.main)) {
				firstChild.position = new Vector3(firstChild.position.x, lastPosition.y + lastSize.y, firstChild.position.z);
				backgroundPart.Remove(firstChild);
				backgroundPart.Add(firstChild);
					
			} 

			if (!secondChild.renderer.IsVisibleFrom(Camera.main)) {
					secondChild.position = new Vector3(secondChild.position.x, firstChild.position.y - lastSize.y, secondChild.position.z);
					backgroundPart.Remove(secondChild);
					backgroundPart.Add(secondChild);
			}
        }
	}
}
