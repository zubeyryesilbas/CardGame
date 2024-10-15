using System;

[System.Serializable]
public class SkillEffect
{
    public SkillEffectType EffectType; // Type of the skill effect
    public int EffectValue; // Value of the effect
    public int EffectStartTurn; // Turns until the effect starts
    public int EffectEndTurn; // Turns until the effect ends

    // Constructor for easy initialization
    public SkillEffect(SkillEffectType effectType, int effectValue, int startTurn, int endTurn)
    {
        EffectType = effectType;
        EffectValue = effectValue;
        EffectStartTurn = startTurn;
        EffectEndTurn = endTurn;
    }

    // Processes the turn, decrementing the start and end turn counters
    public void ProcessTurn()
    {
        EffectStartTurn--; // Decrement start turn counter
        EffectEndTurn--; // Decrement end turn counter
    }

    // Checks if the effect is active based on the turn counters
    public bool IsActive()
    {
        return EffectEndTurn > 0; // The effect is active if the end turn is greater than 0
    }

    // Reset the effect for reuse if needed
    public void ResetEffect()
    {
        EffectStartTurn = 0;
        EffectEndTurn = 0;
    }
}