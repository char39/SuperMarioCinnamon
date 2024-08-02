using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MarioCtrl
{
    [System.Serializable]
    public struct MarioVars
    {
        [Header("Components Value")]                // *중요* 컴포넌트 값 [인스펙터에서 할당]
        public LayerMask groundLayer;                   // 땅의 레이어 index
        public LayerMask sideGroundLayer;               // 옆 블록의 레이어 index
        public LayerMask backGroundLayer;               // 뒷 배경의 레이어 index

        [Header("Const, ReadOnly")]                 // *중요* [상수값]

        [Header("Internal Vars, Components")]       // *중요* [참조 전용]
        internal Rigidbody2D rb;
        internal Transform tr;

        internal float maxFallSpeed;                    // [Mario_Gravity.cs] 종단속도
        internal bool isGroundEnter;                    // [Mario_Gravity.cs] 블록 충돌판정
        internal int sideCollision;                     // [Mario_Gravity.cs] 옆 블록 충돌판정
        internal bool isSideCollision;                  // [Mario_Gravity.cs] 옆 블록 충돌판정
        internal bool isJump;                           // [Mario_Jump.cs] 점프 유무
        internal float gravityMultiValue;               // [Mario_Gravity.cs] 중력 배율
        internal float jumpForce;                       // [Mario_Jump.cs] 점프력
        internal float maxMoveSpeed;                    // [Mario_Move.cs] 좌우 최대 속도
        internal float moveForce;                       // [Mario_Move.cs] 좌우 움직이는 힘

        internal float acceleration;                    // [Mario_Move.cs] 가속도
        internal float deceleration;                    // [Mario_Move.cs] 감속도
        internal float moveInput;                       // [Mario_Move.cs] 좌우 입력값
        
    }

    public class Mario : MonoBehaviour
    {
        public MarioVars vars;
        public Mario(MarioVars marioVars) { vars = marioVars; }     // 생성자

        [Header("Vars Value [영향 X]")]              // 수치 바꿔도 전~~~혀 관계없음 [인스펙터 전용]
        public Rigidbody2D rb;                          // [Mario.cs] 물리 효과
        public float fallSpeed;                         // [Mario_Gravity.cs] 현재 떨어지는 속도
        public bool isGroundEnter;                      // [Mario_Gravity.cs] 블록 충돌판정
        public int sideCollision;                       // [Mario_Gravity.cs] 옆 블록 충돌판정
        public bool isSideCollision;                    // [Mario_Gravity.cs] 옆 블록 충돌판정
        public bool isJump;                             // [Mario_Jump.cs] 점프 유무
        public float moveInput;                         // [Mario_Move.cs] 좌우 입력값
        public float moveSpeed;                         // [Mario_Move.cs] 현재 속도



        void Start()
        {
            StartComponents();
            StartVars();
            StartMethod();
        }
        void FixedUpdate()
        {
            FixedUpdateComponents();
            FixedUpdateVars();
            FixedUpdateMethod();
        }
        void Update()
        {
            UpdateComponents();
            UpdateVars();
            UpdateMethod();
        }
        void LateUpdate()
        {
            LateUpdateComponents();
            LateUpdateVars();
            LateUpdateMethod();
        }

        private void StartComponents()                  // void Start() 컴포넌트 값 할당
        {
            vars.rb = this.gameObject.GetComponent<Rigidbody2D>();
            vars.tr = GetComponent<Transform>();
        }
        private void StartVars()                        // void Start() 변수 값 할당
        {
            vars.maxFallSpeed = -20f;
            vars.gravityMultiValue = 3.0f;
            vars.jumpForce = 17f;
            vars.maxMoveSpeed = 9f;
            vars.moveForce = 5f;
            vars.isGroundEnter = false;
            vars.isSideCollision = false;
            vars.sideCollision = 0;                     // 0: 미충돌, -1: 왼쪽, 1: 오른쪽
            vars.isJump = true;
            vars.moveInput = 0f;
            vars.acceleration = 2.0f;
            vars.deceleration = 2.0f;
        }
        private void StartMethod()                      // void Start() 에 메서드를 1회 호출
        {
            StartRigidBody();
        }

        private void FixedUpdateComponents()            // void FixedUpdate() 컴포넌트 값 할당
        {

        }
        private void FixedUpdateVars()                  // void FixedUpdate() 변수 값 할당
        {

        }
        private void FixedUpdateMethod()                // void FixedUpdate() 에 메서드를 1회 호출
        {

        }

        private void UpdateComponents()                 // void Update() 컴포넌트 값 할당
        {

        }
        private void UpdateVars()                       // void Update() 변수 값 할당
        {
            rb = vars.rb;
            fallSpeed = vars.maxFallSpeed;
            isGroundEnter = vars.isGroundEnter;
            sideCollision = vars.sideCollision;
            isSideCollision = vars.isSideCollision;
            isJump = vars.isJump;
            moveInput = vars.moveInput;
            moveSpeed = vars.maxMoveSpeed;
        }
        private void UpdateMethod()                     // void Update() 에 메서드를 1회 호출
        {
            asdf();
        }

        private void LateUpdateComponents()             // void LateUpdate() 컴포넌트 값 할당
        {

        }
        private void LateUpdateVars()                   // void LateUpdate() 변수 값 할당
        {

        }
        private void LateUpdateMethod()                 // void LateUpdate() 에 메서드를 1회 호출
        {

        }

        public void StartRigidBody()                        // RigidBody 초기화
        {
            vars.rb.bodyType = RigidbodyType2D.Dynamic;    // 물리 효과
            vars.rb.useAutoMass = false;    // 질량 자동 계산
            vars.rb.mass = 1.0f;    // 질량
            vars.rb.drag = 0.0f;    // 공기 저항
            vars.rb.angularDrag = 0.05f;    // 회전 저항
            vars.rb.gravityScale = 3.0f;    // 중력 적용
            vars.rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;    // 회전 제한
            vars.rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;    // 충돌 감지
            vars.rb.sleepMode = RigidbodySleepMode2D.StartAwake;    // 시작시 깨어있음
            vars.rb.interpolation = RigidbodyInterpolation2D.Interpolate;    // 보간

            vars.rb.velocity = Vector2.zero;    // 초기 속도 초기화
        }

        public void asdf()
        {
            // 왼쪽 화살표를 누르고 있는 경우
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                // 물체의 속도를 왼쪽으로 설정합니다.
                vars.rb.velocity = new Vector2(-moveSpeed, vars.rb.velocity.y);

                // scale 값을 이용해 캐릭터가 이동 방향을 바라보게 합니다.
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 화살표를 누르고 있는 경우
            {
                // 물체의 속도를 오른쪽으로 설정합니다.
                vars.rb.velocity = new Vector2(moveSpeed, vars.rb.velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) // 이동 키를 뗀 경우
            {
                // x 속도를 줄여 이동 방향으로 아주 살짝만 움직이고 거의 바로 멈추게 합니다.
                vars.rb.velocity = new Vector2(0, vars.rb.velocity.y);
            }

            // 스페이스바를 누르면 점프합니다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded())
                    vars.rb.velocity = new Vector2(vars.rb.velocity.x, vars.jumpForce);
            }
        }

        public bool IsGrounded()
        {
            // 캐릭터를 중심으로 아래 방향으로 ray 를 쏘아 그 곳에 Layer 타입이 Ground 인 객체가 있는지 검사합니다.
            var ray = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << LayerMask.NameToLayer("Ground"));
            return ray.collider != null;
        }


    }
}



/* public void asdf()
        {
            // 왼쪽 화살표를 누르고 있는 경우
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                // 물체에 왼쪽 방향으로 물리적 힘을 가해줍니다. 즉, 왼쪽으로 이동 시킵니다.
                vars.rb.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);

                // velocity 는 물체의 속도입니다. 일정 속도에 도달하면 더 이상 빨라지지 않게합니다.
                vars.rb.velocity = new Vector2(Mathf.Max(vars.rb.velocity.x, -vars.maxMoveSpeed), vars.rb.velocity.y);

                // scale 값을 이용해 캐릭터가 이동 방향을 바라보게 합니다.
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 화살표를 누르고 있는 경우
            {
                vars.rb.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
                vars.rb.velocity = new Vector2(Mathf.Min(vars.rb.velocity.x, vars.maxMoveSpeed), vars.rb.velocity.y);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) // 이동 키를 뗀 경우
            {
                // x 속도를 줄여 이동 방향으로 아주 살짝만 움직이고 거의 바로 멈추게 합니다.
                vars.rb.velocity = new Vector3(vars.rb.velocity.normalized.x, vars.rb.velocity.y);
            }

            // 스페이스바를 누르면 점프합니다.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGrounded())
                    vars.rb.AddForce(Vector2.up * vars.jumpForce, ForceMode2D.Impulse);
            }
        }

        public bool IsGrounded()
        {
            // 캐릭터를 중심으로 아래 방향으로 ray 를 쏘아 그 곳에 Layer 타입이 Ground 인 객체가 있는지 검사합니다.
            var ray = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << LayerMask.NameToLayer("Ground"));
            return ray.collider != null;
        } */