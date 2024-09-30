using System.Security.Cryptography;

public class PlayerDeck : Deck
{
    public override int Cost => Manager.Instance.Data.GetPlayerDeckData(ID).Cost;
    public override string Name => Manager.Instance.Data.GetPlayerDeckData(ID).Name;
    public override int Atk => Manager.Instance.Data.GetPlayerDeckData(ID).Atk;
    public override int Def => Manager.Instance.Data.GetPlayerDeckData(ID).Def;
    public override bool IsDisappear => Manager.Instance.Data.GetPlayerDeckData(ID).Disappear;
    public override E_DeckType Type => Manager.Instance.Data.GetPlayerDeckData(ID).Type;

    public E_DeckUseType UseType => Manager.Instance.Data.GetPlayerDeckData(ID).UseType;
    public E_DeckGrade Grade => Manager.Instance.Data.GetPlayerDeckData(ID).Grade;
    public int AddDeckCount => Manager.Instance.Data.GetPlayerDeckData(ID).AddDeckCount;
    public int AlphaAtk => Manager.Instance.Data.GetPlayerDeckData(ID).AlphaAtk;
    public int SelfAtk => Manager.Instance.Data.GetPlayerDeckData(ID).SelfAtk;
    public bool SaveDef => Manager.Instance.Data.GetPlayerDeckData(ID).SaveDef;

    public string Description;

    public int SetSortOrderValue(int m_value)
    {
        _baseSprite.sortingOrder = m_value;

        _deckImage.sortingOrder = m_value++;
        _mask.frontSortingOrder = m_value;

        _canvas.sortingOrder = m_value++;


        return m_value;
    }

    public uint SetLayerMask(uint m_value)
    {
        this._mask.renderingLayerMask = m_value;
        this._deckImage.renderingLayerMask = m_value;
        return m_value << 1;
    }

    public override void SetDeckID(int m_idNumber)
    {
        base.SetDeckID(m_idNumber);
        SetUIElements();
    }

    private void SetUIElements()
    {
        _costText.text = $"{Cost}";
        _nameText.text = Name;
        _typeText.text = $"{Type}";
        //_deckDescriptionText.text = $"{}"; 
        SetDeckImage();
    }

    private void SetDeckImage()
    {
        this._deckImage.sprite = Manager.Instance.Data.GetDeckImage(this.ID);
    }
}
