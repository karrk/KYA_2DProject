using UnityEngine;

public class BtnFunctions
{
    public static void NoneAction() { Debug.Log("None"); }

    public static void ExitPlayerTurn()
    {
        Manager.Instance.Event.PlayEvent(E_Events.PlayerTurnEnd);
    }

}
