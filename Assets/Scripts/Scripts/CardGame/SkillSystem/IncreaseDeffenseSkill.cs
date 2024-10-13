using UnityEngine;

public class IncreaseDefenseSkill : Skill
{
    private int _defenseBoost;
    public IncreaseDefenseSkill(int defenseBoost,string name, string description, SkillFactory skillFactory) : base(name, description, skillFactory)
    {
        _defenseBoost = defenseBoost;
    }
    
    public override void Apply(Player player, Player opponent)
    {   
        base.Apply(player , opponent);
        player.CurrentCard.SetDeffenseValue(player.CurrentCard.Defense + _defenseBoost);
    }


    
}