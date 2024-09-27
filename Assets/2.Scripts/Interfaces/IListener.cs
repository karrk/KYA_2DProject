using System.ComponentModel;

public interface IListener
{
    public void OnEvent(E_Events m_eventType,Component m_order,object m_param);
}