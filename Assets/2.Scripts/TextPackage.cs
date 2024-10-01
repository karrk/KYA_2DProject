public class TextPakage
{
    private static TextPakage _instance = null;
    public static TextPakage Instance
    {
        get
        {
            if (_instance == null)
                _instance = new TextPakage();

            return _instance;
        }
    }

    private DataManager _data = Manager.Instance.Data;

    public DBValue<int> MaxAP => _data.v_data.PlayerData.MaxAP;
    public DBValue<int> CurAP => _data.v_data.CurrentCharacter.AP;

    public DBValue<string> NickName => _data.v_data.PlayerData.NickName;
    public DBValue<string> CharacterName => _data.v_data.CurrentCharacter.Name;
    public DBValue<int> PlayerHP => _data.v_data.CurrentCharacter.HP;
    public DBValue<int> PlayerGold => _data.v_data.PlayerData.RoundGold;

    public DBValue<int> GraveDecksCount => _data.v_data.GraveDecksCount;
    public DBValue<int> WaitDecksCount => _data.v_data.WaitDecksCount;
}
        
