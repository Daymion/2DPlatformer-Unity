using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;

    // Left and right movement variables
    public float moveSpeed;
    private int moveDirection = 0;
    private bool moving = false;

    // Jump variables
    public int jumpForce;
    private bool onGround = true;
    private bool isJump = false;

    // Wall jump variables
    public float wallJumpSpeed;
    private bool onWall = false;
    private bool isWallJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MovementAction();
        JumpAction();
        WallJumpAction();
    }

    void Update()
    {
        MovementInput();
        JumpInput();
        WallJumpInput();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Reset the jump variables
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
            isJump = false;
            isWallJump = false;
        }
        // Change the wall jump variables
        if (other.gameObject.tag == "Wall")
        {
            onWall = true;
            isWallJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Reset the wall jump variables
        if (other.gameObject.tag == "Wall")
        {
            onWall = false;
        }
    }

    /**
     * Get the direction of the player movement and transition between moving and not
     */
    private void MovementInput()
    {
        if (Input.GetKey(KeyCode.RightArrow) && isWallJump == false)
        {
            moveDirection = 1;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && isWallJump == false)
        {
            moveDirection = -1;
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    /**
     * Change the velocity of the player based on direction and whether they are moving or not
     * Lock the players movement when wall jumping
     */
    private void MovementAction()
    {        
        if (isWallJump == true)
        {
            rb.velocity = new Vector3(wallJumpSpeed * -moveDirection, rb.velocity.y);
        }
        else if (moving == true && isWallJump == false)
        {
            rb.velocity = new Vector3(moveSpeed * moveDirection, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
        }
    }

    /**
     * Get the key input for the jump
     */
    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround == true && isJump == false)
        {
            isJump = true;
        }
    }

    /**
     * Apply the jump force to the player
     */
    private void JumpAction()
    {
        if (onGround == true && isJump == true)
        {
            rb.AddForce(new Vector3(0, jumpForce));
            onGround = false;
        }
    }

    /**
     * Get the input for the wall jump
     * The player must be moving into the wall
     */
    private void WallJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround == false && isJump == true && onWall == true && isWallJump == false && moving == true)
        {
            isWallJump = true;
        }
    }

    /**
     * Apply the jump force to the player and change the velocity
     */
    private void WallJumpAction()
    {
        if (onGround == false && isJump == true && onWall == true && isWallJump == true)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0, jumpForce));
            onWall = false;
        }
    }
}
