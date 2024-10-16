using System;
using UnityEngine;

namespace PoolingSystem
{   [Serializable]
    public class PoolData
    {
        public int Amount;
        public PoolType PoolType;
        public bool CanExpandPool;
        public GameObject PoolPrefab;
    }
}

