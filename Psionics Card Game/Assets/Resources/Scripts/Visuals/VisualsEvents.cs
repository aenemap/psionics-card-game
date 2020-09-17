using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsEvents : MonoBehaviour
{
    public static VisualsEvents current;

    public void Awake()
    {
        current = this;
    }

    public event Action<List<GameObject>> onSameDistanceCalculate;
    public event Action<GameObject> onUpdateDraggableOriginalPosition;

    public void SameDistanceCalculate(List<GameObject> cards)
    {
        if (onSameDistanceCalculate != null)
        {
            onSameDistanceCalculate(cards);
        }
    }

    public void UpdateDraggableOriginalPosition(GameObject card)
    {
        if (onUpdateDraggableOriginalPosition != null)
        {
            onUpdateDraggableOriginalPosition(card);
        }
    }
}
