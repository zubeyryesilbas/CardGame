using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
   [SerializeField] private BasicHealthBar _basicHealthBar;
   [SerializeField] private TextMeshProUGUI _healthText;
   [SerializeField] private ShieldEffect _effect;
   private int _currentValue;
   private int _health;

   public void ApplyEffect(SkillEffect effect)
   {
      if (effect.EffectType == SkillEffectType.Shield)
      {
         _effect.Show(effect.EffectValue);
      }
   }

   public void DiscardEffect()
   {
      _effect.Hide();
   }
   public void UpdatePlayerHealth(int health , float ratio)
   {
      var difference = _health - health;
      _effect.TakeDamage(difference);
      _health = health;
      _healthText.text = health.ToString();
      _basicHealthBar.SetValue(ratio);
   }
}
