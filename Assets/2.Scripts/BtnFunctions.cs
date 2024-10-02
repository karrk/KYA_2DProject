using UnityEngine;

public class BtnFunctions
{
    public static void NoneAction() { Debug.Log("BTN : NoneAction"); }
    public static void ExitPlayerTurn()
    {
        Manager.Instance.Event.PlayEvent(E_Events.PlayerTurnEnd);
    }

    public static void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
