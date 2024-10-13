using System;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Skill
{
    protected SkillFactory _skillFactory;
    public Skill(string name , string description , SkillFactory skillFactory)
    {
        SkillName = name;
        SkillDescription = description;
        _skillFactory = skillFactory;
    }
    
    public string SkillName { get; protected set; }
    public string SkillDescription { get; protected set; }
    public Action<SkillEffect [] , Player > OnSkillUsed;

    public virtual void Apply(Player player, Player opponent)
    {   
        Debug.Log("Skill Used Apply");
        WaitForSkillApply(player);
    }

    private async void WaitForSkillApply(Player player)
    {
        await Task.Yield();
        OnSkillUsed?.Invoke(_skillFactory.GetEffectsOfSkill(this),player);
    }
}