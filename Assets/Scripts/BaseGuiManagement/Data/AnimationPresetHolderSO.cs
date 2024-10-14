using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationPresetHolder", menuName = "Animation/Animation Preset Holder")]
public class AnimationPresetHolderSO :ScriptableObject
{
   public List<AnimationPreset> AnimationPresets;
}
