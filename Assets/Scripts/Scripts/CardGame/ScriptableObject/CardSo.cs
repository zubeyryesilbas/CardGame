using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "CardGame/Card", order = 0)]
public class CardSo : ScriptableObject
{
    public string CardName;
    public Sprite CardImage;
    public int Attack;
    public int Defense;
}