using UnityEngine;
using DG.Tweening;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterInfo _charInfo;
    private CharacterAnimator _anim;

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
                case E_CharacterState.ReadyComplete:
                    ReadyCompleteAction();
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

    private DBValue<int> _def = new DBValue<int>();
    public DBValue<int> Def => _def;

    private DBValue<int> _alphaPower = new DBValue<int>();
    public DBValue<int> AlphaPower => _alphaPower;

    private DBValue<int> _decreasePowerTurns = new DBValue<int>();
    public DBValue<int> DecreasePowerTurns => _decreasePowerTurns;

    private bool _saveDef = false;
    public bool SaveDef => _saveDef;

    protected virtual void Initialilze()
    {
        _anim = GetComponent<CharacterAnimator>();

        InitHP();
        InitAP();
        SetName();

        _anim.ConnectAnimator(this.Name.Value);
    }

    protected abstract void IdelAction();

    protected abstract void AttackAction();

    protected abstract void OnDamageAction();

    protected abstract void DeadAction();

    protected abstract void ReadyCompleteAction();

    protected void MoveToReadyPos()
    {
        _anim.SetAnimParameter(E_AnimParams.IsWalk);

        transform.position = SpawnPos;
        transform.DOMove(ReadyPos, 4f).SetEase(Ease.Linear)
            .OnComplete(() => 
            {
                _anim.SetAnimParameter(E_AnimParams.IsWalk, false);
                State = E_CharacterState.ReadyComplete;
            });
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

    public void AddAP(int m_value)
    {
        this._ap.Value += m_value;
    }

    protected void SetName()
    {
        this._name.Value = _charInfo.Name;
    }

    protected void InitDef()
    {
        this._def.Value = 0;
    }

    public void AddDef(int m_value)
    {
        this._def.Value += m_value;
    }

    protected void InitAlphaAtk()
    {
        this._alphaPower.Value = 0;
    }

    public void AddAlphaAtk(int m_value)
    {
        this._alphaPower.Value += m_value;
    }

    protected void InitSaveDef()
    {
        this._saveDef = false;
    }

    public void SetSaveDef(bool m_active)
    {
        this._saveDef = m_active;
    }

    protected void InitDecreasePowerTurns()
    {
        this.DecreasePowerTurns.Value = 0;
    }

    public void AddDecreasePowerTurns(int m_value)
    {
        this.DecreasePowerTurns.Value += m_value;
    }
}
