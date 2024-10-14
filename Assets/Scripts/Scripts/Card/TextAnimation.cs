using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
public class TextAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro; 
    private float _animationDuration = 0.25f;
    private int _currentValue;

    public void SetInitialValue(int val)
    {
        _currentValue = val;
    }
    public void AnimateTextValue(int toValue)
    {
        DOTween.To(() => _currentValue, x => 
        {
            _currentValue = x; 
            _textMeshPro.text = _currentValue.ToString();  
        }, toValue, _animationDuration).SetEase(Ease.Linear);  
    }
}
