using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoolableObject
{
    void Setup();
    void OnEnabled();
    void OnDisabled();
    void Destroy();
}
