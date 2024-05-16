using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectLibrary<T> : Singleton<ObjectLibrary<T>> where T: IIdentifiable, new()
{
    [SerializeField] protected List<T> Elements;

    public List<T> GetElements() => Elements;
    public virtual T GetElementData(string id)
    {
        foreach (var currency in Elements)
            if (currency.GetId().Equals(id))
                return currency;

        return default(T);
    }
}
