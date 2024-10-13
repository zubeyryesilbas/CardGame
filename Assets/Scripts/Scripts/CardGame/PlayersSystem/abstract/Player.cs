using System.Collections.Generic;
using System.Linq;

public abstract class Player
{
    public int Health { get; set; }
    public List<Card> Deck { get; private set; }
    public Skill CurrentSkill { get; private set; }
    
    public Card CurrentCard;
    public int Shield;
    public int AttackBoostNextTurn;

    public Player(int health , List<Card> cards , Skill skill)
    {
        Health = health;
        Deck = new List<Card>(cards);
        CurrentSkill = skill;
    }
    
    public abstract void ProcessTurn();

    public void PlayCard()
    {
        if (CurrentCard == null) return;
        Deck.Remove(CurrentCard); 
    }

    public void SelectCard(string str)
    {
        CurrentCard = Deck.FirstOrDefault(x => x.Name == str);
    }
}