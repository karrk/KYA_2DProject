using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BtnController : MonoBehaviour
{
    [HideInInspector] public float _floatParam;
    [HideInInspector] public string _strParam;

    [HideInInspector] public string _selectedMethodName;
    [HideInInspector] public MethodInfo _selectedMethod;

    private void Start()
    {
        if (!string.IsNullOrEmpty(_selectedMethodName))
        {
            MethodInfo method = typeof(BtnFunctions)
                .GetMethod(_selectedMethodName, 
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            
            if (method != null)
            {
                _selectedMethod = method;
                AssignMethodToButton();
            }
        }
    }

    private void AssignMethodToButton()
    {
        Button btn = GetComponent<Button>();

        if (btn != null && _selectedMethod != null)
        {
            btn.onClick.RemoveAllListeners();

            UnityAction action = () =>
            {
                if (_selectedMethod.GetParameters().Length == 0)
                {
                    _selectedMethod.Invoke(this, null);
                }
                else if (_selectedMethod.GetParameters()[0].ParameterType == typeof(float))
                {
                    _selectedMethod.Invoke(this, new object[] {_floatParam} );
                }
                else if (_selectedMethod.GetParameters()[0].ParameterType == typeof(string))
                {
                    _selectedMethod.Invoke(this, new object[] { _strParam });
                }
            };

            btn.onClick.AddListener(action);
        }
    }
}
