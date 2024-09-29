public interface IWaiter
{
    public void StartNextAction(E_Events m_prevEvent);
    public void SendFinishSign(E_Events m_finEvent);
}