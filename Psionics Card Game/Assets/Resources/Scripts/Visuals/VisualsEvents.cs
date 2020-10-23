using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsEvents : MonoBehaviour
{
    public static VisualsEvents current;

    public VisualsEvents()
    {
        current = this;
    }

    public void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    public event Action<GameObject, float, float> onUpdateDraggableOriginalPosition;
    public event Action<GameObject> onCardToSmallPreview;
    public event Action<GameObject> onCardToNormalPreview;
    public event Action<GameObject> onAddCardToDiscards;

    public void UpdateDraggableOriginalPosition(GameObject card, float xPos, float yPos)
    {
        onUpdateDraggableOriginalPosition?.Invoke(card, xPos, yPos);
    }

    public void CardToSmallPreview(GameObject card)
    {
        onCardToSmallPreview?.Invoke(card);
    }

    public void CardToNormalPreview(GameObject card)
    {
        onCardToNormalPreview?.Invoke(card);
    }

    public void AddCardToDiscards(GameObject card)
    {
        onAddCardToDiscards?.Invoke(card);
    }

}
