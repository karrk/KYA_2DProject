using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CustomEditor(typeof(MonstersController))]
public class MonstersInfoEditor : Editor, IListener
{
    private bool _isShowReady;
    private bool _isAddListened;

    private void OnEnable()
    {
        if (!Application.isPlaying)
            return;

        _isShowReady = false;
        _isAddListened = false;

        if (!_isAddListened)
        {
            Manager.Instance.Event.AddListener(E_Events.StartBattle, this);
            _isAddListened = true;
        }
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if (m_eventType == E_Events.StartBattle)
            _isShowReady = true;
    }

    public override void OnInspectorGUI()
    {
        MonstersController container = (MonstersController)target;
        DrawDefaultInspector();

        if(_isShowReady)
        {
            for (int i = 0; i < container.MonsterCount; i++)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField($" {container[i+1].CharacterInfo.Name} ", EditorStyles.boldLabel);
                EditorGUILayout.Space();

                foreach (var e in container[i+1].CharacterInfo.DeckInventory)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"µ¦ ID : {e.Key}", GUILayout.Width(60f));
                    EditorGUILayout.LabelField($"µ¦ÀÌ¸§ : {Manager.Instance.Data.GetMobDeckData(e.Key).Name}", GUILayout.Width(150f));
                    EditorGUILayout.LabelField($"°¹¼ö : {e.Value}");
                    EditorGUILayout.EndHorizontal();
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
