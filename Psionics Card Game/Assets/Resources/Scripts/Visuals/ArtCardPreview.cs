using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtCardPreview : MonoBehaviour
{
    public GameObject artCardPreview;
    public GameObject artCardFront;
    public GameObject artCardBack;
    public GameObject cardFront;
    public GameObject cardBack;

    private CardDisplay transformCardDisplay;

    private void OnEnable()
    {
        VisualsEvents.current.onCardToSmallPreview += CardToSmallPreview;
        VisualsEvents.current.onCardToNormalPreview += CardToNormalPreview;
    }

    // Start is called before the first frame update
    void Start()
    {
        transformCardDisplay = this.transform.GetComponent<CardDisplay>();
    }

    public void CardToSmallPreview(GameObject card)
    {
        CardDisplay cardDisplay = card.transform.GetComponent<CardDisplay>();
        if (cardDisplay.card.CardId == transformCardDisplay.card.CardId)
        {
            if (cardDisplay.card.LocationOfCard == Enums.CardLocation.ShieldsArea || cardDisplay.card.LocationOfCard == Enums.CardLocation.TalentArea)
            {
                cardFront.SetActive(false);
                cardBack.SetActive(false);
                artCardPreview.SetActive(true);
            }

            if (cardDisplay.card.IsFaceDown)
            {
                artCardFront.SetActive(false);
                artCardBack.SetActive(true);
            }
            else
            {
                artCardFront.SetActive(true);
                artCardBack.SetActive(false);
            }
        }
    }

    public void CardToNormalPreview(GameObject card)
    {
        CardDisplay cardDisplay = card.transform.GetComponent<CardDisplay>();
        if (cardDisplay.card.CardId == transformCardDisplay.card.CardId)
        {
            if (cardDisplay.card.LocationOfCard != Enums.CardLocation.ShieldsArea || cardDisplay.card.LocationOfCard != Enums.CardLocation.TalentArea)
            {
                cardFront.SetActive(true);
                artCardPreview.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        VisualsEvents.current.onCardToSmallPreview -= CardToSmallPreview;
        VisualsEvents.current.onCardToNormalPreview -= CardToNormalPreview;
    }
}
