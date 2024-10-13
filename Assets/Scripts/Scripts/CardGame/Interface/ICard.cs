using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICard 
{
    Card Card { get; }
    Transform CardTr { get; }
    void Initialize(Sprite sprite, Card card);
    void ApplySkillEffects(SkillEffect[] effects);
    Action <int> DamageCallBack { get; set; }
}
