using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAreaEvents : MonoBehaviour
{
    public static HandAreaEvents current;

    public HandAreaEvents()
    {
        current = this;
    }

    public event Action onBendHand;
    public event Action<int, bool> onRemoveCardFromHand;
    public event Action<GameObject> onAddCardToHand;

    public void BendHand()
    {
        if (onBendHand != null)
        {
            onBendHand();
        }
    }

    public void RemoveCardFromHand(int cardId, bool bent)
    {
        onRemoveCardFromHand?.Invoke(cardId, bent);
    }

    public void AddCardToHand(GameObject card)
    {
        onAddCardToHand?.Invoke(card);
    }
}
