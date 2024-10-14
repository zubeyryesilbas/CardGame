using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillCalculator 
{
    public static void AddSkillEffects( SkillEffect [] effects,Player userplayer, Player opponentPlayer)
    {
        foreach (var item in effects)
        {
            switch (item.EffectType)
            {
                case SkillEffectType.Shield:
                case SkillEffectType.IncreaseAttack:
                case SkillEffectType.IncreaseDefense:
                case SkillEffectType.IncreaseHealth:
                    userplayer.AddSkillEffect(item);
                    break;
                case SkillEffectType.OpponentAttackBoostNextTurn:
                case SkillEffectType.DecreaseOpponentDefense:
                case SkillEffectType.DecreaseOpponentAttack:
                    opponentPlayer.AddSkillEffect(item);
                    break;
            }
        }
    }

    
}
