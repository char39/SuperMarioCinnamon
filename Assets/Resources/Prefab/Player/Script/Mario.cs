using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCtrl;

namespace MarioCtrl
{
    public class Mario : MonoBehaviour
    {
        public PlayerVars mario;                                    // PlayerVars 클래스 참조
        public Mario(PlayerVars playerVars) { mario = playerVars; } // 생성자 (mario 변수 초기화)

        [Header("Vars Value [영향 X]")]                 // 수치 바꿔도 전~~~혀 관계없음 [인스펙터 전용]
        public Vector2 velocity;
        public Rigidbody2D rb;
        public Transform tr;

        void Start()                            // void Start
        {
            StartComponents();
            StartVars();
            StartRigidBody();
        }

        private void StartComponents()      // 컴포넌트 값 초기화
        {
            mario.rb = GetComponent<Rigidbody2D>();
            mario.tr = GetComponent<Transform>();
        }
        private void StartVars()            // 변수 초기화
        {
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
            mario.ignoreSideCheck = false;                          // bool

            mario.originalGravityScale = mario.rb.gravityScale;      // float

            mario.moveLeft = -1.0f;                                  // float
            mario.moveRight = 1.0f;                                  // float

            mario.canJump = false;                                   // bool
            mario.isJump = false;                                    // bool
            mario.isCrouchJump = false;                              // bool
            mario.jumpCount = 0;                                     // int
            mario.jumpForce = 10.0f;                                 // float

            mario.canMove = true;                                    // bool
            mario.isMove = false;                                    // bool
            mario.moveSpeed = 8.0f;                                  // float
            mario.maxMoveSpeed = 10.0f;                              // float
        }
        private void StartRigidBody()       // RigidBody 초기화
        {
            mario.rb.bodyType = RigidbodyType2D.Dynamic;    // 물리 효과
            mario.rb.useAutoMass = false;    // 질량 자동 계산
            mario.rb.mass = 1.0f;    // 질량
            mario.rb.drag = 0.0f;    // 공기 저항
            mario.rb.angularDrag = 0.05f;    // 회전 저항
            mario.rb.gravityScale = 3.0f;    // 중력 적용
            mario.rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;    // 회전 제한
            mario.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;    // 충돌 감지
            mario.rb.sleepMode = RigidbodySleepMode2D.StartAwake;    // 시작시 깨어있음
            mario.rb.interpolation = RigidbodyInterpolation2D.Interpolate;    // 보간

            mario.rb.velocity = Vector3.zero;    // 초기 속도 초기화
        }

        void FixedUpdate()                      // void FixedUpdate
        {

        }

        void Update()                           // void Update
        {
            InspectorUpdate();
        }

        private void InspectorUpdate()      // 인스펙터에 값 출력용
        {
            rb = mario.rb;
            tr = mario.tr;
            velocity = mario.rb.velocity;
        }



    }
}