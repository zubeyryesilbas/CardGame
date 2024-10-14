using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace SkillSystem
{
    public class SkillFactory
    {
        private static readonly System.Random _random = new System.Random();
        private readonly List<Skill> _cachedSkills;
        private readonly Dictionary<Skill, SkillEffect[]> _skillEffects = new Dictionary<Skill, SkillEffect[]>();

        public SkillEffect[] GetEffectsOfSkill(Skill skill)
        {
            return _skillEffects.TryGetValue(skill, out var effects) ? effects : null;
        }

        public SkillFactory(SkillsHolderSo skillsHolderSo)
        {
            _cachedSkills = new List<Skill>();

            foreach (var skillData in skillsHolderSo.AllSkills)
            {
                if (skillData == null || skillData.Effects == null) continue; // Null check
                
                var skill = CreateSkill(skillData);
                if (skill != null)
                {
                    _cachedSkills.Add(skill);
                    _skillEffects[skill] = skillData.Effects;
                }
            }
        }

        private Skill CreateSkill(SkillHolderSo skillData)
        {
            switch (skillData.SkillType)
            {
                case SkillType.IncreaseHealth:
                    return new IncreaseHealthSkill(skillData.SkillName, skillData.Description, skillData.Effects);
                case SkillType.IncreaseAttack:
                    return new IncreaseAttackSkill(skillData.SkillName, skillData.Description, skillData.Effects);
                case SkillType.IncreaseDeffense:
                    return new IncreaseDefenseSkill(skillData.SkillName, skillData.Description, skillData.Effects);
                case SkillType.DecreaseOpponentAttack:
                    return new DecreaseOpponentAttackSkill(skillData.Effects[0].EffectValue, skillData.SkillName, skillData.Description, skillData.Effects);
                case SkillType.DecreaseOpponentDeffense:
                    return new DecreaseOpponentDefenseSkill(skillData.SkillName, skillData.Description, skillData.Effects);
                case SkillType.ShieldSkill:
                    return new ShieldSkill(skillData.SkillName, skillData.Description, skillData.Effects);
                default:
                    return null; // In case of an unsupported skill type
            }
        }

        public Skill GetRandomSkill()
        {
            if (_cachedSkills.Count == 0) return null; // Avoid invalid access if no skills are cached

            int randomSkillIndex = _random.Next(_cachedSkills.Count);
            return _cachedSkills[randomSkillIndex];
        }
    }
}
