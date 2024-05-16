using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
    where T : Component
{

    protected static T _singleton;
    public static T Instance
    {
        get
        {
            if (!InstanceExists())
            {
                T aux = (T)GameObject.FindObjectOfType<T>();
                if (aux == null)
                {
                    aux = CreateInstance();
                }
                _singleton = aux;
            }
            return _singleton;
        }
        set
        {
            _singleton = value;
        }
    }

    public static T CreateInstance()
    {
        return (new GameObject("---- " + typeof(T).Name + " ----", typeof(T))).GetComponent<T>();
    }

    private static bool InstanceExists()
    {
        return !(_singleton == null);
    }
}
