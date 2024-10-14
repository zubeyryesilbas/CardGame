[System.Serializable]
public class SkillEffect
{
    public SkillEffectType EffectType;
    public int EffectValue;
    public int EffectStartTurn; //to show how many turns after it is activated it will take effect and how many turns after it will end
    public int EffectEndTurn;

    public void ProcessTurn()
    {
        EffectStartTurn--;
        EffectEndTurn--;
    }
}