using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario_FallSpeed : MonoBehaviour
{
    private Rigidbody2D rb;
    private float maxFallSpeed; // 종단속도

    [Header("Variation Value")] // 수치 바꿔도 전~~~혀 관계없는 변수
    public float fallSpeed;     // 현재 떨어지는 속도를 보여줌

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxFallSpeed = -20f;
    }

    void Update()
    {
        MaxFallSpeed();
    }

    private void MaxFallSpeed()
    {
        fallSpeed = rb.velocity.y;
        if (rb.velocity.y < maxFallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
    }
}
