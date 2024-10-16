using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using SkillSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private DeckDisplay _deckDisplay;
    private SkillFactory _skillFactory;
    private CardFactory _cardFactory;
    private BattleHandler _battleHandler;
    private IInputHandler _inputHandler;
    private SettingsHolderSO _settingsHolderSo;

    [Inject]
    public void Constructor( SettingsHolderSO settingsHolderSo ,IInputHandler inputHandler ,BattleHandler battleHandler,DeckDisplay deckDisplay , SkillFactory skillFactory , CardFactory cardFactory )
    {
        _settingsHolderSo = settingsHolderSo;
        _deckDisplay = deckDisplay;
        _skillFactory = skillFactory;
        _cardFactory = cardFactory;
        _battleHandler = battleHandler;
        _inputHandler = inputHandler;
    }

   
    public void StartGame(List<Card> cards)
    {   
        
       var player= new UserPlayer(_settingsHolderSo.PlayerStartHealth, cards , _skillFactory.GetRandomSkill());
        var opponent = new OpponentPlayer(_settingsHolderSo.PlayerStartHealth, _cardFactory.GetUniqueRandomCards(6), _skillFactory.GetRandomSkill());
        _deckDisplay.CreateCardDisplays(cards);
        _battleHandler.StartBattle(player , opponent);
        _inputHandler.SwitchDragging(true);
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
