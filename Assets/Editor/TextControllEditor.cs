
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextController))]
public class TextControllEditor : Editor
{
    private TextController _controller;
    private int _selectedIdx;
    private string _selectedText;
    private List<PropertyInfo> _DBProperties;
    private string[] _propertiesNames;

    private void OnEnable()
    {
        _controller = (TextController)target;
        _DBProperties = new List<PropertyInfo>();

        GetDBProperties();
        CopyPropertiesName();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (!Application.isPlaying)
        {
            if (_controller.DBpropertyIdx != -1)
                _selectedIdx = _controller.DBpropertyIdx;

            _selectedIdx = EditorGUILayout.Popup("선택한 데이터", _selectedIdx, _propertiesNames);
        }
        else
        {
            EditorGUILayout.LabelField("선택한 데이터", $"{_controller.DBpropertyName}");
        }

        if(GUI.changed)
        {
            _controller.DBpropertyName = _propertiesNames[_selectedIdx];
            _controller.DBpropertyIdx = _selectedIdx;
        }
    }

    private void GetDBProperties()
    {
        PropertyInfo[] properties = typeof(TextPakage).
        GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.PropertyType.GetGenericTypeDefinition() == typeof(DBValue<>))
            {
                _DBProperties.Add(property);
            }
        }
    }

    private void CopyPropertiesName()
    {
        _propertiesNames = new string[_DBProperties.Count];

        for (int i = 0; i < _propertiesNames.Length; i++)
        {
            _propertiesNames[i] = _DBProperties[i].Name;
        }
    }

}
