using System.Collections.Generic;
using UnityEngine;


public abstract class CharacterInfo
{
    public abstract int ID { get; } 
    
    public abstract string Name { get; }
    public abstract int HP { get; }
    public abstract int AP { get; }
}

public class PlayerCharacterInfo : CharacterInfo
{
    private PlayerCharacterStruct Info => Manager.Instance.Data.GetCharacterData(ID);

    public override int ID => Manager.Instance.Data.v_data.PlayerData.SelectedCharacterID;

    public int PullDecksCount => Info.PullDeckCount;
    public override string Name => Info.Name;
    public override int HP => Info.HP; // �ʱ� ĳ������ ���� �ݿ��Ǿ��⿡ �ǽð� �ݿ����� �ʿ�
    public override int AP => Info.AbilityPoint;
}

public class MobInfo : CharacterInfo
{
    private int _level = 0;
    public int Level => _level;

    public override int ID => _id;
    public override string Name => _name;
    public override int HP => _hp;
    public override int AP => _ap;

    private int _id;
    private string _name;
    private int _hp;
    private int _ap;

    public void CopyMonsterInfo(MonsterStruct m_mobData)
    {
        this._id = m_mobData.ID;
        this._name = m_mobData.Name;
        this._hp = m_mobData.HP;
        this._ap = m_mobData.AbilityPoint;
        this._level = m_mobData.Level;
    }
}