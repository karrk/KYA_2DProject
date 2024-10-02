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

    public void SendDeck(PlayerDeck m_deck)
    {
        _arrangedDecks.Add(m_deck.transform);
        _posXs.Add(0);
        Arrange();
    }

    public void ClearDecks()
    {
        _arrangedDecks.Clear();
        _posXs.Clear();
    }

    public void Arrange()
    {
        CalculatePos();
        Transform tr;

        for (int i = 0; i < _arrangedDecks.Count; i++)
        {
            tr = _arrangedDecks[i];
            _mover.MoveArrange(tr.GetComponent<PlayerDeck>(),
                new Vector2(_posXs[i],Manager.Instance.Data.DeckHandPos.y));
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

    public float GetPosX(int m_idx)
    {
        return _posXs[m_idx];
    }
}
