using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropzoneShields : MonoBehaviour, IDropHandler
{
    
    private void Start()
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        CardDisplay cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
        if (cardDisplay.card.CardType == Enums.CardType.Shield || cardDisplay.card.CardType == Enums.CardType.Test)
        {
            ShieldAreaEvents.current.AddCardToShields(eventData.pointerDrag);
            VisualsEvents.current.CardToSmallPreview(eventData.pointerDrag);
            HandAreaEvents.current.RemoveCardFromHand(cardDisplay.card.CardId, false);
            HandAreaEvents.current.BendHand();
            CardRotation cardROtation = eventData.pointerDrag.transform.GetComponent<CardRotation>();
            cardROtation.StartFaceUp();
        }
    }
}
