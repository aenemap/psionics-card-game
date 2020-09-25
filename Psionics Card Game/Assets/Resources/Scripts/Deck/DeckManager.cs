using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour, IPointerDownHandler
{
    public CardManager cardManager;
    private GameObject deck = null;

    private List<Card> cardsInDeck = new List<Card>();

    public float time = 0.1f;
    private int initialDealOfCards = 5;

    void Start()
    {
        var playerDeck = GetDeckById(1);
        if (playerDeck != null)
        {
            foreach(Card crd in playerDeck.DeckList)
            {
                crd.IsFaceDown = true;
                cardsInDeck.Add(crd);
            }
            for (int i = 0; i < initialDealOfCards; i++)
            {
                GameObject card = cardManager.GetCard(cardsInDeck[i]);
                if (cardsInDeck.Count > 0)
                    cardsInDeck.RemoveAt(0);
                HandAreaEvents.current.AddCardToHand(card);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cardsInDeck.Count > 0)
        {
            GameObject card = cardManager.GetCard(cardsInDeck[0]);
            if (cardsInDeck.Count > 0)
                cardsInDeck.RemoveAt(0);
            if (cardsInDeck.Count == 0)
                this.gameObject.SetActive(false);
            card.transform.parent = this.transform.parent;
            card.transform.position = this.transform.position;
            Sequence dealCardSequence = DOTween.Sequence();
            dealCardSequence.Append(card.transform.DOLocalMove(new Vector3(-734, 139, card.transform.position.z), 0.5f).SetEase(Ease.OutQuint));
            dealCardSequence.Append(card.transform.DOLocalMove(new Vector3(0, -166, card.transform.position.z), 0.5f).SetEase(Ease.OutQuint));
            dealCardSequence.OnComplete(() =>
            {
                HandAreaEvents.current.AddCardToHand(card);
            });
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

    private void ShuffleDeck<T>(List<T> cards)
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
