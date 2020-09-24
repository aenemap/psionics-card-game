using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 pointerDisplacement = Vector3.zero;
    private float zDisplacement;

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

    private Transform _originalParent;
    public Transform originalParent
    {
        get { return _originalParent; }
        set { _originalParent = value; }
    }



    void Start()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
        originalParent = this.transform.parent;
        VisualsEvents.current.onUpdateDraggableOriginalPosition += UpdateDraggableOriginalPosition;
    }


    private void UpdateDraggableOriginalPosition(GameObject card, float xPos, float yPos)
    {
        Draggable draggable = card.GetComponent<Draggable>();
        if (draggable != null)
        {
            draggable.originalPosition = xPos >= 0 && yPos >= 0 ? new Vector3(xPos, yPos, card.transform.position.z) :   card.transform.position;
            draggable.originalRotation = card.transform.rotation;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
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
        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward, Color.red);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HoverPreview.PreviewsAllowed = true;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        eventData.pointerDrag.transform.rotation = originalRotation;
        eventData.pointerDrag.transform.DOMove(new Vector3(originalPosition.x, originalPosition.y, originalPosition.z), 0.5f).SetEase(Ease.OutQuint);

    }

    private Vector3 MouseInWorldCoords()
    {
        var screenMousePos = Input.mousePosition;
        //Debug.Log(screenMousePos);
        screenMousePos.z = zDisplacement;
        return Camera.main.ScreenToWorldPoint(screenMousePos);
    }

}
