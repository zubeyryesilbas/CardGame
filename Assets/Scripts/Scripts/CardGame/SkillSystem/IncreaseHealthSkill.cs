public class IncreaseHealthSkill : Skill
{
    private int _healthBoost;

    public IncreaseHealthSkill(int healthBoost,string name, string description) : base(name, description)
    {
        _healthBoost = healthBoost;
    }

    public override void Apply(Player player, Player opponent)
    {
        player.Health += _healthBoost;
    }

    
}

