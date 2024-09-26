public enum E_CSVTableType
{
    Character = 0,
    Deck = 2,
}

#region 캐릭터테이블
public enum E_Character
{
    Ironclad,
    Silent,
    Size,
}

public enum E_CharacterStats
{
    Name,
    HP,
    StartDeck,
}
#endregion

#region 덱 테이블

public enum E_DeckType
{
    Attack = 1,
    Skill,
    Power,
}

public enum E_DeckGrade
{
    Common = 1,
    Spacial,
    Rare,
}

public enum E_DeckInfo
{
    ID,
    Type,
    Grade,
    Name,
    Cost,
    Atk,
    Def,
    AddDeckCount,
    Alpha,
    SelfAtk,
    SaveDef,
    Disappear,
}

#endregion