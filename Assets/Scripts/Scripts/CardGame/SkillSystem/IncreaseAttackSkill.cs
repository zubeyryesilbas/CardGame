public class IncreaseAttackSkill : Skill
{
    private int _attackBoost;

   

    public override void Apply(Player player, Player opponent)
    {
        player.CurrentCard.SetAttackValue(player.CurrentCard.Attack + _attackBoost);
    }

    public IncreaseAttackSkill(int attackBoost ,string name, string description) : base(name, description)
    {
        _attackBoost = attackBoost;
    }
}