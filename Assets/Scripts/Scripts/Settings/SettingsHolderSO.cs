using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings")]
public class SettingsHolderSO : ScriptableObject
{
    public int CardSelectionCount;
    public float TurnDuration;
    public int PlayerStartHealth;
}
