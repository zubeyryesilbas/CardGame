
namespace SkillSystem
{
    using UnityEngine;
    [CreateAssetMenu(fileName = "NewSkillsHolder", menuName = "CardGame/SkillsHolder", order = 2)]
    public class SkillsHolderSo : ScriptableObject
    {
        public SkillHolderSo[] AllSkills;
    }
}

