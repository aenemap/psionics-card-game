using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandCards : MonoBehaviour
{
    public CardManager cardManager;
    public DeckManager deckManager;
    public Transform pivot;
    public GameObject Hand;

    private List<GameObject> cardsInHand = new List<GameObject>();

    private float cardWidth = 4.5f;
    private float bentAngle = 20;
    private float spacing = -2;
    private float height = 0.12f;
    // Start is called before the first frame update
    void Start()
    {
        var deck = deckManager.GetDeckById(1);
        if (deck != null)
        {
            foreach(Card card in deck.Deck)
            {
                GameObject crd = cardManager.GetCard(card.CardId);
                cardsInHand.Add(crd);
                crd.transform.SetParent(Hand.transform, false);
                
            }
            if (cardsInHand.Count > 1)
                Bent(cardsInHand);
           
        }

    }

    private void Bent(List<GameObject> cards)
    {
        if (cards == null)
            return;

        var fullAngle = -bentAngle;
        var anglePerCard = fullAngle / cards.Count;
        var firstAngle = CalcFirstAngle(fullAngle);
        var handWidth = CalcHandWidth(cards.Count);

        var offsetX = pivot.position.x - handWidth / 2;

        for (var i = 0; i < cards.Count; i++ )
        {
            var card = cards[i];

            var angleTwist = firstAngle + i * anglePerCard;

            var xPos = offsetX + cardWidth / 2;

            var yDistance = Mathf.Abs(angleTwist) * height;

            var yPos = pivot.position.y - yDistance;

            var rotation = new Vector3(0, 0, angleTwist);
            var position = new Vector3(xPos, yPos, card.transform.position.z);

            card.transform.position = position;
            card.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            offsetX += cardWidth + spacing;

        }


    }

    public static float CalcFirstAngle(float fullAngle)
    {
        var magicMathFactor = 0.1f;
        return -(fullAngle / 2) + fullAngle * magicMathFactor;
    }

    private float CalcHandWidth(int quantityOfCards)
    {
        var widthCards = quantityOfCards * cardWidth;
        var widthSpacing = (quantityOfCards - 1) * spacing;
        return widthCards + widthSpacing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
