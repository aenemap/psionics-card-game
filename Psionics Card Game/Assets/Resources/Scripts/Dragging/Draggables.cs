using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Draggables : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 pointerDisplacement = Vector3.zero;
    private float zDisplacement;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        HoverPreview.PreviewsAllowed = false;
        zDisplacement = -Camera.main.transform.position.z + transform.position.z;
        pointerDisplacement = -transform.position + MouseInWorldCoords();
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        Vector3 mousePos = MouseInWorldCoords();
        this.transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        HoverPreview.PreviewsAllowed = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        //Debug.Log(screenMousePos);
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
}
