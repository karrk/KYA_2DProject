using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PoolManager
{
    private const string PrefabsPath = "Asset/3.Prefabs/PooledObj";

    private Dictionary<E_PoolType, ObjectPool> _pools;
    private IPooledObject[] _prefabs;

    public PoolManager()
    {
        _pools = new Dictionary<E_PoolType, ObjectPool>();
        _prefabs = new IPooledObject[(int)E_PoolType.Size];

        RegistPrefabs();
    }

    public void CreatePool(E_PoolType m_type)
    {
        ObjectPool newPool = new GameObject().AddComponent<ObjectPool>();
        newPool.transform.SetParent(Manager.Instance.transform);
        newPool.gameObject.name = m_type.ToString();

        _pools.Add(m_type, newPool);
        newPool.Initialize(m_type,Manager.Data.InitPoolCount);
    }

    private void RegistPrefabs()
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabsPath);
            _prefabs[i] = prefab.GetComponent<IPooledObject>();
        }
    }

    public IPooledObject GetPrefab(E_PoolType m_type)
    {
        return _prefabs[(int)m_type];
    }
}
