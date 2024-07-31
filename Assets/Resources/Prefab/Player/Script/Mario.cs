using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarioCtrl
{
    public class Mario : MonoBehaviour
    {
        [Header("Variation Value")] // 수치 바꿔도 전~~~혀 관계없는 변수
        public static float fallSpeed;

        [Header("Internal Vars, Components")]
        internal static Rigidbody2D rb;
        internal static float maxFallSpeed;         // 종단속도
        internal static float jumpForce;            // 점프력
        internal static float maxMoveSpeed;         // 좌우 최대 속도
        internal static float moveForce;            // 좌우 움직이는 힘

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            maxFallSpeed = -20f;
            jumpForce = 10f;
            maxMoveSpeed = 10f;
            moveForce = 50f;
        }

        void FixedUpdate()
        {
            Mario_Move.Move();
            Mario_Jump.Jump();
        }

        void Update()
        {
            Mario_Fall.MaxFallSpeed();
        }
    }
}