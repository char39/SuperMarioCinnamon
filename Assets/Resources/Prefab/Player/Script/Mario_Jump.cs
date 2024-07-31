using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarioCtrl;

public class Mario_Jump : MonoBehaviour
{
    internal static void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            Mario.rb.velocity = new Vector2(Mario.rb.velocity.x, Mario.jumpForce);
    }
}
