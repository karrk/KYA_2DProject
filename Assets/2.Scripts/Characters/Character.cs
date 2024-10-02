using UnityEditor.Animations;
using UnityEngine;
using DG.Tweening;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterInfo _charInfo;
    private Animator _anim;

    protected abstract Vector2 SpawnPos { get; }
    protected abstract Vector2 ReadyPos { get; }

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

    private DBValue<string> _name = new DBValue<string>();
    public DBValue<string> Name => _name;

    private DBValue<int> _hp = new DBValue<int>();
    public DBValue<int> HP => _hp;

    private DBValue<int> _ap = new DBValue<int>();
    public DBValue<int> AP => _ap;

    protected virtual void Initialilze()
    {
        _anim = GetComponent<Animator>();
        InitHP();
        InitAP();
        SetName();
    }

    protected void ConnectAnimatorController()
    {
        _anim.runtimeAnimatorController =
            Resources.Load<AnimatorController>($"Animators/{_charInfo.Name}");
    }

    protected abstract void IdelAction();

    protected abstract void AttackAction();

    protected abstract void OnDamageAction();

    protected abstract void DeadAction();

    protected void MoveToReadyPos()
    {
        _anim.SetBool("IsWalk", true);
        transform.position = SpawnPos;
        transform.DOMove(ReadyPos, 4f).SetEase(Ease.Linear)
            .OnComplete(() => { _anim.SetBool("IsWalk", false); });
    }

    protected void InitHP()
    {
        this._hp.Value = _charInfo.HP;
    }

    public void AddHP(int m_value)
    {
        this._hp.Value += m_value;
    }

    protected void InitAP()
    {
        this._ap.Value = _charInfo.AP;
    }

    protected void AddAP(int m_value)
    {
        this._ap.Value += m_value;
    }

    protected void SetName()
    {
        this._name.Value = _charInfo.Name;
    }

    public abstract void ApplyDeck(PlayerDeck m_deck);
    public abstract void ApplyDeck(MobDeck m_deck);
}
