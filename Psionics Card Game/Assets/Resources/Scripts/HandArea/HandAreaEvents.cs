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
    public event Action<GameObject, Transform> onAddCardToHand;
    public event Action<List<GameObject>, Transform> onAddCardBendHand;

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

    public void AddCardToHand(GameObject card, Transform parent)
    {
        onAddCardToHand?.Invoke(card, parent);
    }

    public void AddCardBendHand(List<GameObject> cards, Transform parent)
    {
        onAddCardBendHand?.Invoke(cards,parent);
    }
}
