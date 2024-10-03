using UnityEngine;

public class PlayerCharacter : Character, IListener, IWaiter
{
    public int PullDeckCount => (_charInfo as PlayerCharacterInfo).PullDecksCount;

    protected override Vector2 SpawnPos => Manager.Instance.Data.PlayerSpawnPos;
    protected override Vector2 ReadyPos => Manager.Instance.Data.PlayerReadyPos;

    private void Start()
    {
        _charInfo = new PlayerCharacterInfo();

        Manager.Instance.Event.AddListener(E_Events.ChangedBattleScene, this);

        Manager.Instance.Wait.AddWaiter(E_Events.ChangedBattleScene, this);
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if (m_eventType == E_Events.ChangedBattleScene)
            Initialilze();

    }

    protected override void Initialilze()
    {
        base.Initialilze();
        Manager.Instance.Data.v_data.CurrentCharacter = this;

        SendFinishSign(E_Events.ChangedBattleScene);
    }

    private void Ready()
    {
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

    protected override void ReadyCompleteAction()
    {
        Manager.Instance.Event.PlayEvent(E_Events.PlayerTurn);
    }

    public void StartNextAction(E_Events m_prevEvent)
    {
        if(m_prevEvent == E_Events.ChangedBattleScene)
            Ready();
    }

    public void SendFinishSign(E_Events m_finEvent)
    {
        Manager.Instance.Wait.SendFinishSign(m_finEvent);
    }

    public override void ApplyDeck(PlayerDeck m_deck)
    {
        // 플레이어 => 플레이어 자기 자신에게

    }

    public override void ApplyDeck(MobDeck m_deck)
    {
        // 몬스터 => 플레이어 몬스터 덱의 영향

    }

    
}
