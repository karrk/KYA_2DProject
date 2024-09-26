using UnityEngine;

public class DataManager
{
    public var_Data v_data;
    private st_Data s_data;

    public DataManager()
    {
        v_data = new var_Data();
        s_data = new st_Data();
    }

    public void SaveData()
    {
        PlayerPrefs.Save();
    }
}



