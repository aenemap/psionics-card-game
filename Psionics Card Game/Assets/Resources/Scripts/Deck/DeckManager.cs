using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public GameObject deck;

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
}
