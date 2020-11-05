using System;
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
    public Sprite CardImage;
    public int EnergyCost;
    public bool IsFaceDown;
    public int ConcistencyValue;
    public int DefenceValue;
    public int AbsorbingValue;
    public bool IsLoadedWithEnergy;
    public int TrashCost;
    public CardLocation LocationOfCard;

    public void PrintCard()
    {
        Debug.Log("Print Card");
    }

}



