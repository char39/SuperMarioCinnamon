using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /// <summary> 오브젝트의 실시간 벡터를 표현함. </summary>
    /// <param name="tr"></param>
    /// <param name="velocity"></param>
    public static void DrawVectorGizmos(Transform tr, Vector2 velocity)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(tr.position, tr.position + (Vector3)velocity);
    }
    /// <summary> 블록 판별을 위한 박스 크기를 표현함. </summary>
    /// <param name="blockCheck"></param>
    /// <param name="blockCheckSize"></param>
    /// <param name="blockLeftCheck"></param>
    /// <param name="blockLeftCheckSize"></param>
    /// <param name="blockRightCheck"></param>
    /// <param name="blockRightCheckSize"></param>
    public static void DrawCollisionGizmos(Transform blockCheck, Vector2 blockCheckSize, Transform blockLeftCheck, Vector2 blockLeftCheckSize, Transform blockRightCheck, Vector2 blockRightCheckSize)
    {
        if (blockCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(blockCheck.position, blockCheckSize);
        if (blockLeftCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(blockLeftCheck.position, blockLeftCheckSize);
        if (blockRightCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(blockRightCheck.position, blockRightCheckSize);
    }
    /// <summary> 바닥에 닿았는지 판별함. </summary>
    /// <param name="blockCheck"></param>
    /// <param name="blockCheckSize"></param>
    /// <param name="blockLayer"></param>
    /// <returns></returns>
    public static bool BlockCheck(Transform blockCheck, Vector2 blockCheckSize, LayerMask blockLayer)
    {
        return Physics2D.OverlapBox(blockCheck.position, blockCheckSize, 0f, blockLayer);               // 바닥에 닿았는지 판별
    }
    /// <summary> 왼쪽이나 오른쪽에 닿았는지 판별함. </summary>
    /// <param name="blockLeftCheck"></param>
    /// <param name="blockRightCheck"></param>
    /// <param name="blockLeftCheckSize"></param>
    /// <param name="blockRightCheckSize"></param>
    /// <param name="blockLayer"></param>
    /// <param name="SideBlockCheck"></param>
    /// <returns></returns>
    public static bool SideCheck(Transform blockLeftCheck, Transform blockRightCheck, Vector2 blockLeftCheckSize, Vector2 blockRightCheckSize, LayerMask blockLayer, out int SideBlockCheck)
    {
        if (Physics2D.OverlapBox(blockLeftCheck.position, blockLeftCheckSize, 0f, blockLayer))          // 왼쪽에 닿았는지 판별
            SideBlockCheck = -1;
        else if (Physics2D.OverlapBox(blockRightCheck.position, blockRightCheckSize, 0f, blockLayer))   // 오른쪽에 닿았는지 판별
            SideBlockCheck = 1;
        else                                                                                            // 닿지 않았을 때
            SideBlockCheck = 0;
        return SideBlockCheck != 0;                             // 왼쪽이나 오른쪽에 닿았는지 판별
    }
    /// <summary> 바닥에 닿았을 때 위치를 고정함. </summary>
    /// <param name="rb"></param>
    /// <param name="tr"></param>
    /// <param name="isBlockTouch"></param>
    /// <param name="ignoreBlockCheck"></param>
    /// <param name="isJump"></param>
    /// <param name="originalGravityScale"></param>
    public static void BlockPositionLock(ref Rigidbody2D rb, ref Transform tr, bool isBlockTouch, bool ignoreBlockCheck, ref bool isJump, float originalGravityScale)
    {
        if (isBlockTouch && !ignoreBlockCheck)          // 땅에 닿았을 때
        {
            rb.gravityScale = 0f;                               // 중력 스케일 0으로 설정
            rb.velocity = new Vector2(rb.velocity.x, 0f);       // y축 속도 0으로 설정
            Vector3 position = tr.position;                     // 현재 위치 저장
            position.y = Mathf.Round(position.y);               // y축 위치 반올림
            tr.position = position;                             // 위치 적용
            isJump = false;                                     // 점프 중이 아님
        }
        else                                            // 땅에 닿지 않았을 때
            rb.gravityScale = originalGravityScale;             // 중력 스케일 원래대로 설정
    }
    /// <summary> 옆에 닿았을 때 위치를 고정함. </summary>
    /// <param name="tr"></param>
    /// <param name="SideBlockCheck"></param>
    public static void SidePositionLock(ref Transform tr, int SideBlockCheck)
    {
        if (SideBlockCheck == 1 || SideBlockCheck == -1)
        {
            float xRoundOff = SideBlockCheck == 1 ? -0.38f : 0.38f;             // 오른쪽에 닿았을 때 : 왼쪽에 닿았을 때

            Vector3 position = tr.position;                                     // 현재 위치 저장
            position.x = Mathf.Round(position.x);                               // x축 위치 반올림
            position = new Vector2(position.x + xRoundOff, position.y);         // x축 위치 조정
            tr.position = position;                                             // 위치 적용
        }
    }
    /// <summary> 플레이어 이동을 처리함. 달리기 중일 때 이동 속도 변경. </summary>
    /// <param name="rb"></param>
    /// <param name="SideBlockCheck"></param>
    /// <param name="moveSpeed"></param>
    /// <param name="isMoveLeft"></param>
    /// <param name="isMoveRight"></param>
    public static void MoveX(ref Rigidbody2D rb, int SideBlockCheck, float moveSpeed, out bool isMoveLeft, out bool isMoveRight, bool isRun)
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveXStandard = isRun ? moveX : moveX * 0.5f;                         // 달리기 중일 때 이동 속도 변경
        Vector2 newVelocity = new Vector2(moveXStandard * moveSpeed, rb.velocity.y);        // 새로운 속도
        if (SideBlockCheck == 0 || (SideBlockCheck == 1 && moveX < 0) || (SideBlockCheck == -1 && moveX > 0))
        {
            rb.velocity = newVelocity;                                              // 새로운 속도 적용
            isMoveLeft = moveX < 0;                                                 // 왼쪽으로 이동 중인지 판별
            isMoveRight = moveX > 0;                                                // 오른쪽으로 이동 중인지 판별
        }
        else
        {
            isMoveLeft = false;                                                     // 왼쪽으로 이동 중인지 판별
            isMoveRight = false;                                                    // 오른쪽으로 이동 중인지 판별
        }
    }
    /// <summary> 플레이어 달리기를 처리함. </summary>
    /// <param name="isRun"></param>
    public static void Run(ref bool isRun)
    {
        if (Input.GetKey(KeyCode.LeftShift))
            isRun = true;
        else
            isRun = false;
    }
    /// <summary> 바닥에 닿았는지 판별하는 함수를 무시함. </summary>
    /// <param name="rb"></param>
    /// <param name="ignoreBlockCheck"></param>
    public static void IgnoreBlockPositionLock(Rigidbody2D rb, ref bool ignoreBlockCheck)
    {
        if (rb.velocity.y > 0)                              // 점프 중일 때 (y축 속도가 양수일 때)
            ignoreBlockCheck = true;                            // 블록 체크 무시
        else                                                // 점프 중이 아닐 때 (y축 속도가 음수일 때)
            ignoreBlockCheck = false;                           // 블록 체크 허용
    }
    /// <summary> 여러 점프 상태를 판별함. </summary>
    /// <param name="isBlockTouch"></param>
    /// <param name="isJump"></param>
    /// <param name="isCrouchJump"></param>
    /// <param name="jumpCount"></param>
    /// <param name="canJump"></param>
    public static void JumpCheck(bool isBlockTouch, ref bool isJump, ref bool isCrouchJump, ref int jumpCount, ref bool canJump)
    {
        if (isBlockTouch && !isJump && !isCrouchJump)                           // 땅에 닿고 점프 중이 아닐 때
        {
            jumpCount = 1;                                                          // 점프 가능 횟수 1로 설정
            canJump = true;                                                         // 점프가 가능하다
            isCrouchJump = false;                                                   // 앉은 상태에서 점프 중이 아님
        }
        else if (jumpCount == 0)                                                // 점프 가능 횟수가 0일 때
            canJump = false;                                                        // 점프 불가능
        
        if (!isBlockTouch && !isJump && !isCrouchJump)                          // 땅에서 떨어졌을 때
        {
            isJump = true;                                                          // 점프 중
            jumpCount--;                                                            // 점프 가능 횟수 감소
        }
    }
    /// <summary> 플레이어 점프를 처리함. </summary>
    /// <param name="rb"></param>
    /// <param name="isJump"></param>
    /// <param name="canJump"></param>
    /// <param name="jumpForce"></param>
    /// <param name="jumpCount"></param>
    public static void Jump(ref Rigidbody2D rb, ref bool isJump, bool canJump, float jumpForce, ref int jumpCount)
    {
        if (Input.GetKeyDown(KeyCode.Z) && canJump)                         // 스페이스바를 누르고 점프 가능할 때
        {
            isJump = true;                                                          // 점프 중
            jumpCount--;                                                            // 점프 가능 횟수 감소
            rb.velocity = new Vector2(rb.velocity.x, 0);                            // y축 속도 0으로 설정
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);            // 점프 실행
        }
        else if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0)            // 스페이스바를 떼고 y축 속도가 양수일 때
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);         // y축 속도를 반으로 줄임
        }  
    }
    /// <summary> Velocity의 최대치 설정. 종단속도를 처리함. [[[아직 완성하지 못함]]] </summary>
    /// <param name="rb"></param>
    /// <param name="maxMoveSpeed"></param>
    /// <param name="originalGravityScale"></param>
    /// <param name="isBlockTouch"></param>
    public static void TerminalVelocity(ref Rigidbody2D rb, float maxMoveSpeed, float originalGravityScale, bool isBlockTouch)
    {
        bool reverseX = rb.velocity.x > 0 ? true : false;       // x축 속도가 양수일 때 : x축 속도가 음수일 때
        bool reverseY = rb.velocity.y > 0 ? true : false;       // y축 속도가 양수일 때 : y축 속도가 음수일 때

        if (!isBlockTouch)                                      // 땅에 닿지 않았을 때만 중력 스케일 조정
        {
            if (rb.velocity.y > 0)
                rb.gravityScale = originalGravityScale - 1;         // y축 속도가 양수일 때 중력 스케일 1 감소
            else
                rb.gravityScale = originalGravityScale;             // y축 속도가 음수일 때 중력 스케일 원래대로
        }

        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed)                                            // x축 속도가 최대 이동 속도보다 클 때
            rb.velocity = new Vector2(reverseX ? maxMoveSpeed : -maxMoveSpeed, rb.velocity.y);      // 최대 이동 속도 제한
        if (Mathf.Abs(rb.velocity.y) > maxMoveSpeed)                                            // y축 속도가 최대 이동 속도보다 클 때
        {
            Vector2 saveVelocity = rb.velocity;
            rb.velocity = new Vector2(rb.velocity.x, reverseY ? maxMoveSpeed : -maxMoveSpeed);      // 최대 이동 속도 제한
        }

    }

}
