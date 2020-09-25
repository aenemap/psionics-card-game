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
        if (cardDisplay.card.CardType == Enums.CardType.Shield)
        {
            ShieldAreaEvents.current.AddCardToShields(eventData.pointerDrag);
            //ShieldAreaCards shieldAreaCards = this.transform.GetComponent<ShieldAreaCards>();
            //eventData.pointerDrag.transform.DORotate(new Vector3(0, 180, 0), 0.5f).SetEase(Ease.OutQuint);

            HandAreaEvents.current.RemoveCardFromHand(cardDisplay.card.CardId);
            HandAreaEvents.current.BendHand();
        }

    }
}
