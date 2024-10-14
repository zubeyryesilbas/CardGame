using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SkillSystem;

public abstract class Player
{
    public int Health { get; set; }
    public int StartHealth { get; set; }
    public List<Card> Deck { get; private set; }
    public Skill CurrentSkill { get; private set; }
    
    public Card CurrentCard;
    public int Shield;
    public int AttackBoostNextTurn;
    public Action<SkillEffect , Player > OnSkillEffectUsed;
    public Action OnDead;
    public Action AllCardsPlayed;
    private List<SkillEffect> _skillEffects = new List<SkillEffect>();

    public void AddSkillEffect(SkillEffect skillEffect)
    {
        _skillEffects.Add(skillEffect);
    }
   
    public Player(int health , List<Card> cards , Skill skill)
    {
        StartHealth = health;
        Health = health;
        Deck = new List<Card>(cards);
        CurrentSkill = skill;
        _skillEffects = new List<SkillEffect>();
    }
    
    public void TakeDamage(int damage)
    {   
        Debug.Log("Health" + Health);
        var finalDamage = Shield - damage;
        if (finalDamage < 0)
            Health += finalDamage;
        
        if (Health <= 0)
        {   
            OnDead?.Invoke();
            Health = 0;
        }
    }

    public void GetReadyTurn()
    {
        foreach (var item in _skillEffects)
        {
            item.ProcessTurn();
            if (item.EffectStartTurn == 0)
            {
                ApplySkilleffect(item);
            }
        }
        ClearUsedEffects();
    }

    private void ClearUsedEffects()
    {
        for (var i = 0; i < _skillEffects.Count; i++)
        {
            Debug.Log("Effect End Turn" +  _skillEffects[i].EffectEndTurn);
        }
        Debug.Log("Skill Effect"  + _skillEffects.Count);
    }
    public virtual void ProcessTurn()
    {
        Shield = 0;
    }

    protected virtual void PlayCard()
    {
        if (CurrentCard == null) return;
            Deck.Remove(CurrentCard); 
        if(Deck.Count == 0)
            AllCardsPlayed?.Invoke();
    }

    private void ApplySkilleffect(SkillEffect skillEffect)
    {   
        switch (skillEffect.EffectType)
        {   
            case SkillEffectType.Shield:
                Shield += skillEffect.EffectValue;
                break;
            case SkillEffectType.IncreaseAttack:
                CurrentCard.IncreaseOrDecreaseAttackValue(skillEffect.EffectValue);
                break;
            case SkillEffectType.IncreaseDefense:
                CurrentCard.IncreaseOrDeccreaseDeffenseValue(skillEffect.EffectValue);
                break;
            case SkillEffectType.IncreaseHealth:
                IncreaseHealth(skillEffect.EffectValue);
                break;
            case SkillEffectType.OpponentAttackBoostNextTurn:
                AttackBoostNextTurn += skillEffect.EffectValue;
                break;
            case SkillEffectType.DecreaseOpponentDefense:
                CurrentCard.IncreaseOrDeccreaseDeffenseValue(-skillEffect.EffectValue);
                break;
            case SkillEffectType.DecreaseOpponentAttack:
                CurrentCard.IncreaseOrDecreaseAttackValue(-skillEffect.EffectValue);
                break;
        }
        OnSkillEffectUsed?.Invoke(skillEffect , this);
    }
    private void IncreaseHealth(int increase)
    {
        Health += increase;
    }
    public void SelectCard(string str)
    {
        CurrentCard = Deck.FirstOrDefault(x => x.Name == str);
    }
}