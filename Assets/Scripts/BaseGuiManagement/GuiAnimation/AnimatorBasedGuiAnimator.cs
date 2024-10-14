using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AnimatorBasedGuiAnimator : GUIAnimator
{   
   [SerializeField] private Animator _animator;
   private GuiManager _guiManager;
   [SerializeField] private GuiData _data;

   [Inject]
   private void Constructor(GuiManager guiManager)
   {
       _guiManager = guiManager;
   }
   
   private void Start()
   {
       _guiManager.Register(_data);
       if (_instantHideAtStart)
       {
           _guiManager.InstantHide(_data.GuiName ,null);
       }
   }

   public override void InstantAction(Vector2 targetPosition, Vector3 targetScale, Vector3 targetRotation, float targetAlpha,
        Action action)
    {
        throw new NotImplementedException();
    }

    public override void Animate(float duration, GUIAnimationType animationType, Vector2 targetPosition, Vector3 targetScale,
        Vector3 targetRotation, float targetAlpha, Action completeAction)
    {
       _animator.SetTrigger("Show");
    }
    
}
