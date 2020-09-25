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

    public event Action<GameObject, float, float> onUpdateDraggableOriginalPosition;


    public void UpdateDraggableOriginalPosition(GameObject card, float xPos, float yPos)
    {
        if (onUpdateDraggableOriginalPosition != null)
        {
            onUpdateDraggableOriginalPosition(card, xPos, yPos);
        }
    }

}
