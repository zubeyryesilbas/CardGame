using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private Player _player;
    private PlayerDisplay _playerDisplay;
    private ICard _playerCard;

    public  PlayerController(Player player, PlayerDisplay playerDisplay)
    {
        _player = player;
        _playerDisplay = playerDisplay;
        _player.OnSkillEffectUsed += OnSkillUsed;
        _player.OnProcessTurn += ProcessTurn;
        _player.OnDamageTaken += UpdateHealth;
    }

    private void ProcessTurn()
    {
        _playerDisplay.DiscardEffect();
    }

    public void UpdateHealth(int damage)
    {
        var ratio =(float) _player.Health / (float)_player.StartHealth;
        Debug.Log("Health" +_player.Health);
        _playerDisplay.UpdatePlayerHealth(_player.Health,ratio);
    }
    public void SwitchCard(ICard card)
    {
        _playerCard = card;
        _player.CurrentCard = card.Card;
    }
    
    private void OnSkillUsed(SkillEffect skill , bool isOpponent)
    {       
        switch (skill.EffectType)
        {   
            case SkillEffectType.Shield:
                _playerDisplay.ApplyEffect(skill);
                break;
            case SkillEffectType.IncreaseHealth:
                var ratio = (float)_player.Health /(float) _player.StartHealth;
                _playerDisplay.UpdatePlayerHealth(_player.Health , ratio);
                break;
            case SkillEffectType.OpponentAttackBoostNextTurn:
            case SkillEffectType.IncreaseAttack:
            case SkillEffectType.IncreaseDefense:
            case SkillEffectType.DecreaseOpponentAttack:
            case  SkillEffectType.DecreaseOpponentDefense:
                    _playerCard.ApplySkillEffect(skill);
                break;
        }
    }
}
