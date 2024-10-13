using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewSkillsHolder", menuName = "CardGame/SkillsHolder", order = 2)]
public class SkillsHolderSo : ScriptableObject
{
    public SkillHolderSo[] AllSkills;
}
