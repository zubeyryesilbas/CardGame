using UnityEngine;

namespace SkillSystem
{
    public class IncreaseAttackSkill : Skill
    {
        public IncreaseAttackSkill(string name, string description, SkillEffect[] effects) : base(name, description, effects)
        {
           
        }
        public override void Apply(Player player, Player opponent)
        {   
            base.Apply(player,opponent);
        }
    }
}
