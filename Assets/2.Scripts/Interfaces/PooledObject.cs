using UnityEngine;

public interface IPooledObject
{
    public GameObject MyObj { get; }
    public ObjectPool MyPool { get; }
    public void ReturnObj();
}