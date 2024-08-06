using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static void DrawVectorGizmos(Transform tr, Vector2 velocity)
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(tr.position, tr.position + (Vector3)velocity);
    }

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

    public static bool BlockCheck(Transform blockCheck, Vector2 blockCheckSize, LayerMask blockLayer)
    {
        return Physics2D.OverlapBox(blockCheck.position, blockCheckSize, 0f, blockLayer);               // 바닥에 닿았는지 판별
    }

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

    public static void BlockPositionLock(ref Rigidbody2D rb, ref Transform tr, bool isBlockTouch, bool ignoreBlockCheck, float originalGravityScale)
    {
        if (isBlockTouch && !ignoreBlockCheck)          // 땅에 닿았을 때
        {
            rb.gravityScale = 0f;                               // 중력 스케일 0으로 설정
            rb.velocity = new Vector2(rb.velocity.x, 0f);       // y축 속도 0으로 설정
            Vector3 position = tr.position;                     // 현재 위치 저장
            position.y = Mathf.Round(position.y);               // y축 위치 반올림
            tr.position = position;                             // 위치 적용
        }
        else                                            // 땅에 닿지 않았을 때
            rb.gravityScale = originalGravityScale;             // 중력 스케일 원래대로 설정
    }

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

    public static Vector3 MoveX(Rigidbody2D rb, int SideBlockCheck, float moveSpeed, out bool isMoveLeft, out bool isMoveRight)
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        Vector2 newVelocity = new Vector2(moveX * moveSpeed, rb.velocity.y);        // 새로운 속도
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
        return rb.velocity;
    }

    public static bool JumpCheck(Rigidbody2D rb, bool isGrounded, bool isJump, bool isCrouchJump, int jumpCount, float jumpForce, bool canJump)
    {
        if (isGrounded && !isJump && !isCrouchJump)                             // 땅에 닿았을 때
        {
            jumpCount = 0;                                                          // 점프 횟수 초기화
            canJump = true;                                                         // 점프 가능
        }
        else if (jumpCount < 2 && !isJump && !isCrouchJump)                     // 점프 횟수가 2미만이고 점프 중이 아닐 때
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);                    // 점프
            jumpCount++;                                                            // 점프 횟수 증가
            canJump = false;                                                        // 점프 불가능
        }
        else
            canJump = false;                                                        // 점프 불가능
        return canJump;
    }

    public static void Jump(ref Rigidbody2D rb, bool canJump, float jumpForce, ref bool ignoreGroundCheck)
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            ignoreGroundCheck = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            IgnoreJumpCheckTime();
        }
    }

    static IEnumerator IgnoreJumpCheckTime()
    {
        yield return new WaitForSeconds(0.1f);
    }


}
