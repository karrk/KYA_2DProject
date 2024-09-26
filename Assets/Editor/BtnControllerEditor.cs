using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

[CustomEditor(typeof(BtnController))]
public class BtnControllerEditor : Editor
{
    private string[] _methodsName;
    private MethodInfo[] _methods;
    private int _methodIdx;
    private MethodInfo _selectedMethod;
    private SerializedProperty _savedIdx;

    private void OnEnable()
    {
        GetMethods(typeof(BtnFunctions));
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _methodIdx = EditorGUILayout.Popup("대기중인 함수", _methodIdx, _methodsName);
        _selectedMethod = _methods[_methodIdx];
    }

    private void GetMethods(Type m_type)
    {
        _methods = m_type.GetMethods(BindingFlags.Public
             | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        _methodsName = new string[_methods.Length];

        for (int i = 0; i < _methodsName.Length; i++)
        {
            _methodsName[i] = _methods[i].Name;
        }
    }
}
