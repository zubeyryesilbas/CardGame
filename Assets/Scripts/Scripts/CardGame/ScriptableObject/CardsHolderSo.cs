using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCardsHolder", menuName = "CardGame/CardsHolder", order = 0)]
public class CardsHolderSo : ScriptableObject
{
    public CardSo[] Cards;
}
