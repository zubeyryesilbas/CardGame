using UnityEngine;

namespace SkillSystem
{
    public class DecreaseOpponentAttackSkill : Skill
    {
        private int _attackReduction;
    
        public DecreaseOpponentAttackSkill(int attackReduction ,string name, string description, SkillEffect[] effects) : base(name, description, effects)
        {
            _attackReduction = attackReduction;
        }
        public override void Apply(Player player, Player opponent)
        {       
            base.Apply(player,opponent);
        }

    
    }
}
