using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationPresetManager : MonoBehaviour
{
    [SerializeField] private AnimationPresetHolderSO _animationPresetHolder;
    private Dictionary<GuiTypeSettingsId, AnimationPreset> _animationPresets = new Dictionary<GuiTypeSettingsId, AnimationPreset>();
    private void Awake()
    {
        CacheAnimationPresets();
    }

    private void CacheAnimationPresets()
    {
        _animationPresets = new Dictionary<GuiTypeSettingsId, AnimationPreset>();

        foreach (var preset in _animationPresetHolder.AnimationPresets)
        {
            _animationPresets[preset.GuiType] = preset;
        }
    }
    
    public AnimationParamater GetAnimationParamater(GuiTypeSettingsId guiType, bool show)
    {
        if (_animationPresets.TryGetValue(guiType, out var preset))
        {
            return show ? preset.ShowParamaters : preset.HideParamaters;
        }
        else
        {
            Debug.LogError("Animation preset not found for GUI type: " + guiType);
            return new AnimationParamater();
        }
    }

}