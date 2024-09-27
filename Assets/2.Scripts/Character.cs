using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Animations;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour, IListener
{
    [SerializeField] private string _name;
    [SerializeField] private int _hp;
    private Animator _anim;

    private E_CharacterState _state = E_CharacterState.None;
    public E_CharacterState State
    {
        set
        {
            _state = value;

            switch (value)
            {
                case E_CharacterState.Idel:
                    break;
                case E_CharacterState.Attack:
                    break;
                case E_CharacterState.OnDamage:
                    break;
                case E_CharacterState.Dead:
                    break;
            }
        }
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        Manager.Instance.Event.AddListener(E_Events.ChangedBattle, this);
    }

    private void ConnectAnimatorController()
    {
        _anim.runtimeAnimatorController =
            Resources.Load<AnimatorController>($"Animators/{_name}");
    }

    private void IdelAction()
    {

    }

    private void AttackAction()
    {

    }

    private void OnDamageAction()
    {

    }

    private void DeadAction()
    {

    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if(m_eventType == E_Events.ChangedBattle)
        {
            BattleReady();
        }
    }

    private void BattleReady()
    {
        ConnectAnimatorController();
        Move();
    }

    private void Move()
    {
        transform.position = Manager.Instance.Data.PlayerSpawnPos;
        transform.DOMove(Manager.Instance.Data.PlayerReadyPos, 2f);
    }

}
