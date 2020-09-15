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
        Debug.Log("Shields OnDrop " + gameObject.name);
        eventData.pointerDrag.transform.SetParent(this.transform);
        CardDisplay cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
        HandEvents.current.RemoveCardFromHand(Convert.ToInt32(cardDisplay.CardId.text));
        HandEvents.current.BendHand();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Shields OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Shields OnPointerExit");
    }
}
