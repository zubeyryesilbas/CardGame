using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleCalculator 
{
    public static void CalculateBattle(Player player, Player opponent)
    {
        var playerAttackPower = player.CurrentCard.Attack;
       
        var opponentDeffensePower = opponent.CurrentCard.Defense;

        var opponentTakeDamage = playerAttackPower - opponentDeffensePower;
        if (opponentTakeDamage >= 0)
        {
            opponent.TakeDamage(opponentTakeDamage);
        }
    }

    public static BattleResult CalculateBattleResult(Player player, Player opponent)
    {
        BattleResult battleResult;
        var healthDifference =  player.Health - opponent.Health;
        Debug.Log("Player Health : "+ player.Health +"," +"Opponent Health : " + opponent.Health);
        if (healthDifference >0)
        {
            battleResult = BattleResult.Win;
          
        }
        else if(healthDifference < 0)
        {
            battleResult = BattleResult.Lost;
        }
        else 
        {
            battleResult = BattleResult.Draw;
        }
        

        return battleResult;
    }
}
