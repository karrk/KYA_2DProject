using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance => _instance;

    #region 각 매니저

    private static GameManager _GM = null;
    public static GameManager GM
    {
        get
        {
            if (_GM == null)
                _GM = new GameManager();

            return _GM;
        }
    }

    private static DataManager _data = null;
    public static DataManager Data
    {
        get
        {
            if (_data == null)
                _data = new DataManager();

            return _data;
        }
    }

    private static SettingManager _setting = null;
    public static SettingManager Setting
    {
        get
        {
            if (_setting == null)
                _setting = new SettingManager();

            return _setting;
        }
    }

    #endregion

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        StartCoroutine(StepInit());
    }

    private IEnumerator StepInit()
    {
        yield return Data.LoadData();
    }

}
