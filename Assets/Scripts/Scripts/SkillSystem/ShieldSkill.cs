using UnityEngine;

namespace SkillSystem
{
    public class ShieldSkill : Skill
    {
        public ShieldSkill(string name, string description, SkillEffect [] effects) : base(name, description,effects)
        {
           
        }
        public override void Apply(Player player, Player opponent)
        {   
            base.Apply(player,opponent);
        }
    }
}
