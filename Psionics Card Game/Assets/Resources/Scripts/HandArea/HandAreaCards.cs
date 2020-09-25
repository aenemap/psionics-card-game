using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;
using DG.Tweening;

public class HandAreaCards : MonoBehaviour
{
    //public CardManager cardManager;
    //public DeckManager deckManager;
    public Transform pivot;
    public GameObject Hand;

    [Header("Bend Info")]
    public float cardWidth;
    public float bentAngle;
    public float spacing;
    public float height;

    public List<GameObject> cardsInHand = new List<GameObject>();

    //private float cardWidth = 10f;
    //private float bentAngle = 20;
    //private float spacing = -2;
    //private float height = 0.12f;
    // Start is called before the first frame update

    private void OnEnable()
    {
        HandAreaEvents.current.onBendHand += OnBendHand;
        HandAreaEvents.current.onRemoveCardFromHand += OnRemoveCardFromHand;
        HandAreaEvents.current.onAddCardToHand += OnAddCardToHand;
    }

    void Start()
    {

        //var deck = deckManager.GetDeckById(1);
        //if (deck != null)
        //{
        //    foreach(Card card in deck.DeckList)
        //    {
        //        GameObject crd = cardManager.GetCard(card.CardId);
        //        cardsInHand.Add(crd);
        //        crd.transform.SetParent(pivot.transform, false);
                
        //    }
            
        //    Bent(cardsInHand);
           
        //}

    }

    private void OnAddCardToHand(GameObject card)
    {
        
        cardsInHand.Insert(0, card);
        Bent(cardsInHand);
    }

    private void OnRemoveCardFromHand(int cardId)
    {
        cardsInHand = cardsInHand.Where(w =>
        {
            CardDisplay cardDisplay = w.transform.GetComponent<CardDisplay>();
            return cardDisplay.CardId.text != cardId.ToString();
        }).ToList();
    }

    private void OnBendHand()
    {
        Bent(cardsInHand);
    }

    public void Bent(List<GameObject> cards)
    {
        if (cards == null || cards.Count < 1)
            return;

        var fullAngle = -bentAngle;
        var anglePerCard = fullAngle / cards.Count;
        var firstAngle = Utilities.CalcFirstAngle(fullAngle);
        //var handWidth = CalcHandWidth(cards.Count);
        var handWidth = Utilities.CalcAreaWidth(cards.Count, cardWidth, spacing);

        var offsetX = pivot.position.x - handWidth / 2;

        for (var i = 0; i < cards.Count; i++ )
        {
            var card = cards[i];

            CardRotation cardRotation = card.transform.GetComponent<CardRotation>();
            if (cardRotation.cardState == Enums.CardState.FaceDown)
            {
                cardRotation.cardState = Enums.CardState.FaceUp;
                if (cardRotation.CardStateChangeDone)
                    cardRotation.CardStateChangeDone = false;
                cardRotation.Flip();
            }

            var angleTwist = firstAngle + i * anglePerCard;

            var xPos = offsetX + cardWidth / 2;

            var yDistance = Mathf.Abs(angleTwist) * height;

            var yPos = pivot.position.y - yDistance;

            var rotation = new Vector3(0, 0, angleTwist);
            var position = new Vector3(xPos, yPos, card.transform.position.z);

            //card.transform.position = position;
            card.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
            
            GameObject cardFront = cardRotation.cardFront;
            if (cardFront.transform.rotation.eulerAngles.y == 180)
            {
                cardFront.transform.rotation = Quaternion.Euler(
                        rotation.x,
                        0,
                        rotation.z
                    );
            }
            card.transform.DOMove(position, 0.5f).SetEase(Ease.OutQuint).OnComplete(() => {
                VisualsEvents.current.UpdateDraggableOriginalPosition(card, -1f, -1f);
            });
            card.transform.parent = pivot.transform;
            offsetX += cardWidth + spacing;

        }


    }

    private void OnDisable()
    {
        HandAreaEvents.current.onBendHand -= OnBendHand;
        HandAreaEvents.current.onRemoveCardFromHand -= OnRemoveCardFromHand;
        HandAreaEvents.current.onAddCardToHand -= OnAddCardToHand;
    }


}
