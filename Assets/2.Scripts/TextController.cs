using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour, IBinder
{
    [HideInInspector] public string DBpropertyName;
    [HideInInspector] public int DBpropertyIdx = -1;
    private TextMeshProUGUI _tmp;

    private static List<TextController> _controllers = new List<TextController>();

    public static void Bind()
    {
        for (int i = 0; i < _controllers.Count; i++)
        {
            if (_controllers[i] == null || _controllers[i].gameObject == null)
                continue;

            _controllers[i].DataBinding();
        }
    }

    private void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _controllers.Add(this);
    }

    private void DataBinding()
    {
        PropertyInfo[] properties = typeof(TextPakage).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.Name == DBpropertyName)
            {
                Type generic = property.PropertyType.GetGenericArguments()[0];

                if (generic == typeof(int))
                {
                    var value = property.GetValue(TextPakage.Instance) as DBValue<int>;
                    _tmp.text = $"{value.Value}";
                    value.AddBinder(this);
                }
                else if (generic == typeof(string))
                {
                    var value = property.GetValue(TextPakage.Instance) as DBValue<string>;
                    _tmp.text = $"{value.Value}";
                    value.AddBinder(this);
                }
            }
        }
    }

    public void OnChangedValue(object m_param)
    {
        this._tmp.text = m_param.ToString();
    }
}


