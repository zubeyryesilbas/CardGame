

namespace SkillSystem
{
    using UnityEngine;
    public class IncreaseHealthSkill : Skill
    {
       
    
        public IncreaseHealthSkill(string name, string description, SkillEffect [] effects) : base(name, description, effects)
        {
           
        }
        public override void Apply(Player player, Player opponent)
        {       
            base.Apply(player , opponent);
        }
    
    }
}


