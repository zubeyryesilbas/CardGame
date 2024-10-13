using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPlayer : Player
{
    public UserPlayer(int health, List<Card> cards, Skill skill) : base(health, cards, skill) { }
    
    public override void ProcessTurn()
    {
        if (CurrentCard != null)
        {
            PlayCard();
            Debug.Log($"Player played {CurrentCard.Name}");
        }
    }
}
