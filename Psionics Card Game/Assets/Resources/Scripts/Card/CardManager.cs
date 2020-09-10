using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject card;
    public List<GameObject> GetAllCards()
    {
        List<GameObject> allCards = new List<GameObject>();
        var cardDisplay = card.GetComponent<CardDisplay>();
        List<Card> cards = GetCardsFromResource();
        foreach (var crd in cards)
        {
            cardDisplay.card = crd;
            GameObject cardToInst = Instantiate(card, new Vector2(0, 0), Quaternion.identity);            
            allCards.Add(cardToInst);
        }

        return allCards;
    }

    public GameObject GetCard (int cardId)
    {
        var cardDisplay = card.GetComponent<CardDisplay>();
        var cardPreview = card.transform.Find("CardPreview");
        var cardPreviewDisplay = cardPreview.GetComponent<CardPreviewDisplay>();

        Card findCard = GetCardsFromResource().Where(w => w.CardId == cardId).FirstOrDefault();
        if (findCard != null)
        {
            cardDisplay.card = findCard;
            cardPreviewDisplay.card = findCard;
            GameObject singleCard = Instantiate(card, new Vector2(0, 0), Quaternion.identity);
            return singleCard;
        }
        return null;
    }

    private List<Card> GetCardsFromResource()
    {
        List<Card>  cards = Resources.LoadAll("Scripts/Card/CardPool", typeof(Card)).Cast<Card>().ToList();
        return cards;
    }

}
