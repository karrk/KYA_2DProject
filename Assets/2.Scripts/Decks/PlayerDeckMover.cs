using UnityEngine;
using DG.Tweening;
using System.Collections;
using System;

public class PlayerDeckMover : MonoBehaviour
{
    private Vector2 GravePos => Manager.Instance.Data.DeckGravePos;
    private Vector2 HandPos => Manager.Instance.Data.DeckHandPos;
    private Vector2 WaitPos => Manager.Instance.Data.DeckWaitPos;

    private DeckArranger _arranger;
    private float MoveTime => Manager.Instance.Data.DeckMoveAnimTime;

    private void Start()
    {
        _arranger = GetComponent<DeckArranger>();
    }

    public void MoveArrange(Transform m_deckTr, Vector2 m_endPos, E_TweenType m_animType = E_TweenType.None)
    {
        m_deckTr.DOMove(m_endPos, MoveTime);
    }

    public void MoveToHand(PlayerDeck m_deck, E_TweenType m_animType = E_TweenType.None)
    {
        m_deck.transform.position = WaitPos;
        m_deck.transform.DOMove(HandPos, MoveTime)
            .OnComplete(() => { _arranger.SendDeck(m_deck); });
    }

    public void MoveToGrave(PlayerDeck m_deck, E_TweenType m_animType = E_TweenType.None)
    {
        m_deck.transform.DOMove(GravePos, MoveTime)
            .OnComplete(() =>
            {
                m_deck.ReturnObj();
            });
    }
}
