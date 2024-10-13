using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolingSystem
{
    [CreateAssetMenu(fileName = "PoolsHolder", menuName = "PoolingSystem/PoolHolder")]
    public class PoolHolderSO : ScriptableObject
    {
        public PoolData[] Pools;
    }
}

