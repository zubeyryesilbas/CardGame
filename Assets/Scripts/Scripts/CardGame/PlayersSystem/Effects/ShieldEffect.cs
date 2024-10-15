using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldEffect : BaseEffect
{
    [SerializeField] private TextMeshProUGUI _barText;
    [SerializeField] private BasicHealthBar _basicHealthBar;
    private int _shieldAmount;
    public override void Show(int effectValue)
    {   
        _basicHealthBar.gameObject.SetActive(true);
        _barText.gameObject.SetActive(true);
        _shieldAmount = effectValue;
        _basicHealthBar.SetValue(1f);
        _barText.text =effectValue.ToString();
    }

    public void TakeDamage(float damage)
    {
        var finalShield = _shieldAmount - damage;
        var ratio = finalShield / _shieldAmount;
        _basicHealthBar.SetValue(ratio);
    }
    public override void Hide()
    {
       _barText.gameObject.SetActive(false);
       _basicHealthBar.gameObject.SetActive(false);
    }
}
