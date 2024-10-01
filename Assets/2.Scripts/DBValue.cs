using System.Collections.Generic;

public class Ref<T>
{
    public T Value;

    public Ref(T value)
    {
        Value = value;
    }
}

public class DBValue<T>
{
    private List<IBinder> _binders = new List<IBinder>();
    private Ref<T> _value;
    public T Value
    {
        get { return _value.Value; }
        set { SetValue(value); }
    }

    public DBValue() { this._value = new Ref<T>(default); }

    public DBValue(T m_value)
    {
        this._value = new Ref<T>(m_value);
    }

    public void SetValue(T m_value)
    {
        this._value.Value = m_value;
        OnChangedValue();
    }

    public void AddBinder(IBinder m_binder)
    {
        this._binders.Add(m_binder);
    }

    private void OnChangedValue()
    {
        for (int i = 0; i < _binders.Count; i++)
        {
            _binders[i].OnChangedValue(this._value.Value);
        }
    }
}

public interface IBinder
{
    public void OnChangedValue(object m_param);
}
