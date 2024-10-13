using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoolingSystem;
using UnityEngine;
using Zenject;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private BaseSlot _playerSlot, _opponentSlot;
    [SerializeField] private Transform _cardSpawnPos;
    [SerializeField] private BasicHealthBar _playerBar, _opponentBar;
    [SerializeField] private SkillPresenter _skillPresenter;
    [SerializeField] private TimerDisplay _timerDisplay;
    private TurnTimer _turnTimer;
    private PoolController _poolController;
    private CardFactory _cardFactory;
    private CardMovementHandler _cardMovementHandler;
    private ICard _playerCard;
    private ICard _opponentCard;

    [Inject]
    public void Constructor(PoolController poolController, CardFactory cardFactory , GameController gameController , CardMovementHandler cardMovementHandler)
    {
        _cardMovementHandler = cardMovementHandler;
        _poolController = poolController;
        _cardFactory = cardFactory;
        _turnTimer = new TurnTimer(5);
        _turnTimer.OnTimeOut += () =>
        {       
            _cardMovementHandler.AutoPlay(() =>
            {
                gameController.AutoPlay();
            });
           
           
        };
    }
    public void UpdatePlayerSkill(string header , string description)
    {
        _skillPresenter.UpdateSkill(header ,description);
    }

    public void Show()
    {
        _playerSlot.gameObject.SetActive(true);
        _opponentSlot.gameObject.SetActive(true);
        _playerBar.gameObject.SetActive(true);
        _opponentBar.gameObject.SetActive(true);
        _skillPresenter.gameObject.SetActive(true);
        _timerDisplay.gameObject.SetActive(true);
        _turnTimer.EnableProcess(true);
    }

    public void OnCardPlaced(ICard card)
    {
        _playerCard = card;
    }
    public BaseSlot GetPlayerSlot()
    {
        return _playerSlot;
    }
    private void Update()
    {   
        _timerDisplay.SetFillAmount(_turnTimer.GetRemained());
        _turnTimer.ExecuteTime();
    }

    private void HandleBattle()
    {
        _playerCard.DamageCallBack += PlayerTakeDamage;
        _opponentCard.DamageCallBack += OpponentTakeDamage;
        _playerCard.Attack(_opponentCard);
        _opponentCard.Attack(_playerCard);
    }

    private void PlayerTakeDamage(float amount)
    {
        _playerBar.SetValue((30 - amount) /30);
    }
    private void OpponentTakeDamage(float amount)
    {
        _opponentBar.SetValue((30 - amount) /30);
    }
    public void ShowOpponentCard(Card card)
    {
        _opponentCard = _poolController.GetFromPool(PoolType.CardDisplay).PoolObj.GetComponent<CardDisplay>();
        _opponentCard.CardTr.position = _cardSpawnPos.position;
        _opponentCard.Initialize(_cardFactory.GetSprite(card.Name ),card);
        _opponentCard.CardTr.DOMove(_opponentSlot.SlotPlacePoint.position, 0.3f).OnComplete(() =>
        {
            HandleBattle();
        });
        
    }
}
