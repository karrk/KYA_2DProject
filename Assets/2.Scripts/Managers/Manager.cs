using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private WaitManager _wait = null;
    public WaitManager Wait
    {
        get
        {
            if (_wait == null)
                _wait = new WaitManager();

            return _wait;
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

        AddManagerEvent();
        DOTween.Init(false, true, LogBehaviour.Verbose).SetCapacity(200, 50);
    }

    private void Start()
    {
        StartCoroutine(StepInit());
    }

    private IEnumerator StepInit()
    {
        yield return Data.LoadData();

        // 임시
        Data.RegistInitCharacter(Data.v_data.PlayerData.SelectedCharacterID);
        // 임시

        Pool.CreatePool(E_PoolType.Deck);
    }

    private void AddManagerEvent()
    {
        Event.AddListener(E_Events.ChangedBattleScene,Data);
    }

    public void SceneLoad(int m_sceneNumber)
    {
        StartCoroutine(SceneLoadRoutine(m_sceneNumber));
    }

    private IEnumerator SceneLoadRoutine(int m_sceneNumber)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_sceneNumber);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        E_Scene scene = (E_Scene)m_sceneNumber;

        switch (scene)
        {
            case E_Scene.MainMenu:
                break;
            case E_Scene.BattleScene:
                Event.PlayEvent(E_Events.ChangedBattleScene);
                break;
            case E_Scene.BounsArea:
                break;
            case E_Scene.Ending:
                break;
            case E_Scene.UnknownArea:
                break;
            default:
                break;
        }
    }
}
