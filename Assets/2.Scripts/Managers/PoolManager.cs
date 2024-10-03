using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PoolManager
{
    private const string PrefabsPath = "Assets/3.Prefabs/PooledObj/";

    private Dictionary<E_PoolType, ObjectPool> _pools;
    private IPooledObject[] _prefabs;

    public ObjectPool GetPool(E_PoolType m_type) { return _pools[m_type]; }

    public void Initialize()
    {
        _pools = new Dictionary<E_PoolType, ObjectPool>();
        _prefabs = new IPooledObject[(int)E_PoolType.Size];

        for (int i = 0; i < (int)E_PoolType.Size; i++)
        {
            Manager.Instance.StartCoroutine(LoadPrefab((E_PoolType)i));
        }
    }

    public void CreatePool(E_PoolType m_type)
    {
        ObjectPool newPool = new GameObject().AddComponent<ObjectPool>();
        newPool.transform.SetParent(Manager.Instance.transform);
        newPool.gameObject.name = m_type.ToString();

        _pools.Add(m_type, newPool);
        newPool.Initialize(m_type,Manager.Instance.Data.InitPoolCount);
    }

    public IPooledObject GetObject(E_PoolType m_type)
    {
        return this._pools[m_type].GetObj();
    }

    private void RegistPrefabs(GameObject m_obj)
    {
        IPooledObject obj = m_obj.GetComponent<IPooledObject>();

        for (int i = 0; i < _prefabs.Length; i++)
        {
            _prefabs[i] = obj;
        }
    }

    public IPooledObject GetPrefab(E_PoolType m_type)
    {
        return _prefabs[(int)m_type];
    }

    private IEnumerator LoadPrefab(E_PoolType m_poolObjectType)
    {
        AsyncOperationHandle<GameObject> load 
            = Addressables.LoadAssetAsync<GameObject>($"{PrefabsPath}{m_poolObjectType}.prefab");

        yield return load;

        if (load.Status == AsyncOperationStatus.Failed)
        {
            Debug.Log("프리팹을 불러올 수 없음");
        }
        RegistPrefabs(load.Result);

        CreatePool(m_poolObjectType);
    }

}
