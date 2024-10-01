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
}