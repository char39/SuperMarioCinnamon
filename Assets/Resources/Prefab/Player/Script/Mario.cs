using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCtrl;

public class Mario : MonoBehaviour
{
    public PlayerVars mario;
    public Mario(PlayerVars playerVars) { mario = playerVars; }   // 생성자 (mario 변수 초기화)

    [Header("Vars Value [영향 X]")]              // 수치 바꿔도 전~~~혀 관계없음 [인스펙터 전용]
    public Rigidbody2D rb;
    public Transform tr;
    


    void Start()                            // void Start
    {
        StartVarsComponents();
        StartRigidBody();
    }

    private void StartVarsComponents()                  // 변수 초기화
    {
        mario.rb = GetComponent<Rigidbody2D>();
        mario.tr = GetComponent<Transform>();
    }
    private void StartRigidBody()                       // RigidBody 초기화
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

        mario.rb.velocity = Vector2.zero;    // 초기 속도 초기화
    }

    void FixedUpdate()                      // void FixedUpdate
    {

    }

    void Update()                           // void Update
    {
        ShowInspectorValue();
    }

    private void ShowInspectorValue()
    {
        rb = mario.rb;
        tr = mario.tr;
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

        }
    }



}
