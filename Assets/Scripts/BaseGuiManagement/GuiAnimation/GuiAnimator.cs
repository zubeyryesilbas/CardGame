using System;
using UnityEngine;

public abstract class GUIAnimator : MonoBehaviour
{
    [SerializeField] protected bool _instantHideAtStart;
    public abstract void InstantAction( Vector2 targetPosition, Vector3 targetScale,
        Vector3 targetRotation, float targetAlpha, Action action);
    public abstract void Animate( float duration , GUIAnimationType animationType, Vector2 targetPosition, Vector3 targetScale, Vector3 targetRotation, float targetAlpha ,Action completeAction);
    
}