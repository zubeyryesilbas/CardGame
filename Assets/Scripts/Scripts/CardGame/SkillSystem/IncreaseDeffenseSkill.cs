public class IncreaseDefenseSkill : Skill
{
    private int _defenseBoost;
    
    public override void Apply(Player player, Player opponent)
    {
        player.CurrentCard.SetDeffenseValue(player.CurrentCard.Defense + _defenseBoost);
    }

    public IncreaseDefenseSkill(int defenseBoost, string name, string description) : base(name, description)
    {
        _defenseBoost = defenseBoost;
    }
}