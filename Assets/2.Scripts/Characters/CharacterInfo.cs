using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    private int _id;

    [SerializeField] private string _name;
    [SerializeField] private int _hp;
    private Dictionary<int, int> _deckInventory = new Dictionary<int, int>();

    public string Name => _name;
    public int ID => _id;
    public int HP => _hp;
    public Dictionary<int, int> DeckInventory => _deckInventory;
    
}