using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BattleEndAnimation
{
    [SerializeField] private Color _textColor, _buttonColor;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Button _button;
    public BattleResult BattleResult;
    public void Initialize(Action onClick)
    {   
        _button.onClick.AddListener(()=>onClick?.Invoke());
        _text.color = _textColor;
        _button.image.color = _buttonColor;
    }

    public void Show()
    {   
        _text.transform.localScale = Vector3.one * 1.5f;
        _text.alpha = 0.5f;
        _text.gameObject.SetActive(true);
        _text.DOFade(1, 0.5f);
        _text.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBounce);
    }
    
    public void Hide()
    {
        _text.DOFade(0f, 0.5f).OnComplete(() =>
        {
            _text.gameObject.SetActive(false);
        });
        _button.image.DOFade(0f, 0.5f).OnComplete(() =>
        {
            _button.gameObject.SetActive(false);
        });
    }
}
