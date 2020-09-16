using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropzoneHand : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler

{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("HAND OnDrop " + gameObject.name);
        HandAreaEvents.current.AddCardToHand(eventData.pointerDrag);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("HAND OnPointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("HAND OnPointerExit");
    }
}
