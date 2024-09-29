using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// 한번 설정된 값은 반영구적으로 사용되는 데이터
/// </summary>
public class st_Data
{
    #region 캐릭터

    public CharacterStruct[] Characters = new CharacterStruct[(int)E_Character.Size];
    
    public void AddCharacter(E_Character m_character,CharacterStruct m_characterData)
    {
        Characters[(int)m_character] = m_characterData;
    }

    #endregion

    #region 몬스터

    public List<MonsterStruct> Monsters = new List<MonsterStruct>();

    public void AddMob(MonsterStruct m_mobData)
    {
        Monsters.Add(m_mobData);
    }

    #endregion

    #region 덱

    public List<PlayerDeckStruct> PlayerDecks = new List<PlayerDeckStruct>();

    public void AddDeckInfo(PlayerDeckStruct m_deck) { PlayerDecks.Add(m_deck); }

    public List<MobDeckStruct> MobDecks = new List<MobDeckStruct>();

    public void AddDeckInfo(MobDeckStruct m_deck) { MobDecks.Add(m_deck); }

    #endregion

    #region 풀 관련

    public int PoolInitCount => 5;

    #endregion

    #region 위치

    public PostionStruct Positions;

    #endregion

    #region 맵 데이터

    public MapStruct[] MapDataList;

    #endregion
}

