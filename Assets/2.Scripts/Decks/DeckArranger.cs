using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeckArranger : MonoBehaviour
{
    private List<Transform> _arrangedDecks = new List<Transform>();
    private Vector2 HandPos => Manager.Instance.Data.DeckHandPos;
    private float Interval => Manager.Instance.Data.DeckWidth;

    private PlayerDeckMover _mover;

    private void Start()
    {
        _mover = GetComponent<PlayerDeckMover>();
    }

    public void SendDeck(Transform m_deck)
    {
        _arrangedDecks.Add(m_deck);
        Arrange();
    }

    public void ClearDecks()
    {

    }

    private void Arrange()
    {
        int deckCount = _arrangedDecks.Count;
        Transform tr;
        float moveDist = Interval / 2;

        if (_arrangedDecks.Count == 1)
        {
            tr = _arrangedDecks[0];
            _mover.MoveArrange(tr, tr.position, HandPos);
        }
        else
        {
            for (int i = 0; i < _arrangedDecks.Count-1; i++)
            {
                tr = _arrangedDecks[i];
                _mover.MoveArrange(tr, tr.position, new Vector2(tr.position.x - moveDist, tr.position.y));
            }

            tr = _arrangedDecks[_arrangedDecks.Count - 1];
            _mover.MoveArrange(tr, tr.position, new Vector2(
                 _arrangedDecks[_arrangedDecks.Count - 2].position.x + Interval, tr.position.y));
        }
    }


}
