using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class CardRotation : MonoBehaviour
{
    public GameObject cardBack;
    public GameObject cardFront;
    public CardState cardState = CardState.FaceUp;
    public float time = 0.1f;

    private bool _cardStateChangeDone;

    public bool CardStateChangeDone
    {
        get { return _cardStateChangeDone; }
        set { _cardStateChangeDone = value; }
    }

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
        CardStateChangeDone = false;
    }

    public void Flip()
    {
        if (!CardStateChangeDone)
        {
            if (cardState == CardState.FaceUp)
            {                
                cardBack.SetActive(false);
            }
            else
            {
                cardBack.SetActive(true);

            }
            CardStateChangeDone = true;
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

    public void FromDeckStartFaceUp()
    {
        if (IsFlipActive)
            return;
        StartCoroutine(FromDeckToFaceUp());
    }

    IEnumerator ToFaceDown()
    {
        HoverPreview.PreviewsAllowed = false;
        IsFlipActive = true;
        CardStateChangeDone = false;
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
        CardStateChangeDone = false;
        IsFlipActive = false;

    }

    IEnumerator ToFaceUp()
    {
        HoverPreview.PreviewsAllowed = false;
        IsFlipActive = true;
        CardStateChangeDone = false;
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
        HoverPreview.PreviewsAllowed = true;
        CardStateChangeDone = false;
        IsFlipActive = false;
    }

    IEnumerator FromDeckToFaceUp()
    {
        HoverPreview.PreviewsAllowed = false;
        IsFlipActive = true;
        CardStateChangeDone = false;
        transform.DORotate(new Vector3(0, 180, 0), time).OnUpdate(() =>
        {            
            if (transform.rotation.eulerAngles.y >= 90)
            {
                cardFront.transform.rotation = Quaternion.Euler(Vector3.zero);
                cardState = CardState.FaceUp;
                Flip();
            }
        });
        for (float i = time; i >= 0; i -= Time.deltaTime)
            yield return 0;
        HoverPreview.PreviewsAllowed = true;
        CardStateChangeDone = false;
        IsFlipActive = false;
    }
}
