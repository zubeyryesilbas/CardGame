using UnityEngine;

public class IncreaseAttackSkill : Skill
{
    private int _attackBoost;

    public IncreaseAttackSkill(int attackBoost ,string name, string description, SkillFactory skillFactory) : base(name, description, skillFactory)
    {
        _attackBoost = attackBoost;
    }
    public override void Apply(Player player, Player opponent)
    {   
        base.Apply(player,opponent);
        Debug.Log("Current Card Attakc");
        player.CurrentCard.SetAttackValue(player.CurrentCard.Attack + _attackBoost);
        
    }
}