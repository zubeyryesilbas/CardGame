using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SkillSystem;
public class GameController : MonoBehaviour
{
    private DeckDisplay _deckDisplay;
    private SkillFactory _skillFactory;
    private CardFactory _cardFactory;
    private Player _player, _opponent;
    public Player Opponent => _opponent;
    public Player Player => _player;
    private CardLayoutCreator _cardLayoutCrator;
    private BattleHandler _battleHandler;
    private IInputHandler _inputHandler;

    [Inject]
    public void Constructor( IInputHandler inputHandler ,BattleHandler battleHandler,DeckDisplay deckDisplay , SkillFactory skillFactory , CardFactory cardFactory , CardLayoutCreator cardLayoutCreator)
    {
        _deckDisplay = deckDisplay;
        _skillFactory = skillFactory;
        _cardFactory = cardFactory;
        _cardLayoutCrator = cardLayoutCreator;
        _battleHandler = battleHandler;
        _inputHandler = inputHandler;
    }

   
    public void StartGame(List<Card> cards)
    {
        _player = new UserPlayer(100, cards , _skillFactory.GetRandomSkill());
        _opponent = new OpponentPlayer(100, _cardFactory.GetUniqueRandomCards(6), _skillFactory.GetRandomSkill());
        _deckDisplay.CreateCardDisplays(cards);
        _battleHandler.Show();
        _inputHandler.SwitchDragging(true);
        _battleHandler.UpdatePlayerSkill(_player.CurrentSkill.SkillName , _player.CurrentSkill.SkillDescription);
    }

    public void OnSkillUsed(Player player)
    {
        if (_player == player)
        {
            var skill = _player.CurrentSkill;
        }
    }

    public void PlayerCardSelected(string card)
    {
        Debug.LogError(card);
        if (card != null)
        {
            _player.SelectCard(card);
        }
    }
    
    public void EndTurn()
    {
           
    }
}
