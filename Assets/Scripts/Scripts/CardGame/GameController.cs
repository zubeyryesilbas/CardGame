using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameController : MonoBehaviour
{
    private DeckDisplay _deckDisplay;
    private SkillFactory _skillFactory;
    private CardFactory _cardFactory;
    private Player _player, _opponent;
    private CardLayoutCrator _cardLayoutCrator;
    private PlayersDisplay _playersDisplay;
    private IInputHandler _inputHandler;
    [SerializeField] private Button _endTurnButton;

    [Inject]
    public void Constructor( IInputHandler inputHandler ,PlayersDisplay playersDisplay,DeckDisplay deckDisplay , SkillFactory skillFactory , CardFactory cardFactory , CardLayoutCrator cardLayoutCrator)
    {
        _deckDisplay = deckDisplay;
        _skillFactory = skillFactory;
        _cardFactory = cardFactory;
        _cardLayoutCrator = cardLayoutCrator;
        _playersDisplay = playersDisplay;
        _inputHandler = inputHandler;
    }
    public void StartGame(List<Card> cards)
    {
        _player = new UserPlayer(100, cards , _skillFactory.GetRandomSkill());
        _opponent = new OpponentPlayer(100, _cardFactory.GetUniqueRandomCards(6), _skillFactory.GetRandomSkill());
        _deckDisplay.CreateCardDisplays(cards);
        _playersDisplay.Show();
        _inputHandler.SwitchDragging(true);
        _playersDisplay.UpdatePlayerSkill(_player.CurrentSkill.SkillName , _player.CurrentSkill.SkillDescription);
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
        _endTurnButton.interactable = true;
        Debug.LogError(card);
        if (card != null)
        {
            _player.SelectCard(card);
        }
    }

    public void PlayerDeselectCard()
    {
        _endTurnButton.interactable = false;
    }

    public void EndTurn()
    {
        
    }
}
