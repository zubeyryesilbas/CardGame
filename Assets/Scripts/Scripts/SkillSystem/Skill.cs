using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    public abstract class Skill
    {
        public Skill(string name , string description , SkillEffect [] effects)
        {
            SkillName = name;
            SkillDescription = description;
            Effects = effects;
        }
    
        public string SkillName { get; protected set; }
        public string SkillDescription { get; protected set; }

        internal SkillEffect[] Effects;
        //public Action<SkillEffect [] , Player > OnSkillUsed;

        public virtual void Apply(Player player, Player opponent)
        {   
            SkillCalculator.AddSkillEffects(Effects,player,opponent);
        }

        private async void WaitForSkillApply(Player player)
        {
        }
    }
}
