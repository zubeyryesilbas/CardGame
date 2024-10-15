using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
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
