using System.Collections.Generic;

/// <summary>
/// �ѹ� ������ ���� �ݿ��������� ���Ǵ� ������
/// </summary>
public class st_Data
{
    #region ĳ����

    public CharacterStruct[] _characters = new CharacterStruct[(int)E_Character.Size];
    
    public void AddCharacter(E_Character m_character,CharacterStruct m_characterData)
    {
        _characters[(int)m_character] = m_characterData;
    }

    #endregion

    #region ��

    public List<DeckStruct> _decks = new List<DeckStruct>();

    public void AddDeckInfo(DeckStruct m_deck)
    {
        _decks.Add(m_deck);
    }

    #endregion

    #region Ǯ ����

    public int PoolInitCount => 5;

    #endregion
}

public struct CharacterStruct
{
    public int HP;
    public string Name;
    public List<int> StartDecks;
}

public struct DeckStruct
{
    public int ID;

    public E_DeckType Type;
    public E_DeckGrade Grade;
    public string Name;
    public int Cost;
    public int Atk;
    public int Def;
    public int AddDeckCount;
    public int Alpha;
    public int SelfAtk;
    public bool SaveDef;
    public bool Disappear;
}