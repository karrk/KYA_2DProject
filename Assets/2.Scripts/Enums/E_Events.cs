public enum E_Events
{
    #region 씬 전환 이벤트 0

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

    
    
    #region 전투 이벤트 50
    
    PlayerTurn = 50,
    EnemyTurn,
    
    StageClear,
    UsedPassiveDeck,
    UsedActiveDeck,
    #endregion


}