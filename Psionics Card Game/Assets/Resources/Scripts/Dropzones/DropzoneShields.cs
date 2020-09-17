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
        //Debug.Log("Shields OnDrop " + gameObject.name);
        //eventData.pointerDrag.transform.SetParent(this.transform);
        ShieldAreaEvents.current.AddCardToShields(eventData.pointerDrag);
        eventData.pointerDrag.transform.parent = this.gameObject.transform;
        //CreateSlotForCard(eventData.pointerDrag);
        VisualsEvents.current.UpdateDraggableOriginalPosition(eventData.pointerDrag);

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
