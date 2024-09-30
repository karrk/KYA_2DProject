using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Deck : MonoBehaviour, IPooledObject
{
    #region UI ¿ä¼Ò
    [SerializeField] protected SpriteRenderer _baseSprite;
    [SerializeField] protected TextMeshProUGUI _costText;
    [SerializeField] protected TextMeshProUGUI _nameText;
    [SerializeField] protected TextMeshProUGUI _typeText;
    [SerializeField] protected TextMeshProUGUI _deckDescriptionText;
    [SerializeField] protected SpriteRenderer _deckImage;
    [SerializeField] protected SpriteMask _mask;
    [SerializeField] protected Canvas _canvas;
    #endregion

    private int _id;
    public int ID => _id;

    public abstract int Cost { get; }
    public abstract string Name { get; }
    public abstract int Atk { get; }
    public abstract int Def { get; }
    public abstract bool IsDisappear { get; }
    public abstract E_DeckType Type { get; }

    public ObjectPool MyPool => Manager.Instance.Pool.GetPool(E_PoolType.Deck);

    public GameObject MyObj => this.gameObject;

    public virtual void SetDeckID(int m_idNumber)
    {
        this._id = m_idNumber;
    }

    public void ReturnObj()
    {
        throw new System.NotImplementedException();
    }
}
