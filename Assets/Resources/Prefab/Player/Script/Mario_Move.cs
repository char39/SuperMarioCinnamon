using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario_Move : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);
    public Transform groundCheck;

    [HideInInspector]
    public Vector2 velocity; // 속도 벡터를 공개 변수로 설정

    private Rigidbody2D rb;
    private bool isGrounded;
    private float originalGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale; // 원래 중력 스케일 저장
    }

    void FixedUpdate()
    {
        // 좌우 입력을 감지
        float moveX = Input.GetAxisRaw("Horizontal");

        // 새로운 속도 설정
        Vector2 newVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // 속도를 업데이트
        rb.velocity = newVelocity;

        // 바닥에 닿았는지 확인
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);

        // 바닥에 닿았을 때와 떠났을 때 중력 스케일 및 속도를 조정
        if (isGrounded)
        {
            rb.gravityScale = 0f;

            // y 속도를 0으로 설정하여 이동 멈추기
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            // y좌표를 정수로 고정
            Vector3 position = transform.position;
            position.y = Mathf.Round(position.y);
            transform.position = position;
        }
        else
        {
            rb.gravityScale = originalGravityScale;
        }

        // 현재 속도를 공개 변수에 저장
        velocity = rb.velocity;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}