using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class CardRotation : MonoBehaviour
{
    public GameObject cardFront;
    public GameObject cardBack;
    public CardState cardState = CardState.FaceUp;
    public float time = 0.1f;
    private bool isFlipActive = false;

    // Start is called before the first frame update
    void Start()
    {
        Init();
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

    public void Init()
    {
        if (cardState == CardState.FaceUp)
        {
            cardFront.transform.eulerAngles = Vector3.zero;
            //cardBack.transform.eulerAngles = new Vector3(0, 90, 0);
            cardBack.SetActive(false);
        }
        else
        {
            cardFront.transform.eulerAngles = new Vector3(0, 90, 0);
            //cardBack.transform.eulerAngles = Vector3.zero;
            cardBack.SetActive(true);
        }
    }

    public void StartFaceDown()
    {
        if (isFlipActive)
            return;
        StartCoroutine(ToFaceDown());
    }

    public void StartFaceUp()
    {
        if (isFlipActive)
            return;
        StartCoroutine(ToFaceUp());
    }

    IEnumerator ToFaceDown()
    {
        isFlipActive = true;
        cardFront.transform.DORotate(new Vector3(0, 90, 0), time).OnComplete(() => {
            cardFront.SetActive(false);
            cardBack.SetActive(true);
        });
        for (float i = time; i >= 0; i -= Time.deltaTime)
            yield return 0;        
        cardBack.transform.DORotate(new Vector3(0, 0, 0), time);
        isFlipActive = false;
        cardState = CardState.FaceDown;
    }

    IEnumerator ToFaceUp()
    {
        isFlipActive = true;
        cardBack.transform.DORotate(new Vector3(0, 90, 0), time).OnComplete(() => {
            cardBack.SetActive(false);
            cardFront.SetActive(true);
        });
        for (float i = time; i >= 0; i -= Time.deltaTime)
            yield return 0;
        cardFront.transform.DORotate(new Vector3(0, 0, 0), time);
        isFlipActive = false;
        cardState = CardState.FaceUp;
    }
}
