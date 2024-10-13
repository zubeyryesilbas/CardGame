using System.Collections.Generic;
using System.Linq;
using Zenject;

public class SkillFactory
{
    private static System.Random _random = new System.Random();
    private List<Skill> _cachedSkills;

    public SkillFactory(SkillsHolderSo skillsHolderSo)
    {
        _cachedSkills = new List<Skill>();

        foreach (var skillData in skillsHolderSo.AllSkills)
        {
            switch (skillData.SkillType) 
            {
                case SkillType.IncreaseHealth :
                    _cachedSkills.Add(new IncreaseHealthSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName , skillData.Description));
                    break;
                case SkillType.IncreaseAttack:
                    _cachedSkills.Add(new IncreaseAttackSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName , skillData.Description   ));
                    break;
                case SkillType.IncreaseDeffense:
                    _cachedSkills.Add(new IncreaseDefenseSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName , skillData.Description ));
                    break;
                case SkillType.DecreaseOpponentAttack:
                    _cachedSkills.Add(new DecreaseOpponentAttackSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName , skillData.Description ));
                    break;
                case SkillType.DecreaseOpponentDeffense:
                    _cachedSkills.Add(new DecreaseOpponentDefenseSkill(skillData.Effects[0].EffectValue,
                        skillData.SkillName , skillData.Description));
                    break;
                case SkillType.ShieldSkill:
                    _cachedSkills.Add(new ShieldSkill(skillData.Effects.FirstOrDefault(x=>x.EffectType == SkillEffectType.Shield).EffectValue , 
                        skillData.Effects.FirstOrDefault(x=>x.EffectType == SkillEffectType.OpponentAttackBoostNextTurn).EffectValue,
                        skillData.SkillName , skillData.Description ));
                    break;
                default:
                    break;
            }
        }
    }

    public Skill GetRandomSkill()
    {
        int randomSkillIndex = _random.Next(_cachedSkills.Count); 
        return _cachedSkills[randomSkillIndex];                    
    }
}