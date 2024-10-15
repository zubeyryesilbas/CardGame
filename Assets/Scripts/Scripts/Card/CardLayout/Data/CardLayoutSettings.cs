using UnityEngine;

[CreateAssetMenu(fileName = "CardLayoutSettings", menuName = "ScriptableObjects/CardLayoutSettings")]
public class CardLayoutSettings : ScriptableObject
{
    public int TopXRatio, TopYRatio, DownXRatio, DownYRatio;
    public float SpaceX;
    public float SpaceY; 
    public int SelectionSize = 6;
    public float TopOffset, DownOffset;
}