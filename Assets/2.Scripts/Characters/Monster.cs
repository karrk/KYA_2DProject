using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    [SerializeField] private int _monsterNumber;
    public int MonsterNumber => _monsterNumber;

    protected override Vector2 _spawnPos => throw new System.NotImplementedException();

    protected override Vector2 _readyPos => throw new System.NotImplementedException();

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
