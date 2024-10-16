using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PoolingSystem;
using UnityEngine;

namespace PoolingSystem
{
   public class PoolController : MonoBehaviour
   {
      [SerializeField] private PoolHolderSO _poolHolder;
      private Dictionary<PoolType, List<IPooledObject>> _poolInstances = new Dictionary<PoolType, List<IPooledObject>>();

      private void Awake()
      {
         Initialize();
      }

      private void Initialize()
      {
         foreach (var item in _poolHolder.Pools)
         {
            var pool = new List<IPooledObject>();
            _poolInstances.Add(item.PoolType ,pool);
            for (var i = 0; i < item.Amount; i++)
            {
               var itemInstance = Instantiate(item.PoolPrefab, Vector3.zero, Quaternion.identity)
                  .GetComponent<IPooledObject>();
               itemInstance.PoolObj.gameObject.SetActive(false);
               pool.Add(itemInstance);
            }
         }
      }
   
      public IPooledObject GetFromPool(PoolType poolType)
      {
         if (_poolInstances[poolType].Count <= 0)
         {
            if(_poolHolder.Pools.FirstOrDefault(x => x.PoolType == poolType).CanExpandPool)
            {
               var pooledObject = Instantiate(_poolHolder.Pools.FirstOrDefault(x => x.PoolType == poolType).PoolPrefab)
                  .GetComponent<IPooledObject>();
               pooledObject.OnGetFromPool();
               pooledObject.PoolObj.SetActive(true);
               return pooledObject;
            }
            else
            {  
               Debug.LogError("There is no object left in the pool");
               return null;
            }
         }
         else
         {
            var item = _poolInstances[poolType].Last();
            item.OnGetFromPool();
            _poolInstances[poolType].Remove(item);
            item.PoolObj.gameObject.SetActive(true);
            return item;
         }
      }
      public IPooledObject GetFromPool(PoolType poolType , Transform parent)
      {
         var pooledObj = GetFromPool(poolType);
         pooledObj.PoolObj.transform.SetParent(parent);
         pooledObj.PoolObj.gameObject.SetActive(true);
         return pooledObj;
      }
   
      public void ReturnToPool(IPooledObject pooledObject)
      {  
         pooledObject.PoolObj.transform.SetParent(transform);
         pooledObject.OnReturnToPool();
         pooledObject.PoolObj.SetActive(false);
         _poolInstances[pooledObject.PoolType].Add(pooledObject);
      }
      
   }

}
