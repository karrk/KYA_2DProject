using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerCharacter))]
public class CharacterInfoEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    PlayerCharacter character = (PlayerCharacter)target;

    //    // �⺻ �ν����� �׸���
    //    DrawDefaultInspector();

    //    if (Application.isPlaying)
    //    {
    //        EditorGUILayout.Space();
    //        EditorGUILayout.LabelField("�� �κ��丮", EditorStyles.boldLabel);

    //        foreach (var e in character.CharacterInfo.DeckInventory)
    //        {
    //            EditorGUILayout.BeginHorizontal();
    //            EditorGUILayout.LabelField($"�� ID : {e.Key}",GUILayout.Width(60f));
    //            EditorGUILayout.LabelField($"���̸� : {Manager.Instance.Data.GetPlayerDeckData(e.Key).Name}", GUILayout.Width(150f));
    //            EditorGUILayout.LabelField($"���� : {e.Value}");
    //            EditorGUILayout.EndHorizontal();
    //        }
    //    }

    //    serializedObject.ApplyModifiedProperties();
    //}
}
