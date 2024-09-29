public enum E_CSVTableType
{
    Character,
    PlayerDeck,
    Map,
    Mob,
    MobDeck,
    Size,
}

#region ĳ�������̺�
public enum E_Character
{
    Ironclad,
    Silent,
    Size,
}

public enum E_CharacterStats
{
    ID,
    Name,
    HP,
    StartDeck,
}
#endregion

#region �� ���̺�

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

public enum E_DeckUseType
{
    NonTarget,
    Targetting,
}

public enum E_PlayerDeckInfo
{
    ID,
    Type,
    Grade,
    Name,
    Cost,
    UseType,
    Atk,
    Def,
    AddDeckCount,
    Alpha,
    SelfAtk,
    SaveDef,
    Disappear,
    Heal,
}

public enum E_MobDeckInfo
{
    ID,
    Type,
    Name,
    Cost,
    UseType,
    Atk,
    Def,
    NextTurnPlusPower,
    DecreasePowerPercent,
    PushDeck,
    N_TurnAction,
    SteelGold,
    Runaway,
    Disappear,
    Heal,
}

#endregion

#region �� ���̺�
public enum E_RoomType
{
    None,
    B, // ����
    N, // ��ָ�
    E, // ����Ʈ��
    R, // �޽�
    U, // ����
    S, // ����
    C, // ����
}
#endregion

#region ���� ���̺�

public enum E_MonsterInfo
{
    ID,
    Grade,
    Name,
    Level,
    HP,
    StartDeck,
}

public enum E_MonsterGrade
{
    None,
    Normal,
    Elite,
    Boss,
}

#endregion