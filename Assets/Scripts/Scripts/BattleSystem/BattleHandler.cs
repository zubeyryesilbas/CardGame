using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PoolingSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private BaseSlot _playerSlot, _opponentSlot;
    [SerializeField] private Transform _cardSpawnPos;
    [SerializeField] private BasicHealthBar _playerBar, _opponentBar;
    [SerializeField] private SkillPresenter _skillPresenter;
    [SerializeField] private TimerDisplay _timerDisplay;
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private Button _useSkillButton;
    private TurnTimer _turnTimer;
    private PoolController _poolController;
    private CardFactory _cardFactory;
    private CardMovementHandler _cardMovementHandler;
    private ICard _playerCard;
    private ICard _opponentCard;
    private GameController _gameController;

    [Inject]
    public void Constructor(PoolController poolController, CardFactory cardFactory , GameController gameController , CardMovementHandler cardMovementHandler)
    {
        _cardMovementHandler = cardMovementHandler;
        _poolController = poolController;
        _cardFactory = cardFactory;
        _endTurnButton.onClick.AddListener(EndTurnClicked);
        _gameController = gameController;
        _turnTimer = new TurnTimer(15);
        _endTurnButton.onClick.AddListener(EndTurnClicked);
        _useSkillButton.onClick.AddListener(UseSkill);
        _turnTimer.OnTimeOut += () =>
        {       
            _cardMovementHandler.AutoPlay(() =>
            {
                ShowOpponentCard();
            });
           
           
        };
    }
    
    private void EndTurnClicked()
    {
        _endTurnButton.interactable = false;
        _useSkillButton.interactable = false;
        ShowOpponentCard();
    }
    public void UpdatePlayerSkill(string header , string description)
    {
        _skillPresenter.UpdateSkill(header ,description);
    }

    private void UseSkill()
    {   
        _gameController.Player.CurrentSkill.Apply(_gameController.Player , _gameController.Opponent);
    }
    public void Show()
    {   
        _endTurnButton.interactable = false;
        _playerSlot.gameObject.SetActive(true);
        _opponentSlot.gameObject.SetActive(true);
        _playerBar.gameObject.SetActive(true);
        _opponentBar.gameObject.SetActive(true);
        _skillPresenter.gameObject.SetActive(true);
        _timerDisplay.gameObject.SetActive(true);
        _turnTimer.EnableProcess(true);
        CreateOpponentCard(_gameController.Opponent.GetCurrentCard());
        _gameController.Player.CurrentSkill.OnSkillUsed += OnSkillUsed;
        _gameController.Opponent.CurrentSkill.OnSkillUsed += OnSkillUsed;
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
        PlayerTakeDamage(_opponentCard.Card.Attack);
        OpponentTakeDamage(_playerCard.Card.Attack);
    }

    private void OnSkillUsed(SkillEffect[]skills , Player player)
    {   
        Debug.Log("Skill Used asadaaxasx");
        var isOpponent = false;
        if (player == _gameController.Opponent) isOpponent = true;
        else
        {
            isOpponent = false;
        }
        foreach (var item in skills)
        {
            switch (item.EffectType)
            {
                case SkillEffectType.Shield:
                    if (isOpponent)
                    {
                        Debug.Log("Opponenet take shield" + item.EffectValue);
                    }
                    else
                    {
                        Debug.Log("Player Take Shield" + item.EffectValue);
                    }
                    break;
                case SkillEffectType.IncreaseAttack:
                case SkillEffectType.IncreaseDefense:
                    if (isOpponent)
                    {
                        _opponentCard.ApplySkillEffects(skills);
                    }
                    else
                    {
                        _playerCard.ApplySkillEffects(skills);
                    }
                    break;
                case SkillEffectType.IncreaseHealth:
                    if (isOpponent)
                    {
                        _opponentCard.ApplySkillEffects(skills);
                    }
                    else
                    {   
                        _playerCard.ApplySkillEffects(skills);
                    }
                    break;
                case SkillEffectType.DecreaseOpponentAttack:
                case  SkillEffectType.DecreaseOpponentDefense:
                    if (isOpponent)
                    {
                        _playerCard.ApplySkillEffects(skills);
                    }
                    else
                    {
                        _opponentCard.ApplySkillEffects(skills);
                    }
                    break;
            }
        }
    }
    
    
    private void PlayerTakeDamage(int amount)
    {
        var player = _gameController.Player;
        var playerHealth = player.Health;
        player.TakeDamage(amount);
        _playerBar.SetValue((float)(playerHealth) /(float)player.Health);
        
    }
    private void OpponentTakeDamage(int amount)
    {
        var player = _gameController.Opponent;
        var playerHealth = player.Health;
        player.TakeDamage(amount);
        _opponentBar.SetValue((float)(playerHealth) /(float)player.Health);
    }

    private void CreateOpponentCard(Card card)
    {
        _opponentCard = _poolController.GetFromPool(PoolType.CardDisplay).PoolObj.GetComponent<CardDisplay>();
        _opponentCard.CardTr.position = _cardSpawnPos.position;
        _opponentCard.Initialize(_cardFactory.GetSprite(card.Name ),card);
    }
    private void ShowOpponentCard()
    {
        _opponentCard.CardTr.DOMove(_opponentSlot.SlotPlacePoint.position, 0.3f).OnComplete(() =>
        {
            HandleBattle();
        });
        
    }
}
