/// <summary>
/// 언제든지 접근하여 변경시킬 수 있는 데이터
/// </summary>
public class var_Data
{
    public var_Data()
    {
        PlayerData.Decks = new System.Collections.Generic.List<int>();
    }

    public PlayerStruct PlayerData;
    public VolumeStruct VolumeData;
}
