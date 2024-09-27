public enum E_Events
{
    #region 씬 전환 이벤트 0

    ChangedMainMenu = 0,
    ChangedBattle,
    ChangedBonusArea,
    ChangedUnknownArea,

    RequestBattle,
    RequestBonusArea,
    RequestUnknownArea,
    OutArea,

    StartBattle,
    #endregion

    
    
    #region 전투 이벤트 50
    
    PlayerTurn = 50,
    EnemyTurn,
    StageReady,
    StageClear,
    UsedPassiveDeck,
    UsedActiveDeck,
    #endregion


}