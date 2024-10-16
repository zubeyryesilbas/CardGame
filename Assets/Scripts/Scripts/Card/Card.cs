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
        Attack += attack;
    }

    public void IncreaseOrDeccreaseDeffenseValue(int deffense)
    {   
        Defense += deffense;
    }
}