using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class CardRotation : MonoBehaviour
{
    public GameObject cardBack;
    public CardState cardState = CardState.FaceUp;
    public float time = 0.1f;

    private bool cardStateChangeDone;

    private bool _isFlipActive;

    public bool IsFlipActive
    {
        get { return _isFlipActive; }
        set { _isFlipActive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        IsFlipActive = false;
        cardStateChangeDone = false;
    }

    //private void OnMouseDown()
    //{
    //    if (cardState == CardState.FaceUp)
    //    {
    //        StartFaceDown();
    //    }
    //    else
    //    {
    //        StartFaceUp();
    //    }
    //}

    public void Flip()
    {
        if (!cardStateChangeDone)
        {
            if (cardState == CardState.FaceUp)
            {                
                cardBack.SetActive(false);
            }
            else
            {                
                cardBack.SetActive(true);
            }
            cardStateChangeDone = true;
        }
    }

    public void StartFaceDown()
    {
        if (IsFlipActive)
            return;
        StartCoroutine(ToFaceDown());
    }

    public void StartFaceUp()
    {
        if (IsFlipActive)
            return;
        StartCoroutine(ToFaceUp());
    }

    IEnumerator ToFaceDown()
    {
        HoverPreview.PreviewsAllowed = false;
        IsFlipActive = true;
        cardStateChangeDone = false;
        transform.DORotate(new Vector3(0, 180, 0), time).OnUpdate(() =>
        {
            if (transform.rotation.eulerAngles.y >= 90)
            {
                cardState = CardState.FaceDown;
                Flip();
            }
        });
        for (float i = time; i >= 0; i -= Time.deltaTime)
            yield return 0;
        HoverPreview.PreviewsAllowed = true;
        cardStateChangeDone = false;
        IsFlipActive = false;

    }

    IEnumerator ToFaceUp()
    {
        IsFlipActive = true;
        transform.DORotate(new Vector3(0, 360, 0), time).OnUpdate(() =>
        {
            if (transform.rotation.eulerAngles.y >= 270)
            {
                cardState = CardState.FaceUp;
                Flip();
            }
        });
        for (float i = time; i >= 0; i -= Time.deltaTime)
            yield return 0;
        cardStateChangeDone = false;
        IsFlipActive = false;
    }
}
