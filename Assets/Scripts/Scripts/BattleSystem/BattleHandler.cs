using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using InputSystem;
using PoolingSystem;
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

    [Inject]
    public void Constructor(EndBattleDisplay endBattleDisplay, IInputHandler inputHandler,PoolController poolController, CardFactory cardFactory , GameController gameController , CardMovementHandler cardMovementHandler)
    {
        _endBattleDisplay = endBattleDisplay;
        _inputHandler = inputHandler;
        _cardMovementHandler = cardMovementHandler;
        _poolController = poolController;
        _cardFactory = cardFactory;
        _gameController = gameController;
       
        _turnTimer = new TurnTimer(8);
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
    
    private void EndTurnClicked()
    {       
        _endTurnButton.interactable = false;
        _useSkillButton.interactable = false;
        _turnTimer.EnableProcess(false);
        ShowOpponentCard();
    }
    public void UpdatePlayerSkill(string header , string description)
    {
        _skillPresenter.UpdateSkill(header ,description);
    }

    private void UseSkill()
    {   
        _gameController.Player.CurrentSkill.Apply(_gameController.Player , _gameController.Opponent);
        _useSkillButton.gameObject.SetActive(false);
    }
    public void Show()
    {   
        _endTurnButton.interactable = false;
        _playerSlot.gameObject.SetActive(true);
        _opponentSlot.gameObject.SetActive(true);
        _playerDisplay.gameObject.SetActive(true);
        _opponentDisplay.gameObject.SetActive(true);
        _skillPresenter.gameObject.SetActive(true);
        _timerDisplay.gameObject.SetActive(true);
        _turnTimer.EnableProcess(true);
       _gameController.Player.OnSkillEffectUsed += OnSkillUsed;
        _gameController.Opponent.OnSkillEffectUsed+= OnSkillUsed;
        _gameController.Opponent.OnDead += EndBattle;
        _gameController.Opponent.AllCardsPlayed += EndBattle;
        _gameController.Player.OnDead += EndBattle;
        StartTurn();
    }

    public void OnCardPlaced(ICard card)
    {   
        _playerCard = card;
        _gameController.Player.CurrentCard = card.Card;
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

    private void HandleBattle()
    {   
        _gameController.Opponent.GetReadyTurn();
        _gameController.Player.GetReadyTurn();
        PlayerCheckDamage();
        OpponentCheckDamage();
        StartCoroutine(ClearCardsAndStart());
    }

    private IEnumerator ClearCardsAndStart()
    {
       // _opponentCard.CardTr.DOMove(_battlePos.position, 0.2f);
       // _playerCard.CardTr.DOMove(_battlePos.position, 0.2F);
        yield return new WaitForSeconds(3f);
        _poolController.ReturnToPool(_playerCard.CardTr.transform.GetComponent<IPooledObject>());
        _poolController.ReturnToPool(_opponentCard.CardTr.transform.GetComponent<IPooledObject>());
        yield return new WaitForSeconds(0.5f);
        if(_gameController.Opponent.CurrentCard == null)
            EndBattle();
        _gameController.Opponent.ProcessTurn();
        _gameController.Player.ProcessTurn();
        _opponentCard = null;
        _playerCard = null;
        if(_gameController.Opponent.CurrentCard == null)
            EndBattle();
        else
        {
            StartTurn();
        }
      
    }

    private void OnSkillUsed(SkillEffect skill , Player player)
    {       
        Debug.Log("Skill Used" + skill.EffectType);
        var isOpponent = false;
        if (player == _gameController.Opponent) isOpponent = true;
        else
        {
            _useSkillButton.interactable = false;
            _skillPresenter.HideSkill();
        }
        switch (skill.EffectType)
        {
            case SkillEffectType.Shield:
                if (isOpponent)
                {
                    _opponentDisplay.ApplyEffect();
                }
                else
                {   
                    _playerDisplay.ApplyEffect();
                }
                break;
            case SkillEffectType.IncreaseAttack:
            case SkillEffectType.IncreaseDefense:
                if (isOpponent)
                {
                    _opponentCard.ApplySkillEffect(skill);
                }
                else
                {
                    _playerCard.ApplySkillEffect(skill);
                }
                break;
            case SkillEffectType.IncreaseHealth:
                if (isOpponent)
                {
                    var ratio = (float)_gameController.Opponent.Health /(float) _gameController.Opponent.StartHealth;
                    _opponentDisplay.UpdatePlayerHealth(_gameController.Opponent.Health , ratio);
                }
                else
                {   
                    var ratio = (float)_gameController.Player.Health /(float) _gameController.Player.StartHealth;
                    _opponentDisplay.UpdatePlayerHealth(_gameController.Player.Health , ratio);
                }
                break;
            case SkillEffectType.DecreaseOpponentAttack:
            case  SkillEffectType.DecreaseOpponentDefense:
                if (isOpponent)
                {
                    _playerCard.ApplySkillEffect(skill);
                }
                else
                {
                    _opponentCard.ApplySkillEffect(skill);
                }
                break;
        }
    }

    private void StartTurn()
    {  
        _playerSlot.UnHighLight();
        _inputHandler.ActivateOrDeactivateInput(true);
        _turnTimer.ResetTimer();
        _turnTimer.EnableProcess(true);
        CreateOpponentCard(_gameController.Opponent.CurrentCard);
        _cardMovementHandler.ClearCardSelections();
    }
    private void PlayerCheckDamage()
    {
        var player = _gameController.Player;
        var playerHealth = player.Health;
        BattleCalculator.CalculateBattle(_gameController.Opponent , player);
        Debug.Log("Player Health" + player.Health);
        var ratio = (float)(playerHealth) / (float)player.StartHealth;
        _playerDisplay.UpdatePlayerHealth(playerHealth , ratio);
        
    }
    private void OpponentCheckDamage()
    {
        var player = _gameController.Opponent;
        var playerHealth = player.Health;
        BattleCalculator.CalculateBattle(_gameController.Player , player);
        var ratio = (float)player.Health / (float)(player.StartHealth);
        _opponentDisplay.UpdatePlayerHealth(playerHealth , ratio);
    }

    private void CreateOpponentCard(Card card)
    {   
        if(_opponentCard != null) return;
        _opponentCard = _poolController.GetFromPool(PoolType.CardDisplay).PoolObj.GetComponent<CardDisplay>();
        _opponentCard.CardTr.position = _cardSpawnPos.position;
        _opponentCard.Initialize(_cardFactory.GetSprite(card.Name ),card);
    }
    private void ShowOpponentCard()
    {   
        Debug.Log("Show Opponent Card");
        _opponentCard.CardTr.DOMove(_opponentSlot.SlotPlacePoint.position, 0.3f).OnComplete(() =>
        {
            HandleBattle();
        });
    }

    private void EndBattle()
    {
        var battleResult = BattleCalculator.CalculateBattleResult(_gameController.Player, _gameController.Opponent);
        Debug.Log(battleResult);
        _endBattleDisplay.ShowBattleEnd(battleResult);
    }
}
