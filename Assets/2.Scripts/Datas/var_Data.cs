/// <summary>
/// 언제든지 접근하여 변경시킬 수 있는 데이터
/// </summary>
public class var_Data
{
    public PlayerStruct PlayerData;
    public VolumeStruct VolumeData;
    public PlayerCharacter CurrentCharacter;
    public Monster[] MonstersDatas;
    public MonstersController CurrentMobsController;

    public var_Data()
    {
        InitPlayerData();
    }

    private void InitPlayerData()
    {
        PlayerData.NickName = new DBValue<string>();
        PlayerData.TotalGold = new DBValue<int>();
        PlayerData.RoundGold = new DBValue<int>();
        PlayerData.MaxAP = new DBValue<int>();
    }
}
