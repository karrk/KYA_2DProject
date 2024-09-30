using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerCharacter))]
public class CharacterInfoEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    PlayerCharacter character = (PlayerCharacter)target;

    //    // ±‚∫ª ¿ŒΩ∫∆Â≈Õ ±◊∏Æ±‚
    //    DrawDefaultInspector();

    //    if (Application.isPlaying)
    //    {
    //        EditorGUILayout.Space();
    //        EditorGUILayout.LabelField("µ¶ ¿Œ∫•≈‰∏Æ", EditorStyles.boldLabel);

    //        foreach (var e in character.CharacterInfo.DeckInventory)
    //        {
    //            EditorGUILayout.BeginHorizontal();
    //            EditorGUILayout.LabelField($"µ¶ ID : {e.Key}",GUILayout.Width(60f));
    //            EditorGUILayout.LabelField($"µ¶¿Ã∏ß : {Manager.Instance.Data.GetPlayerDeckData(e.Key).Name}", GUILayout.Width(150f));
    //            EditorGUILayout.LabelField($"∞πºˆ : {e.Value}");
    //            EditorGUILayout.EndHorizontal();
    //        }
    //    }

    //    serializedObject.ApplyModifiedProperties();
    //}
}
