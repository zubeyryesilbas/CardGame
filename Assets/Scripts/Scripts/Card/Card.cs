using System;
using UnityEngine;

public class Card
{
    public string Name { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public Action<int> OnDamageTaken;

    public Card(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }
    
    public void IncreaseOrDecreaseAttackValue(int attack)
    {   
        Debug.LogError(Attack+"Deffense Increase" + attack);
        Attack += attack;
        Debug.LogError(Attack+"Final Attack");
     
    }

    public void IncreaseOrDeccreaseDeffenseValue(int deffense)
    {   
        Defense += deffense;
    }
}