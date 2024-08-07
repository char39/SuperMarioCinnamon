using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    [System.Serializable]                   // 클래스 내부의 값들을 인스펙터 창에 노출 [public only]
    public class PlayerVars                 // 인스턴스화 하여 참조하기 위한 클래스
    {
        [Header("Const, ReadOnly")]                 // *중요* [상수값]

        [Header("Internal Vars, Components")]       // *중요* [참조 전용]
        internal Rigidbody2D rb;
        internal Transform tr;
        internal LayerMask blockLayer;                  // 블럭 레이어 index

        internal Vector2 blockCheckSize;                // 블록 판별을 위한 박스 크기
        internal Vector2 blockLeftCheckSize;            // 블록 왼쪽 판별을 위한 박스 크기
        internal Vector2 blockRightCheckSize;           // 블록 오른쪽 판별을 위한 박스 크기
        internal Transform blockCheck;                  // 블록을 체크할 위치
        internal Transform blockLeftCheck;              // 왼쪽을 체크할 위치
        internal Transform blockRightCheck;             // 오른쪽을 체크할 위치
        internal bool isBlockTouch;                     // 블록 윗부분에 닿았는지 판별
        internal bool isSideTouch;                      // 블록 옆부분에 닿았는지 판별
        internal int SideBlockCheck;                    // 블록 옆 체크 판별 [왼쪽 : -1, 닿지 않음 : 0, 오른쪽 : 1]
        internal bool ignoreBlockCheck;                 // 블록 체크 무시 플래그
        internal bool ignoreSideCheck;                  // 옆면 체크 무시 플래그

        internal float originalGravityScale;            // 원래 중력 스케일을 저장할 변수

        internal float moveLeft;                        // 왼쪽 이동
        internal float moveRight;                       // 오른쪽 이동

        internal bool canJump;                          // 점프 가능한지 판별
        internal bool isJump;                           // 점프 중인지 판별
        internal bool isCrouchJump;                     //   [쓸지 모르겠음..] 앉은 상태에서 점프 중인지 판별 (앉았는가)
        internal int jumpCount;                         // 점프 가능 횟수
        internal float jumpForce;                       // 점프력
        internal float maxFallSpeed;                    // 최대 낙하 속도

        internal bool canMove;                          // 이동 가능한지 판별
        internal bool isMove;                           // 이동 중인지 판별
        internal bool isMoveLeft;                       // 왼쪽으로 이동 중인지 판별
        internal bool isMoveRight;                      // 오른쪽으로 이동 중인지 판별
        internal bool isRun;                            // 달리기 중인지 판별
        internal float moveForce;                       // 이동 속도
        internal float maxMoveSpeed;                    // 최대 이동 속도

    }
}