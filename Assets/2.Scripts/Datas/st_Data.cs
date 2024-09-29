using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// �ѹ� ������ ���� �ݿ��������� ���Ǵ� ������
/// </summary>
public class st_Data
{
    #region ĳ����

    public CharacterStruct[] Characters = new CharacterStruct[(int)E_Character.Size];
    
    public void AddCharacter(E_Character m_character,CharacterStruct m_characterData)
    {
        Characters[(int)m_character] = m_characterData;
    }

    #endregion

    #region ��

    public List<DeckStruct> Decks = new List<DeckStruct>();

    public void AddDeckInfo(DeckStruct m_deck)
    {
        Decks.Add(m_deck);
    }

    #endregion

    #region Ǯ ����

    public int PoolInitCount => 5;

    #endregion

    #region ��ġ

    public PostionStruct Positions;

    #endregion

    #region �� ������

    public MapStruct[] MapDataList;

    #endregion
}

