using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoolingSystem;
using UnityEngine;

public class Slot : MonoBehaviour, IPooledObject
{
    [SerializeField] private SpriteRenderer _slotRenderer;
    public Action OnCardPlaced;
    public Action OnCardRemoved;
    private SlotStats _stats;
    public SlotStats Stats => _stats;
    public bool IsEmpty;
    public ICard Card;
    private ICard _card;
    [SerializeField] private Transform _slotPlacePos;
    public Transform SlotPlacePos => _slotPlacePos;
    public PoolType PoolType { get; }
    public GameObject PoolObj => gameObject;

    public void PlaceCard()
    {
        _stats = SlotStats.Ocupied;
        _slotRenderer.color = Color.blue;
        OnCardPlaced?.Invoke();
        IsEmpty = false;
    }

    public void SetCardStats(SlotStats slotStats)
    {
        _stats = slotStats;
    }

    public void RemoveCard()
    {
        _stats = SlotStats.Empty;
        IsEmpty = true;
        OnCardRemoved?.Invoke();
    }

    public void HighLight()
    {
        _stats = SlotStats.Highlighted;
        _slotRenderer.color = Color.green;
    }

    public void UnHighLight()
    {
        _slotRenderer.color = Color.grey;
        RemoveCard();
    }

    public void SwitchLight()
    {
        _stats = SlotStats.Switch;
        _slotRenderer.color = Color.yellow;
    }

    public void OnGetFromPool()
    {
       
    }

    public void OnReturnToPool()
    {
       
    }
}
   
