using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDeck : Deck
{
    public override int Cost => Manager.Instance.Data.GetMobDeckData(ID).Cost;
    public override string Name => Manager.Instance.Data.GetMobDeckData(ID).Name;
    public override int Atk => Manager.Instance.Data.GetMobDeckData(ID).Atk;
    public override int Def => Manager.Instance.Data.GetMobDeckData(ID).Def;
    public override bool IsDisappear => Manager.Instance.Data.GetMobDeckData(ID).Disappear;
    public override E_DeckType Type => Manager.Instance.Data.GetMobDeckData(ID).Type;

    public int NextTurnPlusPower => Manager.Instance.Data.GetMobDeckData(ID).NextTurnPlusPower;
    public float DecreasePowerPercent => Manager.Instance.Data.GetMobDeckData(ID).DecreasePowerPercent;
    public int[] N_turnAction => Manager.Instance.Data.GetMobDeckData(ID).N_turnAction;
    public int SteelGold => Manager.Instance.Data.GetMobDeckData(ID).SteelGold;
    public bool Runaway => Manager.Instance.Data.GetMobDeckData(ID).Runaway;
    public int Heal => Manager.Instance.Data.GetMobDeckData(ID).Heal;



}
