public abstract class Skill
{
    public Skill(string name , string description)
    {
        SkillName = name;
        SkillDescription = description;
    }
    public string SkillName { get; protected set; }
    public string SkillDescription { get; protected set; }
    public abstract void Apply(Player player, Player opponent);
}