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
            vars.rb = GetComponent<Rigidbody2D>();
            vars.tr = GetComponent<Transform>();
        }
        private void StartVars()                        // void Start() 변수 값 할당
        {
            vars.maxFallSpeed = -20f;
            vars.gravityMultiValue = 3.0f;
            vars.jumpForce = 17f;
            vars.maxMoveSpeed = 9f;
            vars.moveForce = 50f;
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
            startmariogravity();
        }

        private void FixedUpdateComponents()            // void FixedUpdate() 컴포넌트 값 할당
        {

        }
        private void FixedUpdateVars()                  // void FixedUpdate() 변수 값 할당
        {

        }
        private void FixedUpdateMethod()                // void FixedUpdate() 에 메서드를 1회 호출
        {
            fixedupdatemove();
        }

        private void UpdateComponents()                 // void Update() 컴포넌트 값 할당
        {

        }
        private void UpdateVars()                       // void Update() 변수 값 할당
        {

        }
        private void UpdateMethod()                     // void Update() 에 메서드를 1회 호출
        {
            updateMaxfallspeed();
            updateJump();
            velocitylimit();

        }

        private void LateUpdateComponents()             // void LateUpdate() 컴포넌트 값 할당
        {

        }
        private void LateUpdateVars()                   // void LateUpdate() 변수 값 할당
        {
            isGroundEnter = vars.isGroundEnter;
            isSideCollision = vars.isSideCollision;
            sideCollision = vars.sideCollision;
            isJump = vars.isJump;
            fallSpeed = vars.rb.velocity.y;
            moveInput = vars.moveInput;
            moveSpeed = vars.rb.velocity.x;
        }
        private void LateUpdateMethod()                 // void LateUpdate() 에 메서드를 1회 호출
        {

        }

        /* void OnCollisionEnter2D(Collision2D col)
        {
            if ((vars.groundLayer.value & (1 << col.gameObject.layer)) > 0)
            {
                foreach (ContactPoint2D contact in col.contacts)
                {
                    if (Mathf.Abs(contact.normal.x) > 0.5f) // 옆부분에 닿았나
                    {
                        if (contact.normal.y > 0.5f ) // 윗부분에 닿았는지 확인
                        {
                            isGroundTouch = //Mario_Gravity.GroundTouchEnter(ref vars.isGroundTouch, ref vars.isJump);
                            break;
                        }
                    }
                    else if (contact.normal.y > 0.5f ) // 윗부분에 닿았나
                    {
                        isGroundTouch = //Mario_Gravity.GroundTouchEnter(ref vars.isGroundTouch, ref vars.isJump);
                        break;
                    }
                }
            }
        }

        void OnCollisionStay2D(Collision2D col)
        {
            if ((vars.groundLayer.value & (1 << col.gameObject.layer)) > 0)
            {
                foreach (ContactPoint2D contact in col.contacts)
                {
                    if (Mathf.Abs(contact.normal.x) > 0.5f) // 옆부분에 닿았나
                    {
                        if (contact.normal.y > 0.5f ) // 윗부분에 닿았는지 확인
                        {
                            isGroundTouch = //Mario_Gravity.GroundTouchEnter(ref vars.isGroundTouch, ref vars.isJump);
                            break;
                        }
                    }
                    else if (contact.normal.y > 0.5f ) // 윗부분에 닿았나
                    {
                        isGroundTouch = //Mario_Gravity.GroundTouchEnter(ref vars.isGroundTouch, ref vars.isJump);
                        break;
                    }
                }
            }
        }

        void OnCollisionExit2D(Collision2D col)
        {
            if ((vars.groundLayer.value & (1 << col.gameObject.layer)) > 0)
            {
                isGroundTouch = //Mario_Gravity.GroundTouchExit(ref vars.isGroundTouch);
            }
        } */
 

        void startmariogravity()
        {
            vars.rb.gravityScale = 3.0f;                // 중력을 velocity로 사용
            vars.rb.velocity = Vector2.zero;            // velocity 초기화
            vars.rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation; // 회전 제한
        }
        void updateMaxfallspeed()
        {
            //fallSpeed = vars.rb.velocity.y;         // 인스펙터용
            if (vars.rb.velocity.y < vars.maxFallSpeed) // 종단속도보다 빠르면
                vars.rb.velocity = new Vector2(vars.rb.velocity.x, vars.maxFallSpeed);  // 종단속도로 제한
        }

        void updateJump()
        {
            if (Input.GetKeyDown(KeyCode.Z) && !vars.isJump) // 점프키 누르고 점프중이 아니고 땅에 닿았을 때
            {
                vars.rb.velocity = new Vector2(0, vars.jumpForce); // 점프
                vars.isJump = true; // 점프중
                vars.isGroundEnter = false; // 땅에서 떨어짐
            }
        }

        void fixedupdatemove()
        {
            float targetMoveInput = 0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                targetMoveInput = -1f;
            else if (Input.GetKey(KeyCode.RightArrow))
                targetMoveInput = 1f;
            

            // 부드러운 움직임을 위해 moveInput을 점진적으로 변경
            if (vars.moveInput >= 0)
            {
                vars.moveInput = Mathf.Lerp(vars.moveInput, targetMoveInput, vars.acceleration * Time.fixedDeltaTime); // 가속
            }
            else if (vars.moveInput < 0)
            {
                vars.moveInput = Mathf.Lerp(vars.moveInput, targetMoveInput, vars.acceleration * Time.fixedDeltaTime); // 가속
            }

            // 방향 전환 시 즉시 속도를 0으로 설정하지 않음
            if (Mathf.Sign(vars.moveInput) != Mathf.Sign(targetMoveInput) && targetMoveInput != 0) // 방향 전환 시
            {
                vars.moveInput = Mathf.Lerp(vars.moveInput, 0, vars.deceleration * Time.fixedDeltaTime); // 감속
            }
            
            

            string roundedMoveInputStr = vars.moveInput.ToString("F2"); // 소수점 이하 2자리까지만 남기고 나머지 제거
            float roundMoveInput = float.Parse(roundedMoveInputStr);
            Vector3 moveVelocity = new Vector3(roundMoveInput * vars.maxMoveSpeed, 0, 0);
            vars.tr.position += moveVelocity * Time.deltaTime;

            

            if (vars.isGroundEnter)
                vars.rb.gravityScale = 0.0f;
            else if (!vars.isGroundEnter)
                vars.rb.gravityScale = 3.0f;
        }
        void velocitylimit()
        {
            if (vars.rb.velocity.x > vars.maxMoveSpeed)
                vars.rb.velocity = new Vector2(vars.maxMoveSpeed, vars.rb.velocity.y);
            else if (vars.rb.velocity.x < -vars.maxMoveSpeed)
                vars.rb.velocity = new Vector2(-vars.maxMoveSpeed, vars.rb.velocity.y);
        }

        IEnumerator OnCollisionStay2D(Collision2D col)
        {
            if (this.gameObject == null) yield break;       // 유효성 검사
            if ((vars.sideGroundLayer.value & (1 << col.gameObject.layer)) > 0)     // 옆 블록 충돌 판정
            {
                while (!vars.isSideCollision)
                {
                    foreach (ContactPoint2D contact in col.contacts)
                    {
                        if (Mathf.Abs(contact.normal.x) > 0.5f) // 옆부분에 닿았나
                        {
                            vars.isSideCollision = true;
                            vars.sideCollision = contact.normal.x > 0 ? 1 : -1;
                            break;
                        }
                        yield return null;
                    }
                    yield return new WaitForSeconds(0.05f);
                }
            }
            if ((vars.groundLayer.value & (1 << col.gameObject.layer)) > 0)
            {
                StartCoroutine(groundTouch(col));
            }
        }
        IEnumerator groundTouch(Collision2D col)        // 땅 충돌 판정
        {
            while (!vars.isGroundEnter)
            {
                foreach (ContactPoint2D contact in col.contacts)
                {
                    if (Mathf.Abs(contact.normal.x) > 0.5f) // 옆부분에 닿았나
                    {
                        if (contact.normal.y > 0.5f && vars.rb.velocity.y == 0) // 윗부분에 닿았는지 확인
                        {
                            vars.isGroundEnter = true;
                            vars.isJump = false;
                            break;
                        }
                    }
                    else if (contact.normal.y > 0.5f && vars.rb.velocity.y == 0) // 윗부분에 닿았나
                    {
                        vars.isGroundEnter = true;
                        vars.isJump = false;
                        break;
                    }
                    yield return null;
                }
                yield return new WaitForSeconds(0.05f);
            }
        }

        IEnumerator OnCollisionExit2D(Collision2D col)
        {
            if (this.gameObject == null) yield break;       // 유효성 검사
            if ((vars.sideGroundLayer.value & (1 << col.gameObject.layer)) > 0)     // 옆 블록에서 떨어질 때
            {
                while (vars.isSideCollision)
                {
                    vars.isSideCollision = false;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            if ((vars.groundLayer.value & (1 << col.gameObject.layer)) > 0)     // 땅에서 떨어질 때
            {
                while (vars.isGroundEnter)
                {
                    vars.isGroundEnter = false;
                    vars.isJump = true;
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }
}