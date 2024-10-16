using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InputSystem;
using PoolingSystem;
using SkillSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private BaseSlot _playerSlot, _opponentSlot;
    [SerializeField] private Transform _cardSpawnPos, _battlePos;
    [SerializeField] private PlayerDisplay _playerDisplay, _opponentDisplay;
    [SerializeField] private SkillPresenter _skillPresenter;
    [SerializeField] private TimerDisplay _timerDisplay;
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private Button _useSkillButton;
    private TurnTimer _turnTimer;
    private PoolController _poolController;
    private CardFactory _cardFactory;
    private CardMovementHandler _cardMovementHandler;
    private IInputHandler _inputHandler;
    private ICard _playerCard;
    private ICard _opponentCard;
    private GameController _gameController;
    private EndBattleDisplay _endBattleDisplay;
    private PlayerController _userPlayerController;
    private PlayerController _opponentPlayerController;
    private Player _player, _opponent;
    

    private void Awake()
    {
        _endTurnButton.gameObject.SetActive(false);
        _useSkillButton.gameObject.SetActive(false);
        _skillPresenter.gameObject.SetActive(false);
    }

    [Inject]
    public void Constructor(SettingsHolderSO settingsHolderSo ,EndBattleDisplay endBattleDisplay, IInputHandler inputHandler,PoolController poolController, CardFactory cardFactory , GameController gameController , CardMovementHandler cardMovementHandler)
    {
        _endBattleDisplay = endBattleDisplay;
        _inputHandler = inputHandler;
        _cardMovementHandler = cardMovementHandler;
        _poolController = poolController;
        _cardFactory = cardFactory;
        _gameController = gameController;
        _turnTimer = new TurnTimer(settingsHolderSo.TurnDuration);
        _endTurnButton.onClick.AddListener(EndTurnClicked);
        _useSkillButton.onClick.AddListener(UseSkill);
        _turnTimer.OnTimeOut += () =>
        {       
            _cardMovementHandler.AutoPlay(() =>
            {   
                EndTurnClicked();
            });
        };
    }

    public void StartBattle(Player player , Player opponent)
    {   
        _player = player;
        _opponent = opponent;
        _userPlayerController = new PlayerController(_player, _playerDisplay);
        _opponentPlayerController = new PlayerController(_opponent, _opponentDisplay);
        _player.AllCardsPlayed += EndBattle;
        _player.OnDead += EndBattle;
        _opponent.OnDead += EndBattle;
        StartTurn();
        Show();
    }
    private void EndTurnClicked()
    {       
        _inputHandler.ActivateOrDeactivateInput(false);
        _endTurnButton.interactable = false;
        _useSkillButton.interactable = false;
        _turnTimer.EnableProcess(false);
        ShowOpponentCard();
    }
    private void UpdatePlayerSkill(string header , string description)
    {
        _skillPresenter.UpdateSkill(header ,description);
    }

    private void UseSkill()
    {   
        _player.CurrentSkill.Apply(_player , _opponent);
        _useSkillButton.gameObject.SetActive(false);
        _skillPresenter.gameObject.SetActive(false);
    }
    
    private void Show()
    {   
        _endTurnButton.interactable = false;
        _playerSlot.gameObject.SetActive(true);
        _opponentSlot.gameObject.SetActive(true);
        _playerDisplay.gameObject.SetActive(true);
        _opponentDisplay.gameObject.SetActive(true);
        _skillPresenter.gameObject.SetActive(true);
        _timerDisplay.gameObject.SetActive(true);
        _endTurnButton.gameObject.SetActive(true);
        _useSkillButton.gameObject.SetActive(true);
        _useSkillButton.interactable = true;
        _turnTimer.EnableProcess(true);
        UpdatePlayerSkill(_player.CurrentSkill.SkillName , _player.CurrentSkill.SkillDescription);
    }

    public void OnCardPlaced(ICard card)
    {   
        _userPlayerController.SwitchCard(card);
        _playerCard = card;
        _endTurnButton.interactable = true;
        _useSkillButton.interactable = true;
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

    private IEnumerator HandleBattle()
    {   
        _opponent.GetReadyTurn();
        _player.GetReadyTurn();
        yield return new WaitForSeconds(2f);
        BattleCalculator.CalculateBattle(_player , _opponent);
        StartCoroutine(ClearCardsAndStart());
    }

    private IEnumerator ClearCardsAndStart()
    {
        yield return new WaitForSeconds(3f);
        _poolController.ReturnToPool(_playerCard.CardTr.transform.GetComponent<IPooledObject>());
        _poolController.ReturnToPool(_opponentCard.CardTr.transform.GetComponent<IPooledObject>());
        yield return new WaitForSeconds(0.5f);
        if(_opponent.CurrentCard == null)
            EndBattle();
        
        _opponent.ProcessTurn();
        _player.ProcessTurn();
        _opponentCard = null;
        _playerCard = null;
        if(_opponent.CurrentCard == null)
            EndBattle();
        else
        {
            StartTurn();
        }
      
    }
    private void StartTurn()
    {
        _useSkillButton.interactable = true;
        _playerSlot.UnHighLight();
        _inputHandler.ActivateOrDeactivateInput(true);
        _turnTimer.ResetTimer();
        _turnTimer.EnableProcess(true);
        CreateOpponentCard(_opponent.CurrentCard);
        _cardMovementHandler.ClearCardSelections();
    }
  
    
    private void CreateOpponentCard(Card card)
    {   
        if(_opponentCard != null) return;
        
        _opponentCard = _poolController.GetFromPool(PoolType.CardDisplay).PoolObj.GetComponent<CardDisplay>();
        _opponentPlayerController.SwitchCard(_opponentCard);
        _opponentCard.CardTr.position = _cardSpawnPos.position;
        _opponentCard.Initialize(_cardFactory.GetSprite(card.Name ),card);
    }
    private void ShowOpponentCard()
    {   
        _opponentPlayerController.SwitchCard(_opponentCard);
        Debug.Log("Show Opponent Card");
        _opponentCard.CardTr.DOMove(_opponentSlot.SlotPlacePoint.position, 0.3f).OnComplete(() =>
        {
            StartCoroutine(HandleBattle());
        });
    }

    private void CloseGameUi()
    {
        _useSkillButton.gameObject.SetActive(false);
        _skillPresenter.gameObject.SetActive(false);
        _endTurnButton.gameObject.SetActive(false);
        _playerDisplay.gameObject.SetActive(false);
        _opponentDisplay.gameObject.SetActive(false);
    }
    private void EndBattle()
    {   
        CloseGameUi();
        var battleResult = BattleCalculator.CalculateBattleResult(_player, _opponent);
        Debug.Log(battleResult);
        _endBattleDisplay.ShowBattleEnd(battleResult);
    }
}
