using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarioCtrl;

public class Mario_Fall : MonoBehaviour
{
    internal static void MaxFallSpeed()
    {
        Mario.fallSpeed = Mario.rb.velocity.y;
        if (Mario.rb.velocity.y < Mario.maxFallSpeed)
            Mario.rb.velocity = new Vector2(Mario.rb.velocity.x, Mario.maxFallSpeed);
    }
}
