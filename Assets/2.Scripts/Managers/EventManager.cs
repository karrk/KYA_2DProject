using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

public class EventManager
{
    private Dictionary<E_Events, List<IListener>> _eventListeners;

    public EventManager()
    {
        _eventListeners = new Dictionary<E_Events, List<IListener>>();
    }

    public void AddListener(E_Events m_eventType, IListener m_listener)
    {
        if (!_eventListeners.ContainsKey(m_eventType))
            _eventListeners.Add(m_eventType, new List<IListener>());

        _eventListeners[m_eventType].Add(m_listener);
    }

    public void PlayEvent(E_Events m_eventType,Component m_order = null, object m_param = null)
    {
        if (!_eventListeners.ContainsKey(m_eventType) || _eventListeners[m_eventType].Count <= 0)
            return;

        UnityEngine.Debug.Log($"{m_eventType} 이벤트 발생");

        foreach (var listener in _eventListeners[m_eventType])
        {
            listener.OnEvent(m_eventType, m_order, m_param);
        }
    }

    public void RemoveListener(E_Events m_eventType,IListener m_listener)
    {
        _eventListeners[m_eventType].Remove(m_listener);
    }

    public void MakeEmptyEvent(E_Events m_eventType)
    {
        _eventListeners[m_eventType].Clear();
    }

    public void CleanEvent(E_Events m_eventType)
    {
        foreach (var listener in _eventListeners[m_eventType])
        {
            if (listener == null)
                _eventListeners[m_eventType].Remove(listener);
        }
    }

    public void ClearAllEvents()
    {
        foreach (var ev in _eventListeners)
        {
            ev.Value.Clear();
        }
    }
}
