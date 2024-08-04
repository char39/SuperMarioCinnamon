using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mario_Move))]
public class PlayerMovementRigidbodyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Mario_Move playerMovement = (Mario_Move)target;

        GUILayout.Space(10);
        GUILayout.Label("Velocity", EditorStyles.boldLabel);
        EditorGUILayout.Vector2Field("Current Velocity", playerMovement.velocity);
    }
}