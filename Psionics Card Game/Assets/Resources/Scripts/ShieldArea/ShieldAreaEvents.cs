using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAreaEvents : MonoBehaviour
{
    public static ShieldAreaEvents current;

    public ShieldAreaEvents()
    {
        current = this;
    }

    public event Action<GameObject> onAddCardToShields;
    public event Action<int> onRemoveCardFromShields;


    public void AddCardToShields(GameObject shieldCard)
    {
        if (onAddCardToShields != null)
        {
            onAddCardToShields(shieldCard);
        }
    }

    public void RemoveCardFromShields(int cardId)
    {
        if (onRemoveCardFromShields != null)
        {
            onRemoveCardFromShields(cardId);
        }
    }
}
