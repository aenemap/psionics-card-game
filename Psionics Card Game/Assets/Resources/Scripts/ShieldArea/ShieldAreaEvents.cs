using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAreaEvents : MonoBehaviour
{
    public static ShieldAreaEvents current;

    public void Awake()
    {
        current = this;
    }

    public event Action<GameObject> onAddCardToShields;


    public void AddCardToShields(GameObject shieldCard)
    {
        if (onAddCardToShields != null)
        {
            onAddCardToShields(shieldCard);
        }
    }
}
