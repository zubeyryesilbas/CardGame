using System.Collections.Generic;
using UnityEngine;

public class OpponentPlayer : Player
{
    public OpponentPlayer(int health, List<Card> cards, Skill skill) : base(health, cards, skill) { }
    
    public override void ProcessTurn()
    {
        if (Deck.Count > 0)
        {
            int randomIndex = Random.Range(0, Deck.Count);
            CurrentCard = Deck[randomIndex];
            PlayCard();
            Debug.Log($"Enemy played {CurrentCard.Name}");
        }
    }

    public override Card GetCurrentCard()
    {
        var cardCount = Deck.Count;
        var random = Random.Range(0, cardCount);
        var card = Deck[random];
        CurrentCard = card;
        Deck.Remove(card);
        return card;
    }
}