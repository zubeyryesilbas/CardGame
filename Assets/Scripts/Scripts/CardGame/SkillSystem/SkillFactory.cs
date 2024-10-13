using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SkillFactory
{
    private static System.Random _random = new System.Random();
    private List<Skill> _cachedSkills;
    private Dictionary<Skill, SkillEffect[]> _skillEffects = new Dictionary<Skill, SkillEffect[]>();

    public SkillEffect[] GetEffectsOfSkill(Skill skill)
    {
        return _skillEffects[skill];
    }
    public SkillFactory(SkillsHolderSo skillsHolderSo)
    {
        _cachedSkills = new List<Skill>();

        Skill skill = null;
        foreach (var skillData in skillsHolderSo.AllSkills)
        {
            switch (skillData.SkillType)
            { 
                    
                case SkillType.IncreaseHealth :
                    skill = new IncreaseHealthSkill(skillData.Effects[0].EffectValue,
                    skillData.SkillName, skillData.Description , this);
                    break;
                case SkillType.IncreaseAttack:
                    skill = new IncreaseAttackSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName, skillData.Description , this);
                    break;
                case SkillType.IncreaseDeffense:
                    skill = new IncreaseDefenseSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName, skillData.Description , this);
                    break;
                case SkillType.DecreaseOpponentAttack:
                    skill = new DecreaseOpponentAttackSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName, skillData.Description , this);
                    break;
                case SkillType.DecreaseOpponentDeffense:
                    skill = new DecreaseOpponentDefenseSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName, skillData.Description,this);
                    break;
                case SkillType.ShieldSkill:
                    skill = new ShieldSkill(
                        skillData.Effects.FirstOrDefault(x => x.EffectType == SkillEffectType.Shield).EffectValue,
                        skillData.Effects
                            .FirstOrDefault(x => x.EffectType == SkillEffectType.OpponentAttackBoostNextTurn)
                            .EffectValue,
                        skillData.SkillName, skillData.Description , this);
                    break;
                default:
                    break;
            }
            _cachedSkills.Add(skill);
            _skillEffects.Add(skill ,skillData.Effects);
        }
    }

    public Skill GetRandomSkill()
    {
        int randomSkillIndex = _random.Next(_cachedSkills.Count); 
        return _cachedSkills[randomSkillIndex];                    
    }
}