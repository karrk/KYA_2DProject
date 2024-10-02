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
    public PlayerDeckController PlayerDeckController;

    public DBValue<int> WaitDecksCount = new DBValue<int>();
    public DBValue<int> GraveDecksCount = new DBValue<int>();

    public IllustController Illust;

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
