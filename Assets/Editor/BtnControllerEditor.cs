using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.UI;

[CustomEditor(typeof(BtnController))]
public class BtnControllerEditor : Editor
{
    BtnController _btnController;
    private string[] _methodsName;
    private MethodInfo[] _methods;
    private int _methodIdx;
    private MethodInfo _selectedMethod;

    private float _editFloat;
    private string _editStr;
    private GameObject _editGameobj;

    private Type _parameterType;
    private bool _isChangedMethod;

    private void OnEnable()
    {
        _isChangedMethod = true;
        GetMethods(typeof(BtnFunctions));
        _btnController = (BtnController)target;
        ReloadPrevMethod();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying)
        {
            _methodIdx = EditorGUILayout.Popup("대기중인 함수", _methodIdx, _methodsName);
            _selectedMethod = _methods[_methodIdx];
        }
        else
        {
            EditorGUILayout.LabelField("선택된 함수", _selectedMethod != null ? _selectedMethod.Name : "None");
        }

        DrawParameterField();

        if (GUI.changed)
        {
            ParameterUpdate();
            MethodUpdate();
        }
    }

    private void DrawParameterField()
    {
        if(_isChangedMethod)
            _parameterType = GetMethodType(_selectedMethod);

        if(!Application.isPlaying)
        {
            if (_parameterType == typeof(float))
                _editFloat = EditorGUILayout.FloatField("소수점 매개변수", _btnController._floatParam);
            else if (_parameterType == typeof(string))
                _editStr = EditorGUILayout.TextField("문자열 매개변수", _btnController._strParam);
            else if (_parameterType == typeof(GameObject))
                _editGameobj = (GameObject)EditorGUILayout.ObjectField("대상 오브젝트", _btnController._gameobjParam, typeof(GameObject), true);
        }
        else
        {
            if (_parameterType == typeof(float))
                EditorGUILayout.LabelField("소수점 매개변수", _btnController._floatParam.ToString());
            else if (_parameterType == typeof(string))
                EditorGUILayout.LabelField("문자열 매개변수", $"{_btnController._strParam}");
        }
    }

    private Type GetMethodType(MethodInfo m_targetMethod)
    {
        if (m_targetMethod.GetParameters().Length == 0)
            return typeof(void);

        else if (m_targetMethod.GetParameters()[0].ParameterType == typeof(float))
            return typeof(float);

        else if (m_targetMethod.GetParameters()[0].ParameterType == typeof(string))
            return typeof(string);
        
        else if (m_targetMethod.GetParameters()[0].ParameterType == typeof(GameObject))
            return typeof(GameObject);

        return null;
    }

    private void ReloadPrevMethod()
    {
        if (!string.IsNullOrEmpty(_btnController._selectedMethodName))
        {
            for (int i = 0; i < _methodsName.Length; i++)
            {
                if (_methodsName[i] == _btnController._selectedMethodName)
                {
                    _methodIdx = i;
                    _selectedMethod = _methods[_methodIdx];
                }
            }
        }
    }

    private void GetMethods(Type m_type)
    {
        _methods = m_type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
        _methodsName = new string[_methods.Length];

        for (int i = 0; i < _methods.Length; i++)
        {
            _methodsName[i] = _methods[i].Name;
        }
    }

    private void ParameterUpdate()
    {
        _btnController._floatParam = _editFloat;

        _btnController._strParam = _editStr;

        _btnController._gameobjParam = _editGameobj;
    }

    private void MethodUpdate()
    {
        _btnController._selectedMethod = _selectedMethod;
        _btnController._selectedMethodName = _selectedMethod.Name;
        
        _isChangedMethod = true;
    }
}
