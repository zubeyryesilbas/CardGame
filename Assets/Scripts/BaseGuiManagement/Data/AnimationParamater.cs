using System;
using UnityEngine;

[Serializable]
public struct AnimationParamater
{
    public GUIAnimationType AnimationType;
    public float AnimationDuration;
    public Vector2 TargetPosition;
    public Vector3 TargetScale;
    public Vector3 TargetRotation;
    public float TargetAlpha;
}