using System.Collections.Generic;

/// <summary>
/// 한번 설정된 값은 반영구적으로 사용되는 데이터
/// </summary>
public class st_Data
{
    #region 캐릭터

    public CharacterStruct[] _characters = new CharacterStruct[(int)E_Character.Size];
    
    public void AddCharacter(E_Character m_character,CharacterStruct m_characterData)
    {
        _characters[(int)m_character] = m_characterData;
    }

    #endregion

    #region 덱

    public List<DeckStruct> _decks = new List<DeckStruct>();

    public void AddDeckInfo(DeckStruct m_deck)
    {
        _decks.Add(m_deck);
    }

    #endregion

    #region 풀 관련

    public int PoolInitCount => 5;

    #endregion
}

