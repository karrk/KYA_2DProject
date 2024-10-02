using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControll : MonoBehaviour, IListener
{
    [SerializeField] private bool _isDeckSelectMode;
    [SerializeField] private bool _isNoUseArea;
    
    private bool _hitNoUseAreaFrame;
    private bool _hitMonsterFrame;
    private bool _hitDeckFrame;

    [SerializeField] private PlayerDeck _onCursorDeck;
    [SerializeField] private PlayerDeck _selectedDeck;
    [SerializeField] private PlayerDeck _lastDeck;

    [SerializeField] private Collider2D _onCursorMob;

    private RaycastHit2D[] _hits;
    private Vector2 _mousePos;

    private bool _isDeckLogicComplete;

    private void Start()
    {
        Manager.Instance.Event.AddListener(E_Events.PlayerTurn, this);
        Manager.Instance.Event.AddListener(E_Events.PlayerTurnEnd, this);
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if(m_eventType == E_Events.PlayerTurn)
        {
            _isDeckSelectMode = true;
            _isDeckLogicComplete = false;
        }
        else if(m_eventType == E_Events.PlayerTurnEnd)
        {
            _isDeckSelectMode = false;
        }
    }

    private void Update()
    {
        if (!_isDeckSelectMode)
            return;

        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _hits = Physics2D.RaycastAll(_mousePos, Vector2.zero, 100f);

        _hitNoUseAreaFrame = false;
        _hitMonsterFrame = false;
        _hitDeckFrame = false;
        _isDeckLogicComplete = false;

        foreach (var hit in _hits)
        {
            if(hit.collider.CompareTag("NoUseArea")) 
            {
                _hitNoUseAreaFrame = true;

                if (!_isNoUseArea)
                    _isNoUseArea = true;
            }

            else if(hit.collider.CompareTag("Monster"))
            {
                _hitMonsterFrame = true;

                _onCursorMob = hit.collider;
            }

            else if(hit.collider.CompareTag("Deck"))
            {
                if(!_isDeckLogicComplete)
                {
                    _hitDeckFrame = true; // �̹� �����ӿ� �����Ǿ���

                    if (hit.collider.TryGetComponent<PlayerDeck>(out _onCursorDeck)) // �ش� ������Ʈ�� �ҷ��ͼ�.
                    {
                        if (_lastDeck != _onCursorDeck) // ���������� Ȯ���ߴ� ���� �ٸ��ٸ�
                        {
                            Manager.Instance.Data.v_data.PlayerDeckController.AvoidDecks(_onCursorDeck.ArrangeIdx); // Ʈ��
                            _lastDeck = _onCursorDeck;
                        }

                        _isDeckLogicComplete = true;
                    }
                }
            }
        }

        if (!_hitNoUseAreaFrame && _isNoUseArea)
            _isNoUseArea = false;

        if (!_hitMonsterFrame)
            _onCursorMob = null;

        if (!_hitDeckFrame) // �̹������ӿ� �� �浹�� �����ٸ�
        {
            _onCursorDeck = null; // Ŀ���� �ִ� ���� ���ְ�,
            
            if(_lastDeck != null) // ���� ���������� ��ϵǾ��� ���� �ִٸ�
            {
                Manager.Instance.Data.v_data.PlayerDeckController.ReArrange(); // �� ���� ���� ������
                _lastDeck.SetFocusOutSort();
                _lastDeck = null;
            }
        }
            

        if(Input.GetMouseButtonDown(0))
        {
            if (_onCursorDeck == null)
                return;

            _selectedDeck = _onCursorDeck.GetComponent<PlayerDeck>();
        }
        else if(Input.GetMouseButtonUp(0) && ! _isNoUseArea && _selectedDeck != null)
        {
            if(_selectedDeck.UseType == E_DeckUseType.Targetting && _onCursorMob)
            {
                if(_onCursorMob.TryGetComponent<Monster>(out Monster target))
                {
                    target.ApplyDeck(_selectedDeck);
                }
                    
            }
            else if(_selectedDeck.UseType == E_DeckUseType.NonTarget) // ��Ÿ��
            {
                Monster[] mobs = Manager.Instance.Data.v_data.MonstersDatas;

                for (int i = 1; i < mobs.Length; i++)
                {
                    mobs[i].ApplyDeck(_selectedDeck);
                }
            }
            else if(_selectedDeck.UseType == E_DeckUseType.Self)
            {
                Manager.Instance.Data.v_data.CurrentCharacter.ApplyDeck(_selectedDeck);
            }

            _selectedDeck = null;
        }
    }


}
