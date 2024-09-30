using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeckArranger : MonoBehaviour
{
    private List<Transform> _arrangedDecks = new List<Transform>();
    private List<float> _posXs = new List<float>();

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
        _posXs.Add(0);
        Arrange();
    }

    public void ClearDecks()
    {
        _arrangedDecks.Clear();
        _posXs.Clear();
    }

    private void Arrange()
    {
        CalculatePos();
        Transform tr;

        for (int i = 0; i < _arrangedDecks.Count; i++)
        {
            tr = _arrangedDecks[i];
            _mover.MoveArrange(tr,new Vector2(_posXs[i], tr.position.y));
        }
    }

    private void CalculatePos()
    {
        float offset = (_arrangedDecks.Count - 1) * Interval / 2;

        for (int i = 0; i < _arrangedDecks.Count; i++)
        {
            _posXs[i] = i * Interval - offset;
        }
    }


}
