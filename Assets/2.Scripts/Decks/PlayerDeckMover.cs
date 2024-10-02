using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerDeckMover : MonoBehaviour
{
    private Vector2 GravePos => Manager.Instance.Data.DeckGravePos;
    private Vector2 HandPos => Manager.Instance.Data.DeckHandPos;
    private Vector2 WaitPos => Manager.Instance.Data.DeckWaitPos;

    private DeckArranger _arranger;
    private float MoveTime => Manager.Instance.Data.DeckMoveAnimTime;

    private Tween[] _tweens = new Tween[10];
    private int _tweenIdx;

    private void Start()
    {
        _arranger = GetComponent<DeckArranger>();
    }

    public void ResetTweenIdx()
    {
        this._tweenIdx = 0;
    }

    public void AssignTweenIdx(PlayerDeck m_deck)
    {
        m_deck.SetUsedTweenIdx(_tweenIdx++);
    }

    private void StopPrevWork(int m_idx)
    {
        if (_tweens[m_idx] != null && _tweens[m_idx].IsActive())
            _tweens[m_idx].Kill();
    }

    public void MoveToHand(PlayerDeck m_deck, E_TweenType m_animType = E_TweenType.None)
    {
        m_deck.transform.position = WaitPos;
        m_deck.transform.DOMove(HandPos, MoveTime)
            .OnComplete(() => { 
                _arranger.SendDeck(m_deck);
                m_deck.SetCollider(true);
            });
    }

    public void MoveToGrave(PlayerDeck m_deck, E_TweenType m_animType = E_TweenType.None)
    {
        m_deck.transform.DOMove(GravePos, MoveTime)
            .OnComplete(() =>
            {
                m_deck.ReturnObj();
            });
    }

    public void AvoidMove(PlayerDeck m_deck, float m_dist)
    {
        StopPrevWork(m_deck.UsedTweenIdx);

        _tweens[m_deck.UsedTweenIdx] = 
            m_deck.transform.DOMove(new Vector2(_arranger.GetPosX(m_deck.ArrangeIdx) + m_dist, HandPos.y), Manager.Instance.Data.DeckAvoidSpeed)
            .SetRecyclable(true).SetAutoKill(false);
    }

    public void FocusMove(PlayerDeck m_deck, float m_dist)
    {
        StopPrevWork(m_deck.UsedTweenIdx);

        _tweens[m_deck.UsedTweenIdx] =
            m_deck.transform.DOMove(new Vector2(_arranger.GetPosX(m_deck.ArrangeIdx), HandPos.y + m_dist), Manager.Instance.Data.DeckAvoidSpeed)
            .SetRecyclable(true).SetAutoKill(false);
    }

    public void MoveArrange(PlayerDeck m_deck, Vector2 m_endPos, E_TweenType m_animType = E_TweenType.None)
    {
        StopPrevWork(m_deck.UsedTweenIdx);

        // 기존 트윈이 동작중이라면, 해당 동작의 좌표에서 아래 동작으로 bind한다?

        _tweens[m_deck.UsedTweenIdx] =
            m_deck.transform.DOMove(m_endPos, MoveTime)
            .SetRecyclable(true).SetAutoKill(false);
    }

    public void ReArrangeMove()
    {
        _arranger.Arrange();
    }
}
