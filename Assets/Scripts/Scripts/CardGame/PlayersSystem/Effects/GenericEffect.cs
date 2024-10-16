using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GenericEffect : BaseEffect
{
    [SerializeField] private Image _effectImage;
    [SerializeField] private TextMeshProUGUI _effectText;
    public override void Show(int effectValue)
    {
        _effectText.text = effectValue.ToString();
    }

    public override void Hide()
    {
        
    }
}
