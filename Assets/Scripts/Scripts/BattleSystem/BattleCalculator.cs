using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleCalculator 
{
    public static void CalculateBattle(Player player, Player opponent)
    {
        var playerAttackPower = player.CurrentCard.Attack;
        var opponentAttackPower = opponent.CurrentCard.Attack;
        var playerDeffensePower = player.CurrentCard.Defense;
        var opponentDeffensePower = opponent.CurrentCard.Defense;
        opponent.CurrentCard.IncreaseOrDeccreaseDeffenseValue(-playerAttackPower);
        opponent.CurrentCard.OnDamageTaken?.Invoke(playerAttackPower);
        player.CurrentCard.IncreaseOrDeccreaseDeffenseValue(-opponentAttackPower);
        player.CurrentCard.OnDamageTaken?.Invoke(opponentAttackPower);
        var playerTakeDamage = opponentAttackPower - playerDeffensePower;
        var opponentTakeDamage = playerAttackPower - opponentDeffensePower;
        if (opponentTakeDamage >0)
        {
            opponent.TakeDamage(opponentTakeDamage);
        }

        if (playerTakeDamage > 0)
        {
            player.TakeDamage(playerTakeDamage);
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
