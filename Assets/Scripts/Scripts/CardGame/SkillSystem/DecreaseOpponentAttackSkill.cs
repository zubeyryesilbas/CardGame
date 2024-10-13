using UnityEngine;

public class DecreaseOpponentAttackSkill : Skill
{
    private int _attackReduction;
    
    public DecreaseOpponentAttackSkill(int attackReduction ,string name, string description, SkillFactory skillFactory) : base(name, description, skillFactory)
    {
        _attackReduction = attackReduction;
    }
    public override void Apply(Player player, Player opponent)
    {       
        base.Apply(player,opponent);
        Debug.Log("DecreaseOpponenet Attack");
        opponent.CurrentCard.SetAttackValue(opponent.CurrentCard.Attack- _attackReduction);
    }

    
}