/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarioCtrl;

public class Mario_Move : MonoBehaviour
{
    internal static void FixedUpdate_Move(ref Rigidbody2D rb, float maxMoveSpeed)
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }

        rb.velocity = new Vector2(moveInput * maxMoveSpeed, rb.velocity.y);
    }
    internal static void VelocityLimit(ref Rigidbody2D rb, float maxMoveSpeed)
    {
        if (rb.velocity.x > maxMoveSpeed)
            rb.velocity = new Vector2(maxMoveSpeed, rb.velocity.y);
        else if (rb.velocity.x < -maxMoveSpeed)
            rb.velocity = new Vector2(-maxMoveSpeed, rb.velocity.y);
    }
}
 */