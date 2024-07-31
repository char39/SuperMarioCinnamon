using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarioCtrl;

public class Mario_Move : MonoBehaviour
{
    internal static void Move()
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

        Mario.rb.velocity = new Vector2(moveInput * Mario.maxMoveSpeed, Mario.rb.velocity.y);
    }
    internal static void VelocityLimit()
    {
        if (Mario.rb.velocity.x > Mario.maxMoveSpeed)
            Mario.rb.velocity = new Vector2(Mario.maxMoveSpeed, Mario.rb.velocity.y);
        else if (Mario.rb.velocity.x < -Mario.maxMoveSpeed)
            Mario.rb.velocity = new Vector2(-Mario.maxMoveSpeed, Mario.rb.velocity.y);
    }
}
