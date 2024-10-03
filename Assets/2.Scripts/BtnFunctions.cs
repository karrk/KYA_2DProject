using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnFunctions
{
    public static void NoneAction() { Debug.Log("BTN : NoneAction"); }
    
    public static void ExitPlayerTurn()
    {
        Manager.Instance.Event.PlayEvent(E_Events.PlayerTurnEnd);

        Manager.Instance.Event.PlayEvent(E_Events.EnemyTurn);
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public static void SelectCharacter(float m_value)
    {
        Manager.Instance.Data.v_data.PlayerData.SelectedCharacterID = (int)m_value;
        Manager.Instance.Data.v_data.Illust.SetImage((int)m_value);
    }

    public static void StartGame()
    {
        Manager.Instance.SceneLoad((int)E_Scene.BattleScene);
    }

    public static void TurnOffObject(GameObject m_obj)
    {
        m_obj.SetActive(false);
    }

    public static void TurnOnObject(GameObject m_obj)
    {
        m_obj.SetActive(true);
    }
}
