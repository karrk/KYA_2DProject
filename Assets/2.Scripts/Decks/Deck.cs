using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck : MonoBehaviour, IPooledObject
{
    #region UI 요소
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _typeText;
    [SerializeField] private TextMeshProUGUI _deckDescriptionText;
    [SerializeField] private SpriteRenderer _deckImage;
    #endregion

    private int _id;
    private int _cost;
    private string _name;
    private E_DeckType _type;
    private string _description;

    public ObjectPool MyPool => throw new System.NotImplementedException();

    public GameObject MyObj => this.gameObject;

    public void SetDeckData(int m_idNumber)
    {
        DeckStruct deckData = Manager.Data.GetDeckData(m_idNumber);
        
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
        this._deckImage.sprite = Manager.Data.GetDeckImage(this._id);
    }

    private void CopyDeckData(DeckStruct m_source)
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
