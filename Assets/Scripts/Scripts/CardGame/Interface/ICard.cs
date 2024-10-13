using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard 
{
    Card Card { get; }
    void Attack(ICard target); 
    void TakeDamage(int amount);
    void SetHealth(int health);
    void SetAttack(int attack);
    Transform CardTr { get; }
}
