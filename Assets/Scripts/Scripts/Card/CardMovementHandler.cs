using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using InputSystem;
using UnityEngine;
using Zenject;

public class CardMovementHandler : MonoBehaviour
{
           // Injected deck display for card managemen
    private BaseSlot _slot;                                   // Current slot for placing a card
    private GameObject _selectedCard;                     // The card currently being dragged
    private GameObject _fakeCard;                         // A fake card used for visual feedback
   [SerializeField] private Collider _collider;
   private BattleHandler _battleHandler;// Collider for interaction detection
    private ICard _cardInSlot;
    private List<ICard> _cardsInteracting = new List<ICard>(); // Card currently in the slot
    private bool _canInteract;
    private IInputHandler _inputHandler;         // Injected input handler// Injected game controller for game logic
    private DeckDisplay _deckDisplay;    
    [Inject]
    private void Constructor( IInputHandler inputHandler , DeckDisplay deckDisplay,BattleHandler battleHandler)
    {
        _inputHandler = inputHandler;
        _deckDisplay = deckDisplay;
        _battleHandler = battleHandler;
        if (_slot == null) 
            _slot = _battleHandler.GetPlayerSlot();
    }
    private void Awake()
    {
        _fakeCard = new GameObject("FakeCard");          // Initialize the fake card object
    }

    private void OnEnable()
    {
        // Subscribe to input events
        _inputHandler.OnCardStartDragging += OnCardClick;
        _inputHandler.OnCardDropped += OnCardDrop;
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        _inputHandler.OnCardStartDragging -= OnCardClick;
        _inputHandler.OnCardDropped -= OnCardDrop;
    }

    private void OnCardClick(GameObject obj)
    {
        _canInteract = true;
        _collider.enabled = true;                           // Enable the collider
        _selectedCard = obj;                               // Store the selected card
        
        if (_cardInSlot == obj.GetComponent<ICard>())
        {
            _slot.HighLight();                             // Highlight the slot if the card is in it
        }
        
        _deckDisplay.RemoveCard(obj.transform);           // Remove the card from the deck display
        obj.transform.DORotate(Vector3.zero, 0.2f);       // Rotate the card for visual feedback
    }

    private void Update()
    {
        // Follow the selected card while dragging
        if (_selectedCard != null)
        {   
            HoverEffect();
            transform.position = _selectedCard.transform.position; // Update position to follow the selected card
        }
    }

    public void ClearCardSelections()
    {
        _cardInSlot = null;
        _selectedCard = null;
    }
    private void OnCardDrop(GameObject obj)
    {   
        _deckDisplay.RemoveCard(_fakeCard.transform);
        if (_selectedCard == null) return;                  // If no card is selected, exit

        _collider.enabled = false;                           // Disable the collider on drop
        
        if (_slot == null) return;                          // If no slot is available, exit

        if (_slot.Stats == SlotStats.Switch)
        {
            // If slot is switchable, return the card to the deck and place it in the slot
            _deckDisplay.AddCard(_cardInSlot.CardTr);
            _cardInSlot = obj.GetComponent<ICard>();
            _battleHandler.OnCardPlaced(_cardInSlot);
            obj.transform.DOMove(_slot.SlotPlacePoint.position, 0.3f).OnComplete(() =>
            {
                _slot.PlaceCard();
            });
        }
        else if (_slot.Stats == SlotStats.Highlighted)
        {
            // If slot is highlighted, place the card and notify the Battle Handler
            obj.transform.DOMove(_slot.SlotPlacePoint.position, 0.3f).OnComplete(() =>
            {
                _cardInSlot = obj.GetComponent<ICard>();
               _battleHandler.OnCardPlaced(_cardInSlot); // Inform the BattleHandler about the selected card
                _slot.PlaceCard();
            });
        }
        else
        {
            // If neither, return the card to the deck
            if (_deckDisplay.IsCardIncluded(_fakeCard.transform))
            {
                _deckDisplay.AddCardToNearNeighbor(_fakeCard.transform, _selectedCard.transform);
                _deckDisplay.RemoveCard(_fakeCard.transform);
            }
            else
            {
                _deckDisplay.AddCard(obj.transform);
            }
        }
        
        _selectedCard = null;
    }

    private void HoverEffect()
    {
        if(_cardsInteracting.Count >1 && _canInteract) 
        {
            _fakeCard.transform.position = transform.position;
            _deckDisplay.Hover(_fakeCard.transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<BaseSlot>(out var slot))
        {
            _slot = slot;                                  // Store the current slot
            
            if (slot.Stats == SlotStats.Ocupied)
            {
                slot.SwitchLight();                         // Switch light if slot is occupied
                return;
            }

            slot.HighLight();                               // Highlight the slot
        }

        if (other.transform.TryGetComponent<ICard>(out var card))
        {       
           if(!_cardsInteracting.Contains(card)) _cardsInteracting.Add(card);
            
            //_deckDisplay.RemoveCard(_fakeCard.transform); // Remove the fake card from display
            //_deckDisplay.AddCardToNearNeighbor(card.CardTr, _fakeCard.transform); // Add the fake card next to the displayed card
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<BaseSlot>(out var slot))
        {
            if (_slot.Stats == SlotStats.Highlighted)
            {
                _cardInSlot = null;
            }
            if (_cardInSlot != null)
            {
                _slot.PlaceCard();                        // Place the card if it's in a slot
                return;
            }
            else
            {   
                slot.UnHighLight();                       // Un-highlight the slot
            }
        }

        if (other.transform.TryGetComponent<ICard>(out var card))
        {
            if (_cardsInteracting.Contains(card)) _cardsInteracting.Remove(card);

            if (_cardsInteracting.Count <2)
            {   
                Debug.Log("Card Removed");
                _deckDisplay.RemoveCard(_fakeCard.transform);
            }
        }
    }

    public void AutoPlay(Action onEnd)
    {   
        _canInteract = false;
        _cardsInteracting = new List<ICard>();
        _deckDisplay.RemoveCard(_fakeCard.transform);
        _inputHandler.ActivateOrDeactivateInput(false);
       
        
        if (_cardInSlot != null)
        {   
            _battleHandler.OnCardPlaced(_cardInSlot);
            onEnd?.Invoke();
            return;
        }
        else
        {
          
            if(_selectedCard == null)
            {
                var card = _deckDisplay.SelectRandomCard();
                _selectedCard = card.gameObject;
            }
        }
        _selectedCard.transform.DOMove(_slot.SlotPlacePoint.position, 0.3f).OnComplete(()=>
        {
            var card = _selectedCard.GetComponent<ICard>();
           _battleHandler.OnCardPlaced(card);
            _slot.PlaceCard();
            onEnd?.Invoke();
            _cardInSlot = null;
            _selectedCard = null;
        });
        _selectedCard.transform.DORotate(Vector3.zero, 0.3f);
        
    }
}
