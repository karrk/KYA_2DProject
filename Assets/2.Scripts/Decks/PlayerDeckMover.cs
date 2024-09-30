using UnityEngine;
using DG.Tweening;

public class PlayerDeckMover : MonoBehaviour
{
    private float MoveTime => Manager.Instance.Data.DeckMoveAnimTime;

    public void MoveToPos(Transform m_targetTr,Vector2 m_startPos,Vector2 m_endPos, E_TweenType m_animType = E_TweenType.None)
    {
        m_targetTr.transform.position = m_startPos;
        m_targetTr.DOMove(m_endPos, MoveTime);
    }
}
