using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck : MonoBehaviour, IPooledObject
{
    #region UI 요소
    [SerializeField] private SpriteRenderer _baseSprite;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _deckDescriptionText;
    [SerializeField] private SpriteRenderer _deckImage;
    [SerializeField] private SpriteMask _mask;
    [SerializeField] private Canvas _canvas;
    #endregion

    private int _id;
    private int _cost;
    private string _name;
    private E_DeckType _type;
    private string _description;
    private bool _isDisappeared;

    public ObjectPool MyPool => Manager.Instance.Pool.GetPool(E_PoolType.Deck);

    public GameObject MyObj => this.gameObject;

    public int SetSortOrderValue(int m_value)
    {
        _baseSprite.sortingOrder = m_value;
        
        _deckImage.sortingOrder = m_value ++;
        _mask.frontSortingOrder = m_value;

        _canvas.sortingOrder = m_value++;
        

        return m_value;
    }

    public uint SetLayerMask(uint m_value)
    {
        this._mask.renderingLayerMask = m_value;
        this._deckImage.renderingLayerMask = m_value;
        return m_value<<1;
    }

    public void SetDeckData(int m_idNumber)
    {
        PlayerDeckStruct deckData = Manager.Instance.Data.GetPlayerDeckData(m_idNumber);
        
        this._id = m_idNumber;
        CopyDeckData(deckData);
        SetUIElements();
    }

    private void SetUIElements()
    {
        _costText.text = $"{_cost}";
        _nameText.text = _name;
        _typeText.text = $"{_type}";
        //_deckDescriptionText.text = $"{}"; 
        SetDeckImage();
    }

    private void SetDeckImage()
    {
        this._deckImage.sprite = Manager.Instance.Data.GetDeckImage(this._id);
    }

    private void CopyDeckData(PlayerDeckStruct m_source)
    {
        this._cost = m_source.Cost;
        this._name = m_source.Name;
        this._type = m_source.Type;
        //this._description = 내용설명 csv 추가 요망
    }

    public void ReturnObj()
    {
        throw new System.NotImplementedException();
    }
}
