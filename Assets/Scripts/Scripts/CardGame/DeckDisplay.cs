using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using PoolingSystem;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class DeckDisplay : MonoBehaviour
{
   [SerializeField]private float _slice =10f;
   private float _deckoffset;
   [SerializeField] private float _cardOffset;
   [SerializeField] private float _cardPosZOffset, _cardPosYOffset;
   [SerializeField] private float _radius;
   private List<Transform> _cards = new List<Transform>();
   private PoolController _poolController;
   private CardFactory _cardFactory;
   [SerializeField] private bool _dynamicUpdate;

    [Inject]
    private void Constructor(PoolController poolController, CardFactory cardFactory)
    {
       _poolController = poolController;
       _cardFactory = cardFactory;
    }
   public bool IsCardIncluded(Transform tr)
   {
      return _cards.Contains(tr);
   }

   public void CreateCardDisplays(List<Card> cards)
   {
      foreach (var item in cards)
      {
         var cardDisplay = _poolController.GetFromPool(PoolType.CardDisplay).PoolObj.GetComponent<CardDisplay>();
         cardDisplay.Initialize(_cardFactory.GetSprite(item.Name),item);
         AddCard(cardDisplay.transform);
      }
   }
   public void AddCard(Transform tr)
   {
      if (!_cards.Contains(tr))
      {
         tr.transform.DOMove(transform.position, 0.5f).OnComplete(() =>
         {
            _cards.Add(tr);
            LayoutCards();
         });
      }
   }

   public void Hover(Transform fake)
   {  
      if (_cards.Contains(fake)) _cards.Remove(fake);
      var cardIndex = _cards.Count;
      for (var i = 0; i < _cards.Count; i++)
      {  
         if (_cards[i].position.x < fake.position.x)
         {
            cardIndex--;
         }
      }
      
      _cards.Insert(cardIndex,fake);
      LayoutCards();
      
   }
   public void AddCardToNearNeighbor(Transform neighbor , Transform fake)
   {
      _deckoffset += _slice;
      var index = _cards.IndexOf(neighbor);
      var insetValue = index +1;
      if (insetValue <0)
         insetValue = 0;
         
      _cards.Insert(insetValue ,fake);
      LayoutCards();
   }

   public void RemoveCard(Transform tr)
   {  
      if (_cards.Contains(tr))
      {
        
         _cards.Remove(tr);
         LayoutCards();
      }
   }

   private void Update()
   {
      if(_dynamicUpdate) LayoutCards();
   }

   public Transform SelectRandomCard()
   {
      var count = _cards.Count;
      var random = Random.Range(0, count);
      var card = _cards[random];
      _cards.Remove(card);
      LayoutCards();
      return card;
   }
   private void LayoutCards()
   {
      _deckoffset = 90 - _cards.Count * _slice / 2;
      for (var i = 0; i < _cards.Count; i++)
      {
         var angleDegree = 270 +_slice*i + _deckoffset;
         var angleRadian =  (_slice*i + _deckoffset)* Mathf.Deg2Rad ;
         var eulerAngles = Vector3.forward *( angleDegree);
         Vector3 position = new Vector3(Mathf.Cos(angleRadian) * _radius, Mathf.Sin(angleRadian) * _radius,0 );
        var pos = position + _cardPosZOffset *Vector3.forward * i +_cardPosYOffset * Vector3.down ;
        _cards[i].DOMove(pos, 0.2f);
        _cards[i].DORotate(eulerAngles, 0.2f);

      }
   }
}
