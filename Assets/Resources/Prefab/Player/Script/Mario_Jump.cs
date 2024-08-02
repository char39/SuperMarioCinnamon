/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarioCtrl;

public class Mario_Jump : MonoBehaviour
{
    internal static bool Update_Jump(ref Rigidbody2D rb, ref bool isJump, ref bool isGroundTouch,float jumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isJump && isGroundTouch)
        {
            rb.velocity = new Vector2(0, jumpForce);
            isJump = true;
        }
        return isJump;
    }
}
 */