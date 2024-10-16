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

    public void TakeDamage(int damage)
    {   
        var finalShield = _shieldAmount + damage;
        var ratio = 0f;
        if (finalShield > 0)
        {
            ratio = (float)finalShield / (float)_shieldAmount;
        }
        else
        {
            ratio = 0f;
            finalShield = 0;
        }
       
        if (_basicHealthBar.gameObject.activeSelf)
        {
            _barText.text = finalShield.ToString();
            _basicHealthBar.SetValue(ratio);
        }
    }
    public override void Hide()
    {
       _barText.gameObject.SetActive(false);
       _basicHealthBar.gameObject.SetActive(false);
    }
}
