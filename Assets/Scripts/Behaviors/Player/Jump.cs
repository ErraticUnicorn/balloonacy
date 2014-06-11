using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

    public float playerSpeed = 5;
    private bool playerGrounded = false;
    private int playerJumpHeight = 500;


    public bool isGrounded()
    {
        return playerGrounded;
    }

    public void setGrounded(bool ground)
    {
        playerGrounded = ground;
    }
    public void playerJump()
    {
        if (playerGrounded)
        {
            rigidbody2D.AddForce(new Vector2(0, playerJumpHeight));
        }
    }

    public void xAxisMvmtRight()
    {
        transform.position += Vector3.right * playerSpeed * Time.deltaTime;
    }

    public void xAxisMvmtLeft()
    {
        transform.position -= Vector3.right * playerSpeed * Time.deltaTime;
    }
}
