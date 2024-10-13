public class DecreaseOpponentDefenseSkill : Skill
{
    private int _defenseReduction;
    public DecreaseOpponentDefenseSkill( int defenseReduction ,string name, string description) : base(name, description)
    {
        _defenseReduction = defenseReduction;
    }
    
    public override void Apply(Player player, Player opponent)
    {
        opponent.CurrentCard.SetDeffenseValue(opponent.CurrentCard.Defense - _defenseReduction);
    }

  
}