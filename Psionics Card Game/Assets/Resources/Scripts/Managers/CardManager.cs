using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject shieldCard;
    public GameObject shieldAbsorbingCard;
    public GameObject talentCard;
    public GameObject eventCard;
    public GameObject testCard;

    private GameObject card;
    public List<GameObject> GetAllCards()
    {
        List<GameObject> allCards = new List<GameObject>();
        var cardDisplay = card.GetComponent<CardDisplay>();
        List<Card> cards = GetCardsFromResource();
        foreach (var crd in cards)
        {
            card = GetTypeOfCard(crd);
            cardDisplay.card = crd;
            GameObject cardToInst = Instantiate(card, new Vector2(0, 0), Quaternion.identity);            
            allCards.Add(cardToInst);
        }

        return allCards;
    }

    public GameObject GetCard (int cardId)
    {
        Card findCard = GetCardsFromResource().Where(w => w.CardId == cardId).FirstOrDefault();
        card = GetTypeOfCard(findCard);
        var cardDisplay = card.GetComponent<CardDisplay>();
        var cardPreview = card.transform.Find("CardPreview");
        var cardPreviewDisplay = cardPreview.GetComponent<CardPreviewDisplay>();
        var canvas = card.transform.Find("Canvas");
        var cardArt = canvas.transform.Find("ArtCard");
        ArtCardPreviewDisplay cardArtPreviewDisplay = null;
        if (cardArt != null)
            cardArtPreviewDisplay = cardArt.GetComponent<ArtCardPreviewDisplay>();


        if (findCard != null)
        {
            cardDisplay.card = findCard;
            cardPreviewDisplay.card = findCard;
            if (cardArtPreviewDisplay != null)
                cardArtPreviewDisplay.card = findCard;
            GameObject singleCard = Instantiate(card, new Vector3(0, 0, -0.01f), Quaternion.identity);
            
            CardRotation cardRotation = singleCard.transform.GetComponent<CardRotation>();
            cardRotation.cardState = Enums.CardState.FaceDown;
            cardRotation.Flip();
            

            return singleCard;
        }
        return null;
    }

    public GameObject GetGameObjectCard(int cardId)
    {
        Card findCard = GetCardsFromResource().Where(w => w.CardId == cardId).FirstOrDefault();
        card = GetTypeOfCard(findCard);
        var cardDisplay = card.GetComponent<CardDisplay>();
        var cardPreview = card.transform.Find("CardPreview");
        var cardPreviewDisplay = cardPreview.GetComponent<CardPreviewDisplay>();
        var canvas = card.transform.Find("Canvas");
        var cardArt = canvas.transform.Find("ArtCard");
        ArtCardPreviewDisplay cardArtPreviewDisplay = null;
        if (cardArt != null)
            cardArtPreviewDisplay = cardArt.GetComponent<ArtCardPreviewDisplay>();


        if (findCard != null)
        {
            cardDisplay.card = findCard;
            cardPreviewDisplay.card = findCard;
            if (cardArtPreviewDisplay != null)
                cardArtPreviewDisplay.card = findCard;

            return card;
        }
        return null;
    }

    private GameObject GetTypeOfCard(Card card)
    {
        GameObject returnCard;
        if (card.CardType == Enums.CardType.Shield && card.CardSubType == Enums.CardSubType.Absorbing)
            returnCard = shieldAbsorbingCard;
        else
            returnCard = shieldCard;

        if (card.CardType == Enums.CardType.Talent)
            returnCard = talentCard;

        if (card.CardType == Enums.CardType.Event)
            returnCard = eventCard;

        if (card.CardType == Enums.CardType.Test)
            returnCard = testCard;

        return returnCard;
    }

    private List<Card> GetCardsFromResource()
    {
        List<Card>  cards = Resources.LoadAll("Scripts/Card/CardPool", typeof(Card)).Cast<Card>().ToList();
        return cards;
    }

}
