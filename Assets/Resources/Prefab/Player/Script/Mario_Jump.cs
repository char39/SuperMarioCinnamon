using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario_Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    private float jumpForce;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpForce = 10f;
    }

    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
