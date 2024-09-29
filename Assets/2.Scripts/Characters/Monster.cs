using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] private int _monsterNumber;
    public int MonsterNumber => _monsterNumber;

    protected override Vector2 SpawnPos => _spawnPos;
    protected override Vector2 ReadyPos => _readyPos;

    private Vector2 _spawnPos;
    private Vector2 _readyPos;

    public void MobInitialize()
    {
        base.Initialilze();
        _charInfo = new MobInfo();
        (_charInfo as MobInfo).CopyMobData(Random.Range(0,3));
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
}
