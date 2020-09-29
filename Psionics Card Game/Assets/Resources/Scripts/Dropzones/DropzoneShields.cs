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
        Card cardAsset = eventData.pointerDrag.GetCardAsset();
        if (cardAsset.CardType == Enums.CardType.Shield || cardAsset.CardType == Enums.CardType.Test)
        {
            ShieldAreaEvents.current.AddCardToShields(eventData.pointerDrag);
            VisualsEvents.current.CardToSmallPreview(eventData.pointerDrag);
            HandAreaEvents.current.RemoveCardFromHand(cardAsset.CardId, false);
            HandAreaEvents.current.BendHand();
        }
    }
}
