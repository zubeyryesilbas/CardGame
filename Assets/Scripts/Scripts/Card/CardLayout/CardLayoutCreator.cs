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

public class CardLayoutCreator : MonoBehaviour
{
   private CardsHolderSo _cardsHolderSo;
   private CardLayoutSettings _cardLayoutSettings;
   private Transform _slotHolderTr;
   private Transform _cardsHolderTr;
    private IInputHandler _inputHandler;
   private Dictionary<ICard, BaseSlot> _slotDic = new Dictionary<ICard, BaseSlot>();
   private List<BaseSlot> _cardSelectionSlots = new List<BaseSlot>();
   private List<BaseSlot> _cardShowCaseSlots = new List<BaseSlot>();
   private List<ICard> _selectedCards = new List<ICard>();
   [SerializeField] private Button _startButton;
   private GameController _gameController;
   private PoolController _poolController;
   private int _selectionCount;

   [Inject]
   private void Constructor(SettingsHolderSO settingsHolderSo,CardsHolderSo cardsHolderSo , CardLayoutSettings cardLayoutSettings ,GameController gameController , IInputHandler inputHandler , PoolController poolController)
   {
      _selectionCount = settingsHolderSo.CardSelectionCount;
      _gameController = gameController;
      _inputHandler = inputHandler;
      _poolController = poolController;
      _cardLayoutSettings = cardLayoutSettings;
      _cardsHolderSo = cardsHolderSo;
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

   private BaseSlot GetEmptySlot(List<BaseSlot> slotsToCheck)
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
         if (_selectedCards.Count >= _selectionCount)
         {
            return;
         }
         var emptySlot = GetEmptySlot(_cardSelectionSlots);
         if (emptySlot)
         {  
            EmptySlot(card);
            emptySlot.SetCardStats(SlotStats.Ocupied);
            _slotDic.Add(card , emptySlot);
            clickedObject.transform.position = emptySlot.SlotPlacePoint.position;
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
            clickedObject.transform.position = emptySlot.SlotPlacePoint.position;
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
      if (_selectedCards.Count == _selectionCount)
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

      var slotTransforms = new List<Transform>();
      var cardTransforms = new List<Transform>();
      if (_cardsHolderSo.Cards.Length < _selectionCount) _selectionCount = _cardsHolderSo.Cards.Length;
      for (var i = 0; i < _selectionCount; i++)
      {
         var slot = CreateSlotAtPos(Vector3.zero, _slotHolderTr);
         slotTransforms.Add(slot.transform);
         _cardSelectionSlots.Add(slot);
      }
      DynamicGridPlacer.PlaceObjects(slotTransforms , _cardLayoutSettings.TopXRatio , _cardLayoutSettings.TopYRatio ,
         _cardLayoutSettings.SpaceX,_cardLayoutSettings.SpaceY);
      
      var top = slotTransforms[0].position;
      var top_DownPoint = slotTransforms.Last().position;
      slotTransforms = new List<Transform>();
      for (var j = 0; j<_cardsHolderSo.Cards.Length ; j++ )
      {
         var display = _poolController.GetFromPool(PoolType.CardDisplay , _cardsHolderTr).PoolObj.GetComponent<ICard>();
         cardTransforms.Add(display.CardTr);
         var slot = CreateSlotAtPos(Vector3.zero, _cardsHolderTr);
         _slotDic.Add(display ,slot);
         slotTransforms.Add(slot.transform);
         slot.SetCardStats(SlotStats.Ocupied);
         display.CardTr.position = slot.SlotPlacePoint.position;
         var cardData = _cardsHolderSo.Cards[j];
         var card = new Card(cardData.CardName, cardData.Attack, cardData.Defense);
         display.Initialize(cardData.CardImage ,card);
         _cardShowCaseSlots.Add(slot);
      }
      DynamicGridPlacer.PlaceObjects(slotTransforms , _cardLayoutSettings.DownXRatio , _cardLayoutSettings.DownYRatio , 
         _cardLayoutSettings.SpaceX,_cardLayoutSettings.SpaceY);
     
      for (var i = 0; i < cardTransforms.Count; i++)
      {
         cardTransforms[i].position = _cardShowCaseSlots[i].SlotPlacePoint.position;
      }
      _slotHolderTr.position =Vector3.up*_cardLayoutSettings.TopOffset;
      _cardsHolderTr.position = new Vector3(0,top_DownPoint.y , 0) + Vector3.up*_cardLayoutSettings.DownOffset;
      var down = slotTransforms.Last().transform.position;

      var aspectRatio = Screen.width / Screen.height;
      var vertical = Mathf.Abs(top.y - down.y);
      var horizontal = aspectRatio * vertical;
      horizontal *= 0.77f;
      vertical *= 0.77f;
      var topLeft = new Vector3(-horizontal, vertical, 0);
      var topRight = new Vector3(horizontal, vertical, 0);
      var downRight = new Vector3(horizontal, -vertical, 0);
      var downLeft = new Vector3(-horizontal, -vertical, 0);
      CameraPlacer.PlaceCameraToFitPoints(topLeft,topRight,downRight ,downLeft );
     
   }
   private BaseSlot CreateSlotAtPos(Vector3 pos ,Transform parent)
   {
      var slot = _poolController.GetFromPool(PoolType.Slot , parent).PoolObj.GetComponent<BaseSlot>();
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
