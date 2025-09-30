using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EllenPlayerController))]
public class PlayerControllerEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     EllenPlayerController playerController = (EllenPlayerController)target;
    //
    //     EditorGUILayout.Space();
    //     EditorGUILayout.LabelField("Ellen Player", EditorStyles.boldLabel);
    //     EditorGUILayout.BeginVertical(EditorStyles.helpBox);
    //
    //     switch (playerController.State)
    //     {
    //         case Constants.EPlayerState.Idle:
    //             GUI.backgroundColor = new Color(0, 1, 0, 1);
    //             break;
    //         case Constants.EPlayerState.Move:
    //             GUI.backgroundColor = new Color(0, 1, 0, 1);
    //             break;
    //         case Constants.EPlayerState.Jump:
    //             GUI.backgroundColor = new Color(0, 1, 1, 1);
    //             break;
    //     }
    //
    //     EditorGUILayout.BeginVertical(EditorStyles.helpBox);
    //     EditorGUILayout.LabelField("Player State", playerController.State.ToString(), EditorStyles.boldLabel);
    //     EditorGUILayout.EndVertical();
    //     
    //     EditorGUILayout.EndVertical();
    // }
    //
    // private void OnEnable() { EditorApplication.update += OnEditorPUpdate; }
    // private void OnDisable() { EditorApplication.update -= OnEditorPUpdate; }
    // private void OnEditorPUpdate() { if (target != null) Repaint(); }
}