using System.Collections;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _instance = null;
    public static Manager Instance => _instance;

    #region 각 매니저

    private GameManager _GM = null;
    public GameManager GM
    {
        get
        {
            if (_GM == null)
                _GM = new GameManager();

            return _GM;
        }
    }

    private DataManager _data = null;
    public DataManager Data
    {
        get
        {
            if (_data == null)
                _data = new DataManager();

            return _data;
        }
    }

    private SettingManager _setting = null;
    public SettingManager Setting
    {
        get
        {
            if (_setting == null)
                _setting = new SettingManager();

            return _setting;
        }
    }

    private DeckManager _deckManager = null;
    public DeckManager DM
    {
        get
        {
            if (_deckManager == null)
                _deckManager = new DeckManager();

            return _deckManager;
        }
    }

    private PoolManager _poolManager = null;
    public PoolManager Pool
    {
        get
        {
            if (_poolManager == null)
                _poolManager = new PoolManager();

            return _poolManager;
        }
    }

    private EventManager _event = null;
    public EventManager Event
    {
        get
        {
            if (_event == null)
                _event = new EventManager();

            return _event;
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
