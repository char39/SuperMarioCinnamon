using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mario_MoveTest : MonoBehaviour
{
    public float moveSpeed = 8f;                                    // 이동 속도
    public LayerMask groundLayer;                                   // 땅 레이어
    public Vector2 groundCheckSize = new Vector2(0.7f, 0.1f);       // groundCheck의 박스 크기
    public Vector2 sideLeftCheckSize = new Vector2(0.1f, 0.9f);     // sideLeftCheck의 박스 크기
    public Vector2 sideRightCheckSize = new Vector2(0.1f, 0.9f);    // sideRightCheck의 박스 크기
    public Transform groundCheck;                                   // 땅을 체크할 위치
    public Transform sideLeftCheck;                                 // 왼쪽을 체크할 위치
    public Transform sideRightCheck;                                // 오른쪽을 체크할 위치

    [HideInInspector]               // 인스펙터 창에 노출되지 않음
    public Vector2 velocity;           // 속도 벡터를 공개 변수로 설정

    public Rigidbody2D rb;             // Rigidbody2D 컴포넌트를 저장할 변수
    public bool isGrounded;            // 땅에 닿았는지 판별할 변수
    public int isSideCheck = 0;        // 벽에 닿았는지 판별할 변수.    [왼쪽 : -1, 닿지 않음 : 0, 오른쪽 : 1]
    public float originalGravityScale;   // 원래 중력 스케일을 저장할 변수
    public bool ignoreGroundCheck = false;  // 땅 체크 무시 플래그
    //public bool ignoreSideCheck = false;    // 벽 체크 무시 플래그

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       // Rigidbody2D 컴포넌트 할당
        originalGravityScale = rb.gravityScale; // 원래 중력 스케일 저장
    }

    void FixedUpdate()
    {
        MoveLeftRight();                        // 좌우 이동
        BlockCheck();
    }

    void Update()
    {
        Jump();                                 // 점프
    }

    void BlockCheck()                               // 모든 충돌 판정 체크
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);  // 땅에 닿았는지 판별

        if (Physics2D.OverlapBox(sideLeftCheck.position, sideLeftCheckSize, 0f, groundLayer))   // 왼쪽에 닿았는지 판별
            isSideCheck = -1;
        else if (Physics2D.OverlapBox(sideRightCheck.position, sideRightCheckSize, 0f, groundLayer))  // 오른쪽에 닿았는지 판별
            isSideCheck = 1;
        else
            isSideCheck = 0;

        if (isGrounded && !ignoreGroundCheck)                                 // 땅에 닿았을 때
        {
            rb.gravityScale = 0f;                           // 중력 스케일 0으로 설정
            rb.velocity = new Vector2(rb.velocity.x, 0f);   // y축 속도 0으로 설정
            Vector3 position = transform.position;          // 현재 위치 저장
            position.y = Mathf.Round(position.y);           // y축 위치 반올림                  //x는 0.62
            transform.position = position;                  // 위치 적용
        }
        else                                            // 땅에 닿지 않았을 때
            rb.gravityScale = originalGravityScale;         // 중력 스케일 원래대로 설정
        velocity = rb.velocity;                         // 속도 벡터 저장

        if (isSideCheck == 1)
        {
            Vector3 position = transform.position;
            position.x = Mathf.Round(position.x);
            position = new Vector2(position.x - 0.38f, position.y + 0f);
            transform.position = position;
        }
        else if (isSideCheck == -1)
        {
            Vector3 position = transform.position;
            position.x = Mathf.Round(position.x);
            position = new Vector2(position.x + 0.38f, position.y + 0f);
            transform.position = position;
        }
    }

    private void MoveLeftRight()                    // 좌우 이동
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 newVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);    // 새로운 속도
        if (isSideCheck == 0 || (isSideCheck == 1 && moveX < 0) || (isSideCheck == -1 && moveX > 0))
            rb.velocity = newVelocity;                                              // 새로운 속도 적용
    }

    void OnDrawGizmos()                             // 에디터 상에서만 실행되는 함수. 속도 벡터를 그림
    {
        if (!Application.isPlaying) return;             // 플레이 중이 아닐 때는 실행하지 않음. 속도 벡터를 그림

        Gizmos.color = Color.green;                                                     // 색상 설정
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)velocity);    // 속도 벡터 그리기
    }
    void OnDrawGizmosSelected()                     // 에디터 상에서만 실행되는 함수. groundCheck를 그림
    {
        if (groundCheck == null) return;                // groundCheck가 없을 때는 실행하지 않고, 플레이 중이 아니어도 실행. groundCheck를 그림
        Gizmos.color = Color.red;                                                       // 색상 설정
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);                     // groundCheck의 박스 그리기
        if (sideLeftCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideLeftCheck.position, sideLeftCheckSize);
        if (sideRightCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideRightCheck.position, sideRightCheckSize);
    }

    void Jump()                                     // 점프
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)    // 스페이스바를 누르고 땅에 닿았을 때
        {
            ignoreGroundCheck = true;                         // 땅 체크 무시 플래그 설정
            rb.velocity = new Vector2(rb.velocity.x, 20f);    // y축 속도 설정 (점프 벡터)
            StartCoroutine(ResetGroundCheck());               // 땅 체크를 다시 활성화하는 코루틴 시작
        }
        else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)  // 스페이스바를 떼고 y축 속도가 양수일 때
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);  // y축 속도를 반으로 줄임
        }   
    }
    IEnumerator ResetGroundCheck()                  // 땅 체크를 다시 활성화하는 코루틴
    {
        yield return new WaitForSeconds(0.1f);                // 0.1초 대기
        ignoreGroundCheck = false;                            // 땅 체크 무시 플래그 해제
    }

}
