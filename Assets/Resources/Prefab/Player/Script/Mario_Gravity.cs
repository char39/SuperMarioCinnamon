/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarioCtrl;

public class Mario_Gravity : MonoBehaviour
{
    private const float gravity = -9.81f;
    private const string staticGround = "StaticGround";

    internal static void Start_MarioFall(ref Rigidbody2D rb)           // 중력을 velocity로 사용
    {
        rb.gravityScale = 0.0f;
        rb.velocity = Vector2.zero;
    }

    internal static void Update_MaxFallSpeed(ref Rigidbody2D rb, out float fallSpeed, float maxFallSpeed)             // 종단속도
    {
        fallSpeed = rb.velocity.y;  // 인스펙터용

        if (rb.velocity.y < maxFallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
    }

    internal static bool FixedUpdate_Gravity(ref Rigidbody2D rb, float gravityMultiValue, ref bool isGroundTouch, ref bool isGroundTouchOff, ref bool isJump)
    {
        if (!isGroundTouch)
        {
            rb.velocity += new Vector2(0, gravity * Time.fixedDeltaTime * gravityMultiValue);
        }
        else if (!isJump && !isGroundTouchOff)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            isGroundTouchOff = true;
        }
        return isGroundTouchOff;
    }

    internal static bool GroundTouchEnter(ref bool isGroundTouch, ref bool isJump)
    {
        isGroundTouch = true;
        isJump = false;
        return isGroundTouch;
    }

    internal static bool GroundTouchExit(ref bool isGroundTouch)
    {
        isGroundTouch = false;
        return isGroundTouch;
    }
}
 */