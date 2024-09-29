using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersController : MonoBehaviour, IListener, IWaiter
{
    // depth에 맞는 랜덤스폰

    private Monster[] _monsters;
    public Monster this[int idx] { get { return _monsters[idx]; } }

    public int MonsterCount => _monsters.Length - 1;

    private void Start()
    {
        Manager.Instance.Event.AddListener(E_Events.ChangedBattleScene,this);
        Manager.Instance.Event.AddListener(E_Events.BattleReady,this);
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if (m_eventType == E_Events.ChangedBattleScene)
            InitMonsters();
        else if (m_eventType == E_Events.BattleReady)
            ReadyMonsters();

    }

    private void ReadyMonsters()
    {
        int count = _monsters.Length - 1;
        Vector2 standardSpawnPos = Manager.Instance.Data.MobSpawnPos;
        Vector2 standardReadyPos = Manager.Instance.Data.MobReadyPos;
        float interval;

        if(count == 1)
        {
            _monsters[1].SetSpawnPos(standardSpawnPos);
            _monsters[1].SetReadyPos(standardReadyPos);
        }
        else if(count == 2)
        {
            interval = 2.5f;
            _monsters[1].SetSpawnPos(standardSpawnPos - new Vector2((interval / 2), 0));
            _monsters[2].SetSpawnPos(standardSpawnPos + new Vector2((interval / 2), 0));

            _monsters[1].SetReadyPos(standardReadyPos - new Vector2((interval / 2), 0));
            _monsters[2].SetReadyPos(standardReadyPos + new Vector2((interval / 2), 0));
        }

        else if (count == 3)
        {
            interval = 1.8f;
            _monsters[1].SetSpawnPos(standardSpawnPos - new Vector2(interval, 0));
            _monsters[2].SetSpawnPos(standardSpawnPos);
            _monsters[3].SetSpawnPos(standardSpawnPos + new Vector2(interval, 0));

            _monsters[1].SetReadyPos(standardReadyPos - new Vector2(interval, 0));
            _monsters[2].SetReadyPos(standardReadyPos);
            _monsters[3].SetReadyPos(standardReadyPos + new Vector2(interval, 0));
        }

        for (int i = 1; i < _monsters.Length; i++)
        {
            _monsters[i].MobReady();
        }
    }

    private void InitMonsters()
    {
        int mobCount = Random.Range(1, 4);

        GetMonsters(mobCount);

        for (int i = 1; i < _monsters.Length; i++)
        {
            _monsters[i].MobInitialize();
        }
    }

    private void GetMonsters(int m_mobCount)
    {
        _monsters = new Monster[m_mobCount + 1];

        for (int i = 0; i < m_mobCount; i++)
        {
            GameObject mob = transform.GetChild(i).gameObject;
            mob.gameObject.SetActive(true);
            _monsters[i+1] = mob.GetComponent<Monster>();
        }
    }

    private void OnDisable()
    {
        Manager.Instance.Event.RemoveListener(E_Events.ChangedBattleScene, this);
    }

    public void StartNextAction(E_Events m_prevEvent)
    {
        throw new System.NotImplementedException();
    }

    public void SendFinishSign(E_Events m_finEvent)
    {
        throw new System.NotImplementedException();
    }
}
