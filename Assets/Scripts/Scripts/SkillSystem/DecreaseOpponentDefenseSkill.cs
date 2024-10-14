using UnityEngine;

namespace SkillSystem
{
    public class DecreaseOpponentDefenseSkill : Skill
    {
       
        public DecreaseOpponentDefenseSkill(string name, string description, SkillEffect[] effects) : base(name, description, effects)
        {
           
        }
        public override void Apply(Player player, Player opponent)
        {   
            base.Apply(player,opponent);
        }
    }
}
