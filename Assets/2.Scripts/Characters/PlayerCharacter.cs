using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character, IListener
{
    protected override Vector2 SpawnPos => Manager.Instance.Data.PlayerSpawnPos;

    protected override Vector2 ReadyPos => Manager.Instance.Data.PlayerReadyPos;

    private void Start()
    {
        _charInfo = new PlayerCharacterInfo();

        Manager.Instance.Event.AddListener(E_Events.ChangedBattleScene, this);
        Manager.Instance.Event.AddListener(E_Events.BattleReady, this);
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if (m_eventType == E_Events.ChangedBattleScene)
            Initialilze();

        else if(m_eventType == E_Events.BattleReady)
            Ready();
    }

    private void Initialilze()
    {
        base.Initialilze();
        (_charInfo as PlayerCharacterInfo).CopyCharacterData(Manager.Instance.Data.v_data.PlayerData.SelectedCharacterID);
        Manager.Instance.Event.PlayEvent(E_Events.BattleReady);
    }

    private void Ready()
    {
        base.ConnectAnimatorController();
        base.MoveToReadyPos();
    }

    protected override void AttackAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void DeadAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void IdelAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnDamageAction()
    {
        throw new System.NotImplementedException();
    }

    protected override void DisConnectEvents()
    {

    }
}
