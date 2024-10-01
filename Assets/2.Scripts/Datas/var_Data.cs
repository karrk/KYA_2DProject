/// <summary>
/// �������� �����Ͽ� �����ų �� �ִ� ������
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
