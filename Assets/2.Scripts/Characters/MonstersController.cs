using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersController : MonoBehaviour, IListener, IWaiter
{
    // depth에 맞는 랜덤스폰

    private Monster[] _monsters;
    public int MonsterCount => _monsters.Length - 1;
    private WaitForSeconds _actionIntervalTime;

    private void Start()
    {
        Manager.Instance.Event.AddListener(E_Events.ChangedBattleScene,this);
        Manager.Instance.Event.AddListener(E_Events.EnemyTurn, this);
        Manager.Instance.Data.v_data.CurrentMobsController = this;

        Manager.Instance.Wait.AddWaiter(E_Events.ChangedBattleScene, this);

        _actionIntervalTime = new WaitForSeconds(Manager.Instance.Data.MonsterActionInterval);
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if (m_eventType == E_Events.ChangedBattleScene)
            InitMonsters();

        else if (m_eventType == E_Events.EnemyTurn)
            StartCoroutine(StepStartPattern());
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
        Manager.Instance.Data.v_data.MonstersDatas = new Monster[mobCount +1 ];
        this._monsters = Manager.Instance.Data.v_data.MonstersDatas;

        GetMonsters(mobCount);
        RegistRandomMob(0, 3);

        SendFinishSign(E_Events.ChangedBattleScene);
    }

    private void RegistRandomMob(int m_minRange, int m_maxRange)
    {
        for (int i = 1; i < _monsters.Length; i++)
        {
            _monsters[i].MobInitialize(Random.Range(m_minRange, m_maxRange));
        }
    }

    private void GetMonsters(int m_mobCount)
    {
        for (int i = 0; i < m_mobCount; i++)
        {
            GameObject mob = transform.GetChild(i).gameObject;
            mob.gameObject.SetActive(true);
            Manager.Instance.Data.v_data.MonstersDatas[i + 1] = mob.GetComponent<Monster>();
        }
    }

    private IEnumerator StepStartPattern()
    {
        int currentRound = Manager.Instance.Data.CurrentRound;

        for (int i = 1; i < _monsters.Length; i++)
        {
            int currentMobID = _monsters[i].MobID;
            int deckID = Manager.Instance.Data.GetMobData(currentMobID).Patterns[currentRound];

            DeckJudge.UseDeck(_monsters[i], deckID);

            Debug.Log("뭔가 활동함");

            yield return _actionIntervalTime;
        }

        yield return _actionIntervalTime;

        Manager.Instance.Event.PlayEvent(E_Events.EnmyTurnEnd);
        Manager.Instance.Event.PlayEvent(E_Events.PlayerTurn);
    }

    private void OnDisable()
    {
        Manager.Instance.Event.RemoveListener(E_Events.ChangedBattleScene, this);
    }

    public void StartNextAction(E_Events m_prevEvent)
    {
        ReadyMonsters();
    }

    public void SendFinishSign(E_Events m_finEvent)
    {
        Manager.Instance.Wait.SendFinishSign(m_finEvent);
    }
}
