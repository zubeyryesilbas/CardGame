using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoolingSystem;
using Unity.Mathematics;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class DeckDisplay : MonoBehaviour
{
   [SerializeField]private float _slice =10f;
   [SerializeField] private float _deckoffset;
   [SerializeField] private float _cardOffset;
   [SerializeField] private float _cardPosZOffset, _cardPosYOffset;
   private List<Transform> _cards = new List<Transform>();
   [SerializeField] private float _radius;
    private PoolController _poolController;
    private CardFactory _cardFactory;

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
         cardDisplay.Initialize(_cardFactory.GetSprite(item.Name), item);
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

   public void AddCardToNearNeighbor(Transform neighbor , Transform fake)
   {
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
      for (var i = 0; i < _cards.Count; i++)
      {
         var angleDegree = _slice * i + _cardOffset;
         var angleRadian =  (_slice*i + _deckoffset)* Mathf.Deg2Rad ;
         var eulerAngles = Vector3.forward *( angleDegree);
         Vector3 position = new Vector3(Mathf.Cos(angleRadian) * _radius, Mathf.Sin(angleRadian) * _radius,0 );
        var pos = position + _cardPosZOffset *Vector3.forward * i +_cardPosYOffset * Vector3.down ;
        _cards[i].DOMove(pos, 0.2f);
        _cards[i].DORotate(eulerAngles, 0.2f);

      }
   }
}
