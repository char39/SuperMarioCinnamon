using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCtrl
{
    [System.Serializable]                   // 클래스 내부의 값들을 인스펙터 창에 노출 [public 변수만]
    public class PlayerVars                 // 인스턴스화 하여 객체 참조를 위한 클래스
    {
        [Header("Layer Value")]                 // *중요* 레이어 값 [인스펙터에서 할당된 값이 참조됨]
        public LayerMask usedGroundLayer;               // 땅의 레이어 index
        public LayerMask sideGroundLayer;               // 옆 블록의 레이어 index
        public LayerMask backGroundLayer;               // 뒷 배경의 레이어 index
        
        [Header("Const, ReadOnly")]                 // *중요* [상수값]

        [Header("Internal Vars, Components")]       // *중요* [참조 전용]
        internal Rigidbody2D rb;
        internal Transform tr;
        internal bool isGroundTouch;                    // 블록 윗부분에 닿았는지 판별
        internal bool isGroundSideTouch;                // 블록 옆부분에 닿았는지 판별

        internal bool isPossibleJump;                   // 점프 가능한지 판별 (서있거나, 앉아있거나)
        internal bool isJump;                           // 점프 중인지 판별 (섰는가)
        internal bool isCrouchJump;                     // 앉은 상태에서 점프 중인지 판별 (앉았는가)
        internal int jumpCount;                         // 점프 가능 횟수
        internal float jumpForce;                       // 점프력
        
        internal bool isPossibleMove;                   // 이동 가능한지 판별
        internal bool isMove;                           // 이동 중인지 판별
        internal float moveSpeed;                       // 이동 속도
        internal float maxMoveSpeed;                    // 최대 이동 속도
        internal bool isCrouch;                         // 앉았는지 판별
    }
}