using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _anim;
    
    public void SetAnimParameter(E_AnimParams m_animType, bool m_active = true)
    {
        switch (m_animType)
        {
            case E_AnimParams.Attack:
                _anim.SetTrigger("Attack");
                break;
            case E_AnimParams.OnDamage:
                _anim.SetTrigger("OnDamage");
                break;
            case E_AnimParams.IsWalk:
                _anim.SetBool("IsWalk", m_active);
                break;
            case E_AnimParams.IsDead:
                _anim.SetBool("IsDead", m_active);
                break;
            default:
                break;
        }
    }

    public void ConnectAnimator(string m_controllerName)
    {
        this._anim = GetComponent<Animator>();

        this._anim.runtimeAnimatorController =
            Resources.Load<RuntimeAnimatorController>($"Animators/{m_controllerName}");
    }
}
