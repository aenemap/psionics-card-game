using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public int CardId;
    public string CardName;
    public CardType CardType;
    public CardSubType CardSubType;
    public int EnergyCost;
    public bool IsFaceDown;
    public int ConcistencyValue;
    public int DefenceValue;
    public int AbsorbingValue;
    public bool IsLoadedWithEnergy;
    public int TrashCost;

    public void PrintCard()
    {
        Debug.Log("Print Card");
    }

}



