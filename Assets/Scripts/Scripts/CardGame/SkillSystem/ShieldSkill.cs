public class ShieldSkill : Skill
{
    private int _shieldAmount;
    private int _opponentAttackIncrease;
    
    public ShieldSkill(int shieldAmount , int opponentAttackIncrease ,string name, string description) : base(name, description)
    {
        _shieldAmount = shieldAmount;
        _opponentAttackIncrease = opponentAttackIncrease;
    }

    public override void Apply(Player player, Player opponent)
    {
        player.Shield += _shieldAmount;
        opponent.AttackBoostNextTurn += _opponentAttackIncrease;
    }

   
}