using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    Vector3 cachedScale;
    Vector3 cachedPosition;
    private int objIndex;
    private BoxCollider col;

    void Start()
    {
        cachedScale = transform.localScale;
        cachedPosition = transform.position;

        col = transform.GetComponent<BoxCollider>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        col.enabled = false;
        //GameObject parent = transform.parent.gameObject;
        //objIndex = parent.transform.GetSiblingIndex();
        //parent.transform.SetAsLastSibling();
        transform.position = new Vector3(transform.position.x, transform.position.y + 10f, 0);
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //transform.DOMove(new Vector3(transform.position.x, transform.position.y + 1f, 0), 1f).SetEase(Ease.OutQuint);
        //transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 1f).SetEase(Ease.OutQuint);

        //previewGameObject.transform.DOMove(new Vector3(previewGameObject.transform.position.x, 300f, 0), 1f).SetEase(Ease.OutQuint);
        //previewGameObject.transform.DOScale(new Vector3(3f, 3f, 0), 1f).SetEase(Ease.OutQuint);

        //previewGameObject.transform.localPosition = Vector3.zero;
        //previewGameObject.transform.localScale = Vector3.one;
        //var halfCardHeight = new Vector3(0, 3);
        //var pointZeroScreen = Camera.main.ScreenToWorldPoint(Vector3.zero);
        //var bottomScreenY = new Vector3(0, pointZeroScreen.y);
        //var currentPosWithoutY = new Vector3(previewGameObject.transform.position.x, 0, previewGameObject.transform.position.z);
        //var hoverHeightParameter = new Vector3(0, -5f, 0);
        //var final = currentPosWithoutY + bottomScreenY + halfCardHeight + hoverHeightParameter;
        //Debug.Log("Final => " + final.ToString());
        //GameObject parent = previewGameObject.transform.parent.gameObject;
        //objIndex = parent.transform.GetSiblingIndex();
        //parent.transform.SetAsLastSibling();
        //previewGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        //previewGameObject.transform.DOMove(final, 1f).SetEase(Ease.OutQuint);
        //previewGameObject.transform.DOScale(new Vector3(0, 3f, 0), 1f).SetEase(Ease.OutQuint);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //GameObject parent = transform.parent.gameObject;
        //parent.transform.SetSiblingIndex(objIndex);
        transform.position = cachedPosition;
        transform.localScale = cachedScale;
        col.enabled = true;
    }
}
