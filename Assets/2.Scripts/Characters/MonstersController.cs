using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersController : MonoBehaviour, IListener
{
    // depth에 맞는 랜덤스폰

    private Monster[] _monsters;
    private int _maxMonsterIdx;

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if (m_eventType == E_Events.ChangedBattle)
            GetMonstersComponents();
    }

    private void GetMonstersComponents()
    {
        _monsters = GetComponentsInChildren<Monster>();
    }

    private void ActiveMonsters()
    {

    }
}
