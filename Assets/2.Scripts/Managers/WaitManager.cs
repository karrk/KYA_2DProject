using System.Collections.Generic;

/// <summary>
/// 일정 객체수가 전부 동작이 끝났음을 확인하고 다음 동작을 넘어가기 위해 중간에서 관리하기 위한 클래스
/// 일관성 있는 상호작용을 위함
/// </summary>
public class WaitManager
{
    private Dictionary<E_Events, List<IWaiter>> _waitSignalTable;
    private Dictionary<E_Events, int> _waitCounter;

    public WaitManager()
    {
        _waitSignalTable = new Dictionary<E_Events, List<IWaiter>>();
        _waitCounter = new Dictionary<E_Events, int>();
    }

    public void AddWaiter(E_Events m_eventType, IWaiter m_waiter)
    {
        if (!_waitSignalTable.ContainsKey(m_eventType))
        {
            _waitSignalTable.Add(m_eventType, new List<IWaiter>());
            _waitCounter.Add(m_eventType, 0);
        }

        _waitSignalTable[m_eventType].Add(m_waiter);
    }

    public void RemoveWaiter(E_Events m_eventType, IWaiter m_waiter)
    {
        _waitSignalTable[m_eventType].Remove(m_waiter);
    }

    public void SendFinishSign(E_Events m_eventType)
    {
        _waitCounter[m_eventType]++;

        if (_waitCounter[m_eventType] == _waitSignalTable[m_eventType].Count)
        {
            foreach (var waiter in _waitSignalTable[m_eventType])
            {
                waiter.StartNextAction(m_eventType);
            }

            _waitCounter[m_eventType] = 0;
        }
    }
}
