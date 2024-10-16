
namespace SkillSystem
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "NewSkillHolder", menuName = "CardGame/SkillHolder", order = 1)]
    public class SkillHolderSo : ScriptableObject
    {
        public SkillType SkillType;
        public string SkillName;            
        public string Description;         
        public SkillEffect[] Effects;
        
    }
}
