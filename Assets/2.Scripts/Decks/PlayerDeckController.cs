using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckController : MonoBehaviour, IListener
{
    private PlayerDeckCreator _creator;
    private PlayerDeckMover _mover;

    private int PullDeckCount => Manager.Instance.Data.v_data.CurrentCharacter.PullDeckCount;
    private int AP => Manager.Instance.Data.v_data.CurrentCharacter.AP.Value;

    private List<int> _graves = new List<int>();
    private List<int> _onHands = new List<int>();
    private List<int> _waitDecks = new List<int>();
    private List<PlayerDeck> _realizeDecks = new List<PlayerDeck>();

    private WaitForSeconds _deckPullSec;
    private Coroutine _WaitToHandRoutine;
    private Coroutine _HandToGraveRoutine;
    private Coroutine _GraveToWaitRoutine;

    private int _lastSortValue;
    private uint _lastLayerMask;

    private int _lastHandIdx;

    private void Start()
    {
        Manager.Instance.Event.AddListener(E_Events.BattleReady,this);
        Manager.Instance.Event.AddListener(E_Events.PlayerTurn,this);
        Manager.Instance.Event.AddListener(E_Events.PlayerTurnEnd,this);

        Manager.Instance.Data.v_data.PlayerDeckController = this;

        _creator = GetComponent<PlayerDeckCreator>();
        _mover = GetComponent<PlayerDeckMover>();

        _deckPullSec = new WaitForSeconds(Manager.Instance.Data.DeckPullCoolTime);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Manager.Instance.Event.PlayEvent(E_Events.BattleReady);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            Manager.Instance.Event.PlayEvent(E_Events.PlayerTurn);
    }

    public void OnEvent(E_Events m_eventType, System.ComponentModel.Component m_order, object m_param)
    {
        if(m_eventType == E_Events.BattleReady)
        {
            Initialize();
        }
        else if(m_eventType == E_Events.PlayerTurn)
        {
            if(_WaitToHandRoutine != null) { StopCoroutine(_WaitToHandRoutine); }

            _lastHandIdx = 0;
            _mover.ResetTweenIdx();
            _lastSortValue = Manager.Instance.Data.DeckInitSortValue;
            _lastLayerMask = Manager.Instance.Data.DeckMaskLayerNumber;
            _WaitToHandRoutine = StartCoroutine(StepWaitToHand());
            
        }
        else if(m_eventType == E_Events.PlayerTurnEnd)
        {
            _HandToGraveRoutine = StartCoroutine(StepHandToGrave());
        }
    }

    private void Initialize()
    {
        GetPlayerDeckData();
        UpdateGraveDecksCount();
        UpdateWaitDecksCount();
    }

    private void GetPlayerDeckData()
    {
        List<int> playerDecks = Manager.Instance.Data.v_data.PlayerData.TotalDecks;

        for (int i = 0; i < playerDecks.Count; i++)
        {
            _waitDecks.Add(playerDecks[i]);
        }

        SuffleListElements(_waitDecks);
    }

    private IEnumerator StepWaitToHand()
    {
        for (int i = 0; i < PullDeckCount; i++)
        {
            WaitToHand();
            yield return _deckPullSec;
        }
    }

    private IEnumerator StepHandToGrave()
    {
        for (int i = 0; i < _onHands.Count; i = 0)
        {
            HandToGrave(i);
            yield return _deckPullSec;
        }
    }

    private IEnumerator StepGraveToWait()
    {
        SuffleListElements(_graves);

        for (int i = _graves.Count-1; i >= 0; i--)
        {
            GraveToWaits(i);
            yield return _deckPullSec;
        }
    }

    /// <summary>
    /// 사용한 덱 이동
    /// </summary>
    private void HandToGrave(int m_onHandIdx)
    {
        _graves.Add(_waitDecks[m_onHandIdx]);
        _onHands.RemoveAt(m_onHandIdx);

        PlayerDeck deck = _realizeDecks[m_onHandIdx];
        _mover.MoveToGrave(deck);
        _realizeDecks.RemoveAt(m_onHandIdx);

        UpdateGraveDecksCount();
    }

    /// <summary>
    /// 대기중 덱을 Hand로 이동
    /// </summary>
    private void WaitToHand()
    {
        if(_waitDecks.Count == 0) // 대기 덱이 없다면 묘지에서 끌어오기
        {
            _GraveToWaitRoutine = StartCoroutine(StepGraveToWait());
        }

        int deckId = _waitDecks[_waitDecks.Count - 1];

        _onHands.Add(deckId);
        _waitDecks.RemoveAt(_waitDecks.Count - 1);

        PlayerDeck deck = RealizeDeck(deckId);
        deck.SetArrangeIdx(_lastHandIdx);
        _lastHandIdx++;
        _mover.AssignTweenIdx(deck);
        _mover.MoveToHand(deck);

        UpdateWaitDecksCount();
    }

    /// <summary>
    /// Grave의 덱들을 전부 대기열에 추가
    /// </summary>
    private void GraveToWaits(int m_listIdx)
    {
        _waitDecks.Add(_graves[m_listIdx]);
        _graves.RemoveAt(m_listIdx);

        UpdateWaitDecksCount();
        UpdateGraveDecksCount();
    }

    private void UpdateWaitDecksCount()
    {
        Manager.Instance.Data.v_data.WaitDecksCount.Value = _waitDecks.Count;
    }

    private void UpdateGraveDecksCount()
    {
        Manager.Instance.Data.v_data.GraveDecksCount.Value = _graves.Count;
    }

    public void AvoidDecks(int m_handIdx)
    {
        for (int i = 0; i < m_handIdx; i++)
        {
            _mover.AvoidMove(_realizeDecks[i],-1 *Manager.Instance.Data.DeckAvoidMoveDist);
            _realizeDecks[i].SetFocusOutSort();
        }

        for (int i = m_handIdx+1; i < _onHands.Count; i++)
        {
            _mover.AvoidMove(_realizeDecks[i], Manager.Instance.Data.DeckAvoidMoveDist);
            _realizeDecks[i].SetFocusOutSort();
        }

        _mover.FocusMove(_realizeDecks[m_handIdx], Manager.Instance.Data.DeckFocusDist);
        _realizeDecks[m_handIdx].SetFocusOnSort();
    }

    public void ReArrange()
    {
        _mover.ReArrangeMove();
    }

    private void UseDeck(int m_idx)
    {
        int deckId = _onHands[m_idx];
        int needCost = Manager.Instance.Data.GetPlayerDeckData(deckId).Cost;

        if(AP >= needCost)
        {
            HandToGrave(m_idx);
            //AP 소비
            //사용 액션
        }
            
        else
        {

        }

    }

    private PlayerDeck RealizeDeck(int m_id)
    {
        PlayerDeck deckObject = _creator.GetPlayerDeck(m_id);
        _lastSortValue = deckObject.SetSortOrderValue(_lastSortValue);
        _lastLayerMask = deckObject.SetLayerMask(_lastLayerMask);
        _realizeDecks.Add(deckObject);

        return deckObject;
    }

    private void SuffleListElements(List<int> m_list)
    {
        for (int i = m_list.Count-1; i > 0; i--)
        {
            int randIdx = Random.Range(0, i - 1);

            int temp = m_list[randIdx];
            m_list[randIdx] = m_list[i];
            m_list[i] = temp;
        }
    }

    private void OnDisable()
    {
        if (_WaitToHandRoutine != null)
            StopCoroutine(_WaitToHandRoutine);

        if (_HandToGraveRoutine != null)
            StopCoroutine(_HandToGraveRoutine);

        if (_GraveToWaitRoutine != null)
            StopCoroutine(_GraveToWaitRoutine);
    }
}
