using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    protected int _id;

    [SerializeField] protected string _name;
    [SerializeField] protected int _hp;
    [SerializeField] protected int _pullDeckCount = 3;
    [SerializeField] protected int _abilityPoint;
    protected Dictionary<int, int> _deckInventory = new Dictionary<int, int>();
    

    public string Name => _name;
    public int ID => _id;
    public int HP => _hp;
    public int PullDeckCount => _pullDeckCount;
    public int AP => _abilityPoint;
    public Dictionary<int, int> DeckInventory => _deckInventory;

    public void SetID(int m_Id)
    {
        this._id = m_Id;
    }

    protected void CopyDeckData(List<int> m_deckData)
    {
        for (int i = 0; i < m_deckData.Count; i++)
        {
            if (!_deckInventory.ContainsKey(m_deckData[i]))
                _deckInventory.Add(m_deckData[i], 0);

            _deckInventory[m_deckData[i]]++;
        }
    }
}

public class PlayerCharacterInfo : CharacterInfo
{
    public void CopyCharacterData(int m_selectedID)
    {
        CharacterStruct origin = Manager.Instance.Data.GetCharacterData(m_selectedID);

        this._id = origin.ID;
        this._name = origin.Name;
        this._hp = origin.HP;

        base.CopyDeckData(origin.StartDecks);
    }
}

public class MobInfo : CharacterInfo
{
    private int _level;
    public int Level => _level;

    public void CopyMobData(int m_selectMobID)
    {
        MonsterStruct origin = Manager.Instance.Data.GetMobData(m_selectMobID);

        this._id = origin.ID;
        this._name = origin.Name;
        this._level = origin.Level;

        base.CopyDeckData(origin.StartDecks);
    }
}