using System;
using System.Collections.Generic;
using UnityEngine;

public class GuiManager :MonoBehaviour
{
    private Dictionary<GuiName, GuiData> _guiDataDic = new Dictionary<GuiName, GuiData>();
    private Dictionary<GuiName, AnimationParamater> _presetsDicOpen = new Dictionary<GuiName, AnimationParamater>();
    private Dictionary<GuiName, AnimationParamater> _presetDicClose = new Dictionary<GuiName, AnimationParamater>();
    [SerializeField] private AnimationPresetManager _animationPresetManager;
    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Register(GuiData data)
    {
        _guiDataDic.Add(data.GuiName, data);
        var presetOpen = _animationPresetManager.GetAnimationParamater(data.GuiTypeSettingsId, true);
        var presetClose = _animationPresetManager.GetAnimationParamater(data.GuiTypeSettingsId, false);
        if (!_presetsDicOpen.ContainsKey(data.GuiName))
        {
            _presetsDicOpen.Add(data.GuiName, presetOpen);
        }

        if (!_presetDicClose.ContainsKey(data.GuiName))
        {
            _presetDicClose.Add(data.GuiName, presetClose);
        }
    }

    public void UnRegister(GuiName guiName)
    {   
        _guiDataDic.Remove(guiName);
    }

    public void ShowGuiWithId(GuiName guiName, Action action)
    {   
        var preset = _presetsDicOpen[guiName];
        var animatable = _guiDataDic[guiName].GUIAnimator;
        animatable.gameObject.SetActive(true);
        OverrideAction(animatable.gameObject, action, true);
        animatable.Animate(preset.AnimationDuration, preset.AnimationType, preset.TargetPosition, preset.TargetScale, preset.TargetRotation, preset.TargetAlpha, action);
    }

    public void HideuiWithId(GuiName guiName, Action action)
    {
        var preset = _presetDicClose[guiName];
        var animatable = _guiDataDic[guiName].GUIAnimator;

        // Ensure the action includes deactivating the GameObject
        Action completeAction = () => animatable.gameObject.SetActive(false);
        if (action != null)
        {
            action += completeAction;
        }
        else
        {
            action = completeAction;
        }

        OverrideAction(animatable.gameObject, action, false);
        animatable.Animate(preset.AnimationDuration, preset.AnimationType, preset.TargetPosition, preset.TargetScale, preset.TargetRotation, preset.TargetAlpha, action);
    }

    public void InstantHide(GuiName guiName, Action action)
    {
        var preset = _presetDicClose[guiName];
        var animatable = _guiDataDic[guiName].GUIAnimator;

        // Ensure the action includes deactivating the GameObject
        Action completeAction = () => animatable.gameObject.SetActive(false);
        if (action != null)
        {
            action += completeAction;
        }
        else
        {
            action = completeAction;
        }

        OverrideAction(animatable.gameObject, action, false);
        animatable.InstantAction(preset.TargetPosition, preset.TargetScale, preset.TargetRotation, preset.TargetAlpha, action);
    }

    public void InstantShow(GuiName guiName, Action action)
    {
        var preset = _presetsDicOpen[guiName];
        var animatable = _guiDataDic[guiName].GUIAnimator;
        animatable.gameObject.SetActive(true);
        OverrideAction(animatable.gameObject, action, true);
        animatable.InstantAction(preset.TargetPosition, preset.TargetScale, preset.TargetRotation, preset.TargetAlpha, action);
    }

    private void OverrideAction(GameObject obj, Action action, bool val)
    {
        var canvasGroup = obj.GetComponent<CanvasGroup>();
        if (action != null)
        {
            action += () => canvasGroup.blocksRaycasts = val;
        }
        else
        {
            action = () => canvasGroup.blocksRaycasts = val;
        }
    }
}
