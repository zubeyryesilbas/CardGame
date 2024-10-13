using UnityEngine;

public class DecreaseOpponentDefenseSkill : Skill
{
    private int _defenseReduction;
    public DecreaseOpponentDefenseSkill(int defenseReduction,string name, string description, SkillFactory skillFactory) : base(name, description, skillFactory)
    {
        _defenseReduction = defenseReduction;
    }
    public override void Apply(Player player, Player opponent)
    {   
        base.Apply(player,opponent);
        Debug.Log("Decrease Opponent Deffense");
        opponent.CurrentCard.SetDeffenseValue(opponent.CurrentCard.Defense - _defenseReduction);
    }


    
}