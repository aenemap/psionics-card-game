using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 pointerDisplacement = Vector3.zero;
    private float zDisplacement;


    //private Vector3 originalPosition;
    //private Quaternion originalRotation;
    private Vector3 _originalPosition;
    public Vector3 originalPosition
    {
        get { return _originalPosition; }
        set { _originalPosition = value; }
    }

    private Quaternion _originalRotation;
    public Quaternion originalRotation
    {
        get { return _originalRotation; }
        set { _originalRotation = value; }
    }

    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
        VisualsEvents.current.onUpdateDraggableOriginalPosition += UpdateDraggableOriginalPosition;
    }

    private void UpdateDraggableOriginalPosition(GameObject card)
    {
        Draggable draggable = card.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.originalPosition = card.transform.position;
            draggable.originalRotation = card.transform.rotation;
        }
    }

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
        //Debug.Log("OnDrag");

        Vector3 mousePos = MouseInWorldCoords();
        this.transform.position = new Vector3(mousePos.x - pointerDisplacement.x, mousePos.y - pointerDisplacement.y, transform.position.z);
        Debug.Log("POSITION =>" + this.transform.position);
        Debug.Log("ORIGINAL POSITION =>" + originalPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        HoverPreview.PreviewsAllowed = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        this.transform.position = originalPosition;
        this.transform.rotation = originalRotation;
    }

    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        //Debug.Log(screenMousePos);
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }
}
