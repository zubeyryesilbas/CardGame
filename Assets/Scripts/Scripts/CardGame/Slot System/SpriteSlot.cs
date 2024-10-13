using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpriteSlot : BaseSlot
{
    [SerializeField] private SpriteRenderer _frameRenderer;
    private Vector3 _frameDefaultScale;
    private Tween _highlightTween;
    [SerializeField] private Color _highlightColor = new Color(0.5f, 1f, 0.5f); // Color to change to during highlight (light green)
    [SerializeField] private Color _unhighlightColor = new Color(1f, 0.5f, 0.5f); // Color when unhighlighting (light red)
    [SerializeField] private Color _cardPlacedColor = new Color(0.5f, 0.5f, 1f); // Color when card is placed (light blue)
    [SerializeField] private Color _switchColor = new Color(1f, 1f, 0.5f); // Color when switching (light yellow)

    private void Awake()
    {
        _frameDefaultScale = _frameRenderer.transform.localScale;
    }

    public override void PlaceCard()
    {
        _frameRenderer.color = _cardPlacedColor;
        if (_highlightTween != null) _highlightTween.Kill();
        
        _frameRenderer.transform.DOKill();
        _frameRenderer.transform.DOScale(_frameDefaultScale, 0.2f).SetEase(Ease.OutBounce);
        _stats = SlotStats.Ocupied;
    }

    public override void RemoveCard()
    {
        _stats = SlotStats.Empty;
    }

    public override void HighLight()
    {
        _stats = SlotStats.Highlighted;
       HighlightSlot();
    }

    public override void UnHighLight()
    {
        _frameRenderer.color = _unhighlightColor;
        _frameRenderer.transform.DOKill();
        _frameRenderer.transform.DOScale(_frameDefaultScale, 0.2f).SetEase(Ease.OutBounce);
        RemoveCard(); 
    }

    public override void SwitchLight()
    {
        _frameRenderer.color = _switchColor;
        _stats = SlotStats.Switch;
    }

    public void HighlightSlot()
    {
        _frameRenderer.color = _highlightColor;
        _stats = SlotStats.Highlighted;
       _highlightTween = _frameRenderer.transform.DOScale( _frameDefaultScale* 1.1f, 0.3f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            _frameRenderer.transform.DOScale(_frameDefaultScale, 0.3F).SetEase(Ease.InQuad).OnComplete(HighlightSlot);
        });
    }


}
