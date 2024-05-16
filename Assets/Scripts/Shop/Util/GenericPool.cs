using UnityEngine;
using UnityEngine.Pool;

public class GenericPool<T> where T: MonoBehaviour, IPoolable
{
    private IObjectPool<T> m_Pool;
    private T m_Prefab;
    public GenericPool(T m_Prefab)
    {
        m_Pool = new LinkedPool<T>(_create, _get, _release, _destroy, true, 30);
        this.m_Prefab = m_Prefab;
    }

    public T Get()
    {
        return m_Pool.Get();
    }
    public void Release(T obj)
    {
        obj.transform.parent.SetParent(null);
        m_Pool.Release(obj);
    }

    T _create()
    {
        var go = GameObject.Instantiate(m_Prefab);

        go.Setup();

        return go;
    }
    void _get(T obj)
    {
        obj.OnEnabled();
    }

    void _release(T obj)
    {
        obj.OnDisabled();
    }
    void _destroy(T obj)
    {
        obj.Destroy();
    }
}
