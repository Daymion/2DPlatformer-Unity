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
    private bool airMoving = false;

    // Jump variables
    public int jumpForce;
    private bool onGround = true;
    private bool isJump = false;

    // Wall jump variables
    public float wallJumpForce;
    private float wallJumpDirection = 0;
    private bool isWallJump = false;
    private RaycastHit2D rightSide;
    private float distanceRight;
    private RaycastHit2D leftSide;
    private float distanceLeft;
    private bool onWall = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        WallJumpRays();
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
        // Reset the jump and wall jump booleans
        if (other.gameObject.tag == "Ground")
        {
            onGround = true;
            isJump = false;
            isWallJump = false;
            airMoving = false;
            onWall = false;
        }

        if (other.gameObject.tag == "Wall")
        {
            onWall = true;
        }

        if (other.gameObject.tag == "InvisibleWall")
        {
            onWall = false;
        }
    }

    /**
     * Get the direction of the player movement and transition between moving and not
     */
    private void MovementInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection = 1;
            moving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
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
     * Change the velocity of the player based on direction
     * Stop the velocity of the player on the ground
     * Stop the velocity of the player in the air based on circumstances
     */
    private void MovementAction()
    {        
        if (moving == true)
        {
            rb.velocity = new Vector3(moveSpeed * moveDirection, rb.velocity.y);
        }
        else if (moving == false && isWallJump == false && isJump == false)
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
        }
        else if (moving == false && isJump == true && airMoving == true)
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
            airMoving = true;
        }
    }

    /**
     * Get the input for the wall jump
     */
    private void WallJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround == false && isJump == true)
        {
            if (distanceRight < 0.52 && onWall == true || distanceLeft < 0.52 && onWall == true)
            {
                isWallJump = true;
                onWall = false;
            }
        }
    }

    /**
     * Apply the wall jump force to the player and change the velocity
     */
    private void WallJumpAction()
    {
        if (onGround == false && isJump == true && isWallJump == true)
        {
            airMoving = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(wallJumpForce * wallJumpDirection, jumpForce));
            isWallJump = false;
        }
    }

    /**
     * Check the distance between the player and the wall using rays
     * Change the direction of the wall jump
     */
    private void WallJumpRays()
    {
        rightSide = Physics2D.Raycast(transform.position, Vector2.right);
        if (rightSide.collider != null)
        {
            distanceRight = Mathf.Abs(rightSide.point.x - transform.position.x);
            if (distanceRight < 0.52)
            {
                wallJumpDirection = -1;
            }
        }
        leftSide = Physics2D.Raycast(transform.position, Vector2.left);
        if (leftSide.collider != null)
        {
            distanceLeft = Mathf.Abs(leftSide.point.x - transform.position.x);
            if (distanceLeft < 0.52)
            {
                wallJumpDirection = 1;
            }
        }
    }
}
