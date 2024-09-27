using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<IPooledObject> _pool;
    private IPooledObject _prefab;
    private E_PoolType _poolType;

    public void Initialize(E_PoolType m_type, int m_initCount)
    {
        this._poolType = m_type;
        this.gameObject.name = $"{m_type} Pool";

        _pool = new List<IPooledObject>(m_initCount);
        this._prefab = Manager.Pool.GetPrefab(m_type);

        CreateObj(m_initCount);
    }

    private void CreateObj(int m_initCount)
    {
        for (int i = 0; i < m_initCount; i++)
        {
            GameObject newObj = Instantiate(_prefab.MyObj);
            SetInitOptions(newObj);

            if (newObj.TryGetComponent<IPooledObject>(out IPooledObject pooledObj))
            {
                _pool.Add(pooledObj);
            }
            else
                Debug.LogError("풀링오브젝트가 아닌 요소 추가요청 \n 컴포넌트에 인터페이스가 해당하는지 확인하세요");
        }

        void SetInitOptions(GameObject obj)
        {
            obj.name = _poolType.ToString();
            obj.transform.SetParent(transform);
            obj.SetActive(false);
        }
    }

    public IPooledObject GetObj()
    {
        if(_pool.Count > 0)
        {
            IPooledObject obj = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count-1);

            obj.MyObj.SetActive(true);

            return obj;
        }
        else
        {
            CreateObj(_pool.Capacity * 2);
            return GetObj();
        }
    }

    public IPooledObject GetObj(Transform m_requestTr)
    {
        IPooledObject newObj = GetObj();
        newObj.MyObj.transform.SetParent(m_requestTr);

        return newObj;
    }

    public IPooledObject GetObj(Vector3 m_pos)
    {
        IPooledObject newObj = GetObj();
        newObj.MyObj.transform.position = m_pos;

        return newObj;
    }

    public void ReturnObj(IPooledObject m_obj)
    {
        m_obj.MyObj.SetActive(false);

        if (m_obj.MyObj.transform.parent != transform)
            m_obj.MyObj.transform.SetParent(transform);

        _pool.Add(m_obj);
    }
}
