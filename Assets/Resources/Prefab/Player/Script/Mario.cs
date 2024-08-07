using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCtrl;

namespace MarioCtrl
{
    public class Mario : MonoBehaviour
    {
        public static Mario instance;                                   // Mario 클래스의 인스턴스
        public PlayerVars mario;                                        // PlayerVars 클래스 참조
        public Mario(PlayerVars playerVars) { mario = playerVars; }     // 생성자 (mario 변수 초기화)

        #region //////////////////////////// 변수 노출 ////////////////////////////
        [Header("Vars Setting [영향 O]")]               // *중요*
        public LayerMask blockLayer;

        [Header("Vars Value [영향 X]")]                 // 수치 바꿔도 전~~~혀 관계없음 [인스펙터 전용]
        public Rigidbody2D rb;
        public Transform tr;
        public bool isBlockTouch;
        public bool isSideTouch;
        public int SideBlockCheck;
        public bool ignoreBlockCheck;
        public float originalGravityScale;
        public bool canJump;
        public bool isJump;
        public bool isCrouchJump;
        public int jumpCount;
        public float jumpForce;
        public bool canMove;
        public bool isMove;
        public bool isMoveLeft;
        public bool isMoveRight;
        public float moveSpeed;
        public float maxMoveSpeed;

        [HideInInspector] public Vector2 velocity;
        #endregion //////////////////////////////////////////////////////////////

        void Awake()                        // void Awake
        {
            instance = this;
        }

        void Start()                        // void Start
        {
            StartComponents();
            StartRigidBody();
            StartVars();
        }

        void FixedUpdate()                  // void FixedUpdate
        {
            PlayerMoveUpdate();
        }

        void Update()                       // void Update
        {
            PlayerJumpUpdate();
            InspectorUpdate();
        }

        private void StartComponents()          // 컴포넌트 값 초기화
        {
            mario.rb = GetComponent<Rigidbody2D>();
            mario.tr = GetComponent<Transform>();
        }
        private void StartRigidBody()           // RigidBody 초기화
        {
            mario.rb.bodyType = RigidbodyType2D.Dynamic;    // 물리 효과
            mario.rb.useAutoMass = false;    // 질량 자동 계산
            mario.rb.mass = 1.0f;    // 질량
            mario.rb.drag = 0.0f;    // 공기 저항
            mario.rb.angularDrag = 0.05f;    // 회전 저항
            mario.rb.gravityScale = 6.0f;    // 중력 적용
            mario.rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;    // 회전 제한
            mario.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;    // 충돌 감지
            mario.rb.sleepMode = RigidbodySleepMode2D.StartAwake;    // 시작시 깨어있음
            mario.rb.interpolation = RigidbodyInterpolation2D.Interpolate;    // 보간

            mario.rb.velocity = Vector3.zero;    // 초기 속도 초기화
        }
        private void StartVars()                // 변수 초기화
        {
            mario.blockLayer = blockLayer;                          // LayerMask

            mario.blockCheckSize = new Vector2(0.7f, 0.1f);         // Vector2
            mario.blockLeftCheckSize = new Vector2(0.1f, 0.9f);     // Vector2
            mario.blockRightCheckSize = new Vector2(0.1f, 0.9f);    // Vector2
            mario.blockCheck = mario.tr.GetChild(0);                // Transform
            mario.blockLeftCheck = mario.tr.GetChild(1);            // Transform
            mario.blockRightCheck = mario.tr.GetChild(2);           // Transform
            mario.isBlockTouch = false;                             // bool
            mario.isSideTouch = false;                              // bool
            mario.SideBlockCheck = 0;                               // int
            mario.ignoreBlockCheck = false;                         // bool

            mario.originalGravityScale = mario.rb.gravityScale;      // float

            mario.moveLeft = -1.0f;                                  // float
            mario.moveRight = 1.0f;                                  // float

            mario.canJump = false;                                   // bool
            mario.isJump = false;                                    // bool
            mario.isCrouchJump = false;                              // bool
            mario.jumpCount = 0;                                     // int
            mario.jumpForce = 22.0f;                                 // float

            mario.canMove = true;                                    // bool
            mario.isMove = false;                                    // bool
            mario.isMoveLeft = false;                                // bool
            mario.isMoveRight = false;                               // bool
            mario.isRun = false;                                     // bool
            mario.moveSpeed = 12.0f;                                 // float
            mario.maxMoveSpeed = 30.0f;                              // float
        }

        private void PlayerMoveUpdate()         // 플레이어 이동 업데이트
        {
            PlayerMove.IgnoreBlockPositionLock(mario.rb, ref mario.ignoreBlockCheck);
            mario.isBlockTouch = PlayerMove.BlockCheck(mario.blockCheck, mario.blockCheckSize, mario.blockLayer);
            mario.isSideTouch = PlayerMove.SideCheck(mario.blockLeftCheck, mario.blockRightCheck, mario.blockLeftCheckSize, mario.blockRightCheckSize, mario.blockLayer, out mario.SideBlockCheck);
            PlayerMove.BlockPositionLock(ref mario.rb, ref mario.tr, mario.isBlockTouch, mario.ignoreBlockCheck, ref mario.isJump, mario.originalGravityScale);
            PlayerMove.SidePositionLock(ref mario.tr, mario.SideBlockCheck);
            PlayerMove.Run(ref mario.isRun);
            PlayerMove.MoveX(ref mario.rb, mario.SideBlockCheck, mario.moveSpeed, out mario.isMoveLeft, out mario.isMoveRight, mario.isRun);
            PlayerMove.TerminalVelocity(ref mario.rb, mario.maxMoveSpeed, mario.originalGravityScale, isBlockTouch);
        }
        private void PlayerJumpUpdate()         // 플레이어 점프 업데이트
        {
            PlayerMove.JumpCheck(mario.isBlockTouch, ref mario.isJump, ref mario.isCrouchJump, ref mario.jumpCount, ref mario.canJump);
            PlayerMove.Jump(ref mario.rb, ref mario.isJump, mario.canJump, mario.jumpForce, ref mario.jumpCount);
        }

        private void InspectorUpdate()          // 인스펙터에 값 출력용
        {
            rb = mario.rb;
            tr = mario.tr;
            velocity = mario.rb.velocity;
            isBlockTouch = mario.isBlockTouch;
            isSideTouch = mario.isSideTouch;
            SideBlockCheck = mario.SideBlockCheck;
            ignoreBlockCheck = mario.ignoreBlockCheck;
            originalGravityScale = mario.originalGravityScale;
            canJump = mario.canJump;
            isJump = mario.isJump;
            isCrouchJump = mario.isCrouchJump;
            jumpCount = mario.jumpCount;
            jumpForce = mario.jumpForce;
            canMove = mario.canMove;
            isMove = mario.isMove;
            isMoveLeft = mario.isMoveLeft;
            isMoveRight = mario.isMoveRight;
            moveSpeed = mario.moveSpeed;
            maxMoveSpeed = mario.maxMoveSpeed;
        }
        private void OnDrawGizmos()                 // 에디터 상에서만 실행되는 함수. 캐릭터의 실시간 벡터 그리기
        {
            if (!Application.isPlaying) return;
            PlayerMove.DrawVectorGizmos(mario.tr, mario.rb.velocity);
        }
        private void OnDrawGizmosSelected()         // 에디터 상에서만 실행되는 함수. 블록 판별을 위한 체크 박스 그리기
        {
            if (!Application.isPlaying) return;
            PlayerMove.DrawCollisionGizmos(mario.blockCheck, mario.blockCheckSize, mario.blockLeftCheck, mario.blockLeftCheckSize, mario.blockRightCheck, mario.blockRightCheckSize);
        }

    }
}