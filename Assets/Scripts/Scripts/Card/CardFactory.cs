using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CardFactory
{
    [Inject] private CardsHolderSo _cardsHolderSo;

    private List<Card> _cards = new List<Card>();
    private Dictionary<string, Sprite> _spriteDic = new Dictionary<string, Sprite>();

    public void Initialize()
    {   
        foreach (var item in _cardsHolderSo.Cards)
        {
            var card = new Card(item.CardName, item.Attack, item.Defense);
            _cards.Add(card);
            _spriteDic.Add(item.CardName , item.CardImage);
        }
    }

    public void ResetCards()
    {
        
    }
    
    public Sprite GetSprite(string name)
    {
        return _spriteDic[name];
    }
    public List<Card> GetUniqueRandomCards(int count)
    {
        if (_cards == null || _cards.Count == 0)
        {
            Debug.LogError("Card list is empty. Cannot return unique random cards.");
            return new List<Card>(); // Return an empty list
        }

        count = Mathf.Min(count, _cards.Count);
        List<Card> shuffledCards = new List<Card>(_cards);
        Shuffle(shuffledCards);
        return shuffledCards.GetRange(0, count);
    }
    
    private void Shuffle(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Card temp = cards[i];
            int randomIndex = Random.Range(i, cards.Count);
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

}
