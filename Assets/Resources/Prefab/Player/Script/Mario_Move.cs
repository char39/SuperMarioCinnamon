using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Mario_Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private float maxMoveSpeed;
    private float moveForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;                // 중력 Off
        maxMoveSpeed = 10f;
        moveForce = 50f;
    }

    void FixedUpdate()
    {
        Move();
        VelocityLimit();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector2(-moveForce, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector2(moveForce, 0));
        }
    }
    private void VelocityLimit()
    {
        if (rb.velocity.x > maxMoveSpeed)
            rb.velocity = new Vector2(maxMoveSpeed, rb.velocity.y);
        else if (rb.velocity.x < -maxMoveSpeed)
            rb.velocity = new Vector2(-maxMoveSpeed, rb.velocity.y);
    }
}
