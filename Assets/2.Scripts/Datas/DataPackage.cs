using System.Collections.Generic;
using UnityEngine;

public struct CharacterStruct
{
    public int ID;
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
    public E_DeckUseType UseType;
}

public struct PlayerStruct
{
    public string NickName;
    public E_PlayerGrade Grade;
    public int SelectedCharacterID;
    public int TotalGold;
    public int RoundGold;
    public List<int> Decks;
}

public struct MonsterStruct
{
    public int ID;
    public E_MonsterGrade Grade;
    public string Name;
    public int Level;
    public int HP;
    public List<int> StartDecks;
}

public struct VolumeStruct
{
    public float BGM_Volume;
    public float SFX_Volume;
}

public struct PostionStruct
{
    public Vector2 PlayerSpawnPos;
    public Vector2 PlayerReadyPos;
    public Vector2 MobSpawnPos;
    public Vector2 MobReadyPos;
}

public class MapStruct // ����
{
    public E_RoomType[,] RoomTypes;
    public string[,] NextPaths;
}