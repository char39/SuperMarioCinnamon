using UnityEditor;
using UnityEngine;
using MarioCtrl;

[CustomEditor(typeof(Mario))]                          // Mario_Move 스크립트를 인스펙터 창에 표시
public class PlayerMovementRigidbodyEditor : Editor
{
    private const float updateInterval = 0.05f;                                     // 갱신 주기 (초 단위)
    private double lastUpdateTime;                                                  // 마지막 갱신 시간

    void OnEnable()
    {
        lastUpdateTime = EditorApplication.timeSinceStartup;                        // 마지막 갱신 시간 초기화
        EditorApplication.update += OnEditorUpdate;                                 // 에디터 갱신 이벤트 추가 (OnEditorUpdate 함수 호출)
    }

    void OnDisable()
    {
        EditorApplication.update -= OnEditorUpdate;                                 // 에디터 갱신 이벤트 제거 (OnEditorUpdate 함수 호출 중지)
    }

    void OnEditorUpdate()
    {
        if (EditorApplication.timeSinceStartup - lastUpdateTime >= updateInterval)  // 갱신 주기마다
        {
            Repaint();                                                              // 인스펙터 창 갱신
            lastUpdateTime = EditorApplication.timeSinceStartup;                    // 마지막 갱신 시간 갱신
        }
    }

    public override void OnInspectorGUI()                                           // 인스펙터 창에 표시할 내용
    {
        DrawDefaultInspector();                                                     // 기본 인스펙터 창 표시

        Mario playerMovement = (Mario)target;                             // Mario_Move 스크립트를 가져옴

        GUILayout.Space(10);                                                        // 공백 추가
        GUILayout.Label("Velocity", EditorStyles.boldLabel);                        // "Velocity" 레이블 표시
        EditorGUILayout.Vector2Field("Current Velocity", playerMovement.velocity);  // 현재 속도 표시
    }
}