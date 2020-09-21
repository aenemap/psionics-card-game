﻿using System;
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
    public event Action<GameObject, float, float> onUpdateDraggableOriginalPosition;

    public void SameDistanceCalculate(List<GameObject> cards)
    {
        if (onSameDistanceCalculate != null)
        {
            onSameDistanceCalculate(cards);
        }
    }

    public void UpdateDraggableOriginalPosition(GameObject card, float xPos, float yPos)
    {
        if (onUpdateDraggableOriginalPosition != null)
        {
            onUpdateDraggableOriginalPosition(card, xPos, yPos);
        }
    }

}
