using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropzoneHand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler

{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("HAND OnDrop " + gameObject.name);
        HandAreaEvents.current.AddCardToHand(eventData.pointerDrag, this.gameObject.transform);
        CardDisplay cardDisplay = eventData.pointerDrag.transform.GetComponent<CardDisplay>();
        VisualsEvents.current.CardToNormalPreview(eventData.pointerDrag);
        if (cardDisplay.card.CardType == Enums.CardType.Shield || cardDisplay.card.CardType == Enums.CardType.Test)
            ShieldAreaEvents.current.RemoveCardFromShields(cardDisplay.card.CardId);


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
