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

   public void ApplyEffect()
   {
      _effect.gameObject.SetActive(true);
   }
   public void UpdatePlayerHealth(int health , float ratio)
   {
      _healthText.text = health.ToString();
      _basicHealthBar.SetValue(ratio);
   }
}
