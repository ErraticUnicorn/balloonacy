Some basic info on Unity 2D:

Default scripts come with the Start and Update methods. Here is a short list of the most used "Message" functions:

Awake() is called once when the object is created. See it as replacement of a classic constructor method.
Start() is executed after Awake(). The difference is that the Start() method is not called if the script is not enabled (remember the checkbox on a component in the "Inspector").
Update() is executed for each frame in the main game loop.
FixedUpdate() is called at every fixed framerate frame. You should use this method over Update() when dealing with physics ("RigidBody" and forces).
Destroy() is invoked when the object is destroyed. It's your last chance to clean or execute some code.
You also have some functions for the collisions :

OnCollisionEnter2D(CollisionInfo2D info) is invoked when another collider is touching this object collider.
OnCollisionExit2D(CollisionInfo2D info) is invoked when another collider is not touching this object collider anymore.
OnTriggerEnter2D(Collider2D otherCollider) is invoked when another collider marked as a "Trigger" is touching this object collider.
OnTriggerExit2D(Collider2D otherCollider) is invoked when another collider marked as a "Trigger" is not touching this object collider anymore.
 

Check out the mindmap to get a sense of what we are going for:
http://www.mindmeister.com/maps/show/416890882
