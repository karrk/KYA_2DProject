using System.Collections.Generic;

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

public struct PlayerStruct
{
    public string NickName;
    public E_PlayerGrade Grade;
    public Character Char;
    public int Gold;
    public List<int> Decks;
}

public struct VolumeStruct
{
    public float BGM_Volume;
    public float SFX_Volume;
}