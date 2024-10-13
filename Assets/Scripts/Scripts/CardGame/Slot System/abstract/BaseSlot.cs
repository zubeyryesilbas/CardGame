using PoolingSystem;
using UnityEngine;

public abstract class BaseSlot : MonoBehaviour,IPooledObject
{
   

    [SerializeField] protected Transform _slotPlacePoint;
    public Transform SlotPlacePoint => _slotPlacePoint;
    protected SlotStats _stats;
    public SlotStats Stats => _stats;

    public abstract void PlaceCard();
    public abstract void RemoveCard();
    public abstract void HighLight();
    public abstract void UnHighLight();
    public abstract void SwitchLight();
    
    public void SetCardStats(SlotStats slotStats)
    {
        _stats = slotStats;
    }

    public void OnGetFromPool()
    {
        _stats = SlotStats.Empty;
    }

    public void OnReturnToPool()
    {
        _stats = SlotStats.Empty;
    }

    public PoolType PoolType { get; }
    public GameObject PoolObj => gameObject;
}