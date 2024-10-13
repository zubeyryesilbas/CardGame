using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck 
{
    private List<Card> _cards = new List<Card>();

    public void AddCard(Card card)
    {
        _cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        _cards.Remove(card);
    }

    public List<Card> GetCards()
    {
        return _cards;
    }
}
