using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 cashedScale;

    void Start()
    {
        cashedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = cashedScale;
    }
}
