using UnityEngine;

public class ShieldSkill : Skill
{
    private int _shieldAmount;
    private int _opponentAttackIncrease;
    public ShieldSkill(int shieldAmount ,int opponentAttackIncrease,string name, string description, SkillFactory skillFactory) : base(name, description, skillFactory)
    {
        _shieldAmount = shieldAmount;
        _opponentAttackIncrease = opponentAttackIncrease;
    }
    public override void Apply(Player player, Player opponent)
    {   
        base.Apply(player,opponent);
        Debug.Log("Shield Skill Used");
        player.Shield += _shieldAmount;
        opponent.AttackBoostNextTurn += _opponentAttackIncrease;
    }
    
}