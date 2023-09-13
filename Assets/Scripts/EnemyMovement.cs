using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Enemy movement type variables
    public int movementType = 1;

    // Left and right movement variables
    public float moveSpeed;
    private int moveDirection = -1;

    // Enemy jump variables
    public float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (movementType == 2)
        {
            InvokeRepeating("EnemyJump", 5.0f, 5.0f);
        }   
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveSpeed * moveDirection, rb.velocity.y);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Change the move direction when the enemy hits a wall
        if (other.gameObject.tag == "Wall")
        {
            moveDirection *= -1;
        }
    }

    private void EnemyJump()
    {
        rb.AddForce(new Vector3(0, jumpForce));
    }
}
