public class DecreaseOpponentAttackSkill : Skill
{
    private int _attackReduction;
    

    public override void Apply(Player player, Player opponent)
    {
        opponent.CurrentCard.SetAttackValue(opponent.CurrentCard.Attack- _attackReduction);
    }

    public DecreaseOpponentAttackSkill(int attackReduction ,string name, string description) : base(name, description)
    {
        _attackReduction = attackReduction;
    }
}