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