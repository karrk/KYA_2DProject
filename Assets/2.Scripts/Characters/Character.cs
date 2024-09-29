using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Animations;
using UnityEngine;
using DG.Tweening;

public abstract class Character : MonoBehaviour, IListener
{
    [SerializeField] private CharacterInfo _charInfo = new CharacterInfo();
    public CharacterInfo CharacterInfo => _charInfo;
    private Animator _anim;

    protected abstract Vector2 _spawnPos { get; }
    protected abstract Vector2 _readyPos { get; }

    private E_CharacterState _state = E_CharacterState.None;
    public E_CharacterState State
    {
        set
        {
            _state = value;

            switch (value)
            {
                case E_CharacterState.Idel:
                    IdelAction();
                    break;
                case E_CharacterState.Attack:
                    AttackAction();
                    break;
                case E_CharacterState.OnDamage:
                    OnDamageAction();
                    break;
                case E_CharacterState.Dead:
                    DeadAction();
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
            Resources.Load<AnimatorController>($"Animators/{_charInfo.Name}");
    }

    protected abstract void IdelAction();

    protected abstract void AttackAction();

    protected abstract void OnDamageAction();

    protected abstract void DeadAction();

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
        MoveToReadyPos();
    }

    private void MoveToReadyPos()
    {
        _anim.SetBool("IsWalk", true);
        transform.position = _spawnPos;
        transform.DOMove(_readyPos, 4f).SetEase(Ease.Linear)
            .OnComplete(() => { _anim.SetBool("IsWalk", false); });
    }

    private void UseDeck(Character m_target,int m_deckID)
    {

    }

    private void OnDisable()
    {
        Manager.Instance.Event.RemoveListener(E_Events.ChangedBattle,this);
    }

}
