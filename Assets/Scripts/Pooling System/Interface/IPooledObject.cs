using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{
    public interface IPooledObject
    {
        void OnGetFromPool();
        void OnReturnToPool();
        PoolType PoolType { get; }
        GameObject PoolObj { get; }
    }
}

