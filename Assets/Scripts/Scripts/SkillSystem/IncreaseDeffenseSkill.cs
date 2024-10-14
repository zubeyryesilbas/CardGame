using UnityEngine;

namespace SkillSystem
{
    public class IncreaseDefenseSkill : Skill
    {
        public IncreaseDefenseSkill(string name, string description, SkillEffect[]effects) : base(name, description, effects)
        {
           
        }
    
        public override void Apply(Player player, Player opponent)
        {
            base.Apply(player, opponent);
        }
    }
}
