using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckController : MonoBehaviour, IListener
{
    private PlayerDeckCreator _creator;
    private PlayerDeckMover _mover;

    private Vector2 GravePos => Manager.Instance.Data.DeckGravePos;
    private Vector2 HandPos => Manager.Instance.Data.DeckHandPos;
    private Vector2 WaitPos => Manager.Instance.Data.DeckWaitPos;

    private int PullDeckCount => Manager.Instance.Data.v_data.CurrentCharacter.CharacterInfo.PullDeckCount;
    private int AP => Manager.Instance.Data.v_data.CurrentCharacter.CharacterInfo.AP;

    private List<int> _graves = new List<int>();
    private List<int> _onHands = new List<int>();
    private List<int> _waitDecks = new List<int>();

    private void Start()
    {
        Manager.Instance.Event.AddListener(E_Events.BattleReady,this);
        Manager.Instance.Event.AddListener(E_Events.PlayerTurn,this);
        Manager.Instance.Event.AddListener(E_Events.PlayerTurnEnd,this);

        _creator = GetComponent<PlayerDeckCreator>();
        _mover = GetComponent<PlayerDeckMover>();
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
            SetupPlayerDeck();
        }
        else if(m_eventType == E_Events.PlayerTurnEnd)
        {
            ReturnAllHands();
        }
    }

    private void Initialize()
    {
        GetPlayerDeckData();
    }

    private void GetPlayerDeckData()
    {
        List<int> playerDecks = Manager.Instance.Data.v_data.PlayerData.Decks;

        for (int i = 0; i < playerDecks.Count; i++)
        {
            _waitDecks.Add(playerDecks[i]);
        }
    }

    private void SetupPlayerDeck()
    {
        for (int i = 0; i < PullDeckCount; i++)
        {
            WaitToHand();
        }
    }

    /// <summary>
    /// Hand에 있는 모든 덱 => Grave
    /// </summary>
    private void ReturnAllHands()
    {
        for (int i = _onHands.Count-1; i >= 0; i--)
        {
            HandToGrave(i);
        }
    }

    /// <summary>
    /// 사용한 덱 이동
    /// </summary>
    private void HandToGrave(int m_onHandIdx)
    {
        _graves.Add(_waitDecks[m_onHandIdx]);
        _onHands.RemoveAt(m_onHandIdx);
    }

    /// <summary>
    /// 대기중 덱을 Hand로 이동
    /// </summary>
    private void WaitToHand()
    {
        int deckId = _waitDecks[_waitDecks.Count - 1];

        _onHands.Add(deckId);
        _waitDecks.RemoveAt(_waitDecks.Count - 1);

        Deck deckObject = _creator.GetDeck(deckId);
        _mover.MoveToPos(deckObject.transform, WaitPos, HandPos);
    }

    /// <summary>
    /// Grave의 덱들을 전부 대기열에 추가
    /// </summary>
    private void GraveToWaits()
    {
        for (int i = _graves.Count-1; i >= 0 ; i--)
        {
            _waitDecks.Add(_graves[i]);
            _graves.RemoveAt(i);
        }
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

    private void SuffleWaits()
    {
        for (int i = _waitDecks.Count-1; i > 0; i--)
        {
            int randIdx = Random.Range(0, i - 1);

            int temp = _waitDecks[randIdx];
            _waitDecks[randIdx] = _waitDecks[i];
            _waitDecks[i] = temp;
        }
    }
}
