using DG.Tweening;
using UnityEngine;
using System;
using Zenject;

public class DOTweenGUIAnimator : GUIAnimator
{
    // Reference to the RectTransform or CanvasGroup component
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    [SerializeField] private GuiData _data;
    [SerializeField] private GuiManager _guiManager;
    private void Start()
    {
        if (_guiManager == null)
            _guiManager = FindObjectOfType<GuiManager>();
        _guiManager.Register(_data);
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_instantHideAtStart)
        {
            _guiManager.InstantHide(_data.GuiName ,null);
        }
    }

    public override void InstantAction( Vector2 targetPosition, Vector3 targetScale, Vector3 targetRotation, float targetAlpha, Action action)
    {
        _rectTransform.anchoredPosition = targetPosition;
        _rectTransform.localScale = targetScale;
        _rectTransform.transform.eulerAngles = targetRotation;
        _canvasGroup.alpha = targetAlpha;
        action?.Invoke();
    }
    
    public override void Animate( float animationDuration,GUIAnimationType animationType, Vector2 targetPosition, Vector3 targetScale, Vector3 targetRotation, float targetAlpha , Action onComplete)
    {   
        
        Sequence sequence = DOTween.Sequence();
        sequence.onComplete += () => onComplete?.Invoke();

        switch (animationType)
        {
            case GUIAnimationType.Position:
                sequence.Append(_rectTransform.DOAnchorPos(targetPosition, animationDuration));
                break;
            case GUIAnimationType.Scale:
                sequence.Append(_rectTransform.DOScale(targetScale, animationDuration));
                break;
            case GUIAnimationType.Rotation:
                sequence.Append(_rectTransform.DORotate(targetRotation, animationDuration));
                break;
            case GUIAnimationType.Alpha:
                if (_canvasGroup != null)
                {
                    sequence.Append(_canvasGroup.DOFade(targetAlpha, animationDuration));
                }
                break;
            case GUIAnimationType.All:
                sequence.Append(_rectTransform.DOAnchorPos(targetPosition, animationDuration));
                sequence.Join(_rectTransform.DOScale(targetScale, animationDuration));
                sequence.Join(_rectTransform.DORotate(targetRotation, animationDuration));
                if (_canvasGroup != null)
                {
                    sequence.Join(_canvasGroup.DOFade(targetAlpha, animationDuration));
                }
                break;
        }
    }
    void OnDestroy()
    {
       _guiManager.UnRegister(_data.GuiName);
        Debug.Log("Object is being destroyed.");
    }
}