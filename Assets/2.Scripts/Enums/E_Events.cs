public enum E_Events
{
    #region �� ��ȯ �̺�Ʈ 0

    ChangedMainMenu = 0,
    
    ChangedBonusArea,
    ChangedUnknownArea,

    
    RequestBonusArea,
    RequestUnknownArea,
    OutArea,

    RequestBattle,
    ChangedBattleScene,
    BattleReady,
    StartBattle,
    #endregion

    
    
    #region ���� �̺�Ʈ 50
    
    PlayerTurn = 50,
    EnemyTurn,

    PlayerTurnEnd,
    EnmyTurnEnd,

    StageClear,
    StageFail,

    UsedPassiveDeck,
    UsedActiveDeck,
    #endregion


}