using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour, IPointerDownHandler
{
    public CardManager cardManager;
    private GameObject deck = null;

    private List<Card> cardsInDeck = new List<Card>();

    public float time = 0.3f;
    private int initialDealOfCards = 5;
    private float delayBeforeDealCards = 1f;
    private float timeElapsed;
    private bool initialDeal = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().isLoaded)
        {
            timeElapsed += Time.deltaTime;
            if (timeElapsed > delayBeforeDealCards && !initialDeal)
            {
                initialDeal = true;
                var playerDeck = GetDeckById(1);
                if (playerDeck != null)
                {
                    foreach (Card crd in playerDeck.DeckList)
                    {
                        crd.LocationOfCard = Enums.CardLocation.Deck;
                        cardsInDeck.Add(crd);
                    }
                    StartCoroutine(DealInitialCards());
                }
                
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cardsInDeck.Count > 0)
        {
            DealCard();
        }

    }

    private void DealCard()
    {
        GameObject card = cardManager.GetCard(cardsInDeck[0].CardId);
        if (cardsInDeck.Count > 0)
            cardsInDeck.RemoveAt(0);
        if (cardsInDeck.Count == 0)
        {
            this.gameObject.SetActive(false);
        }
        card.transform.SetParent(this.transform.parent);
        card.transform.position = this.transform.position;
        HoverPreview.PreviewsAllowed = false;
        Sequence dealCardSequence = DOTween.Sequence();

        dealCardSequence.Append(card.transform.DOMove(new Vector3(-734, 139, card.transform.position.z), time).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            card.StartRotation(Enums.CardState.FaceUp);
            //CardRotation cardRotation = card.transform.GetComponent<CardRotation>();
            //if (cardRotation.cardState == Enums.CardState.FaceDown)
            //{
            //    cardRotation.cardState = Enums.CardState.FaceUp;
            //    cardRotation.FromDeckStartFaceUp();
            //}
        }));

        dealCardSequence.Append(card.transform.DOMove(new Vector3(0, -166, card.transform.position.z), time).SetDelay(0.5f).SetEase(Ease.OutQuint));
        dealCardSequence.OnComplete(() =>
        {
            HandAreaEvents.current.AddCardToHand(card, null);
            HoverPreview.PreviewsAllowed = true;
        });
    }

    IEnumerator DealInitialCards()
    {
        for (int i = 0; i < initialDealOfCards; i++)
        {
            DealCard();
            yield return new WaitForSeconds(1f);
        }
            
    }

    public List<GameObject> GetAllDecks()
    {
        List<GameObject> allDecks = new List<GameObject>();
        var deckDisplay = deck.GetComponent<DeckDisplay>();
        List<Deck> decks = GetDecksFromResource();
        foreach(var dek in decks)
        {
            deckDisplay.deck = dek;
            GameObject deckToInst = Instantiate(deck, new Vector2(0, 0), Quaternion.identity);
            allDecks.Add(deckToInst);
        }

        return allDecks;
    }

    public GameObject GetGODeckById(int deckId)
    {
        var deckDisplay = deck.GetComponent<DeckDisplay>();
        Deck findDeck = GetDecksFromResource().Where(w => w.DeckId == deckId).FirstOrDefault();
        if (findDeck != null)
        {
            deckDisplay.deck = findDeck;
            GameObject singleDeck = Instantiate(deck, new Vector2(0, 0), Quaternion.identity);
            return singleDeck;
        }
        return null;
    }

    public Deck GetDeckById(int deckId)
    {
        Deck findDeck = GetDecksFromResource().Where(w => w.DeckId == deckId).FirstOrDefault();
        return findDeck;
    }

    private List<Deck> GetDecksFromResource()
    {
        List<Deck> decks = Resources.LoadAll("Scripts/Deck/DeckPool", typeof(Deck)).Cast<Deck>().ToList();
        return decks;
    }

    public void ShuffleDeck<T>(List<T> cards)
    {
        System.Random rng = new System.Random();
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
            
        }
    }

    private void OnDestroy()
    {
        
    }

}
