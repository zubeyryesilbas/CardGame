using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InputSystem;
using PoolingSystem;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CardLayoutCrator : MonoBehaviour
{
   [SerializeField] private float _scale;
   [SerializeField] private float _spaceX, _spaceY;
   [SerializeField] private int _selectionSize;
   [SerializeField] private CardsHolderSo _cardsHolderSo;
   private Transform _slotHolderTr;
   private Transform _cardsHolderTr;
    private IInputHandler _inputHandler;
   private Dictionary<ICard, Slot> _slotDic = new Dictionary<ICard, Slot>();
   private List<Slot> _cardSelectionSlots = new List<Slot>();
   private List<Slot> _cardShowCaseSlots = new List<Slot>();
   private List<ICard> _selectedCards = new List<ICard>();
   [SerializeField] private Button _startButton;
   private GameController _gameController;
   private PoolController _poolController;

   [Inject]
   private void Constructor(GameController gameController , IInputHandler inputHandler , PoolController poolController)
   {
      _gameController = gameController;
      _inputHandler = inputHandler;
      _poolController = poolController;
   }

   private void Start()
   {  
      _startButton.onClick.AddListener(() =>
      {
         var cards = new List<Card>();
         foreach (var item in _selectedCards)
         {
            cards.Add(item.Card);
         }
         _gameController.StartGame(cards);
         gameObject.SetActive(false);
         Hide();
      });
      CreateLayout();
   }

   private void OnEnable()
   {
      _inputHandler.OnCardClicked += OnCardClicked;
   }

   private void OnDisable()
   {
      _inputHandler.OnCardClicked -= OnCardClicked;
   }

   private Slot GetEmptySlot(List<Slot> slotsToCheck)
   {
      foreach (var item in slotsToCheck)
      {
         if(item.Stats == SlotStats.Empty)
         return item;
      }
      return null;
   }
   private void OnCardClicked(GameObject clickedObject)
   {
      var card = clickedObject.GetComponent<ICard>();
      
      if (!_selectedCards.Contains(card))
      {  
         if (_selectedCards.Count >= 6)
         {
            return;
         }
         var emptySlot = GetEmptySlot(_cardSelectionSlots);
         if (emptySlot)
         {  
            EmptySlot(card);
            emptySlot.SetCardStats(SlotStats.Ocupied);
            _slotDic.Add(card , emptySlot);
            clickedObject.transform.position = emptySlot.SlotPlacePos.position;
         }
         _selectedCards.Add(card);
      }
      else
      {
         EmptySlot(card);
         _selectedCards.Remove(card);
         var emptySlot = GetEmptySlot(_cardShowCaseSlots);
         if (emptySlot)
         {  
            _slotDic.Add(card ,emptySlot);
            emptySlot.SetCardStats(SlotStats.Ocupied);
            clickedObject.transform.position = emptySlot.SlotPlacePos.position;
         }
      }
      _slotHolderTr.SetParent(transform);
      _cardsHolderTr.SetParent(transform);
      _startButton.interactable = CheckIfCardSelectionDone();
   }

   private void EmptySlot(ICard card)
   {
      if (_slotDic.ContainsKey(card))
      {
         _slotDic[card].SetCardStats(SlotStats.Empty);
         RemoveElementFromSlot(card);
      }
   }
   private void RemoveElementFromSlot(ICard card)
   {
      if (_slotDic.ContainsKey(card))
         _slotDic.Remove(card);
   }
   private bool CheckIfCardSelectionDone()
   {
      if (_selectedCards.Count == 6)
      {
         return true;
      }
      else
      {
         return false;
      }
   }
   private void CreateLayout()
   {
      if (_slotHolderTr == null) _slotHolderTr = new GameObject("SlotHolder").transform;
      if (_cardsHolderTr == null) _cardsHolderTr = new GameObject("CardHolder").transform;
      for (var i = 0; i < 3; i++)
      {
         for (int j = 0; j < 2; j++)
         {
           _cardSelectionSlots.Add(CreateSlotAtPos(new Vector3((i-1)*_spaceX, j*_spaceY , 0) ,_slotHolderTr));
         }
      }

      var z = 0;
      for (var i = 0; i < 5; i++)
      {
         for (var j = 0; j < 2; j++)
         {
            var display = _poolController.GetFromPool(PoolType.CardDisplay , _cardsHolderTr).PoolObj.GetComponent<CardDisplay>();
            var spawnPos = new Vector3((i - 2f) * _spaceX, j * _spaceY , 0);
            var slot = CreateSlotAtPos(spawnPos, _cardsHolderTr);
            display.transform.position = slot.SlotPlacePos.position;
            var cardData = _cardsHolderSo.Cards[z];
            var card = new Card(cardData.CardName, cardData.Attack, cardData.Defense);
            display.Initialize(cardData.CardImage ,card);
            _cardShowCaseSlots.Add(slot);
            z++;
         }
      }
      _slotHolderTr.position = Vector3.zero +Vector3.up*1;
      _cardsHolderTr.position = Vector3.zero + Vector3.down*3;
   }
   private Slot CreateSlotAtPos(Vector3 pos ,Transform parent)
   {
      var slot = _poolController.GetFromPool(PoolType.Slot , parent).PoolObj.GetComponent<Slot>();
      slot.transform.localPosition = pos;
      return slot;
   }
   public void Hide()
   {
      _slotHolderTr.gameObject.SetActive(false);
      _cardsHolderTr.gameObject.SetActive(false);
      _startButton.gameObject.SetActive(false);
   }
}
