using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvents : MonoBehaviour
{
    public static HandEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onBendHand;
    public event Action<int> onRemoveCardFromHand;
    public event Action<GameObject> onAddCardToHand;

    public void BendHand()
    {
        if (onBendHand != null)
        {
            onBendHand();
        }
    }

    public void RemoveCardFromHand(int cardId)
    {
        if(onRemoveCardFromHand != null)
        {
            onRemoveCardFromHand(cardId);
        }
    }

    public void AddCardToHand(GameObject card)
    {
        if (onAddCardToHand != null)
        {
            onAddCardToHand(card);
        }
    }
}
