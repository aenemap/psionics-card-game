using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropzoneShields : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    
    private void Start()
    {
    }
    public void OnDrop(PointerEventData eventData)
    {   
        ShieldAreaEvents.current.AddCardToShields(eventData.pointerDrag);
        eventData.pointerDrag.transform.parent = this.gameObject.transform;
        //ShieldAreaCards shieldAreaCards = this.transform.GetComponent<ShieldAreaCards>();
        eventData.pointerDrag.transform.DORotate(new Vector3(0, 180, 0), 0.5f).SetEase(Ease.OutQuint);

        CardDisplay cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
        HandAreaEvents.current.RemoveCardFromHand(Convert.ToInt32(cardDisplay.CardId.text));
        HandAreaEvents.current.BendHand();

    }

    private void CreateSlotForCard(GameObject card)
    {
        GameObject slot = new GameObject("Slot");
        slot.transform.parent = this.gameObject.transform;
        card.transform.parent = slot.transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Shields OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Shields OnPointerExit");
    }
}
