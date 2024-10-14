using System.Collections.Generic;
using SkillSystem;
using UnityEngine;

public class OpponentPlayer : Player
{
    public OpponentPlayer(int health, List<Card> cards, Skill skill) : base(health, cards, skill)
    {
        CurrentCard = GetCurrentCard(); // played cards removes while processing turn and opponent card determined randomly
    }
    
    public override void ProcessTurn()
    {   
        base.ProcessTurn();
        PlayCard();
        CurrentCard = GetCurrentCard();
    }

    private Card GetCurrentCard()
    {   
        var cardCount = Deck.Count;
        Debug.Log("Card Count "+ cardCount);
        var random = Random.Range(0, cardCount);
        var card = Deck[random];
        return card;
    }
}