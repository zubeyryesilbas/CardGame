using UnityEngine;

public class IncreaseHealthSkill : Skill
{
    private int _healthBoost;
    
    public IncreaseHealthSkill(int healthBoost ,string name, string description, SkillFactory skillFactory) : base(name, description, skillFactory)
    {
        _healthBoost = healthBoost;
    }
    public override void Apply(Player player, Player opponent)
    {       
        base.Apply(player , opponent);
        Debug.Log("Increase Health");
        player.Health += _healthBoost;
    }


    
}

