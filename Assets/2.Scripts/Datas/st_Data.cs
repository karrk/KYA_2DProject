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

    #region ����

    public List<MonsterStruct> Monsters = new List<MonsterStruct>();

    public void AddMob(MonsterStruct m_mobData)
    {
        Monsters.Add(m_mobData);
    }

    #endregion

    #region ��

    public List<PlayerDeckStruct> PlayerDecks = new List<PlayerDeckStruct>();

    public void AddDeckInfo(PlayerDeckStruct m_deck) { PlayerDecks.Add(m_deck); }

    public List<MobDeckStruct> MobDecks = new List<MobDeckStruct>();

    public void AddDeckInfo(MobDeckStruct m_deck) { MobDecks.Add(m_deck); }

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

