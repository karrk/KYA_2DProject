using UnityEngine;

public class BtnFunctions
{
    public static void NoneAction() { Debug.Log("None"); }

    public static void ExitPlayerTurn()
    {
        Debug.Log("플레이어 턴 종료");
    }

    public static void TextStr(string m_str)
    {
        Debug.Log(m_str);
    }

    public static void Textff(float m_f)
    {
        Debug.Log(m_f);
    }

}
