using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Character : MonoBehaviour
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
        ConnectAnimatorController();
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
}
