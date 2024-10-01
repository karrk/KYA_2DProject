using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    public int MobID => _charInfo.ID;

    protected override Vector2 SpawnPos => _spawnPos;
    protected override Vector2 ReadyPos => _readyPos;

    private Vector2 _spawnPos;
    private Vector2 _readyPos;

    public void MobInitialize(int m_mobId)
    {
        _charInfo = new MobInfo();
        MonsterStruct data = Manager.Instance.Data.GetMobData(m_mobId);
        (_charInfo as MobInfo).CopyMonsterInfo(data);

        base.Initialilze();
    }

    public void MobReady()
    {
        base.ConnectAnimatorController();
        base.MoveToReadyPos();
    }

    public void SetSpawnPos( Vector2 m_pos) { this._spawnPos = m_pos; }
    public void SetReadyPos( Vector2 m_pos) { this._readyPos = m_pos; }

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

    public override void ApplyDeck(PlayerDeck m_deck)
    {
        // 플레이어 => 몬스터 효과
        Debug.Log($"{m_deck.Name} 사용");

        Destroy(this.gameObject);
    }

    public override void ApplyDeck(MobDeck m_deck)
    {
        // 몬스터 => 몬스터 효과
    }
}
