using UnityEngine;
using UnityEngine.Pool;

public class GenericPool<T> where T: MonoBehaviour, PoolableObject
{
    private IObjectPool<T> m_Pool;
    public GenericPool()
    {
        m_Pool = new LinkedPool<T>(_create, _get, _release, _destroy, true, 30);
    }

    public T Get()
    {
        return m_Pool.Get();
    }
    public void Release(T obj)
    {
        m_Pool.Release(obj);
    }

    T _create()
    {
        var go = new GameObject("");
        var ps = go.AddComponent<T>();

        ps.Setup();

        return ps;
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
