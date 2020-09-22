using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DeckBase : ScriptableObject
{
    public int DeckId;
    public string DeckName;
    public List<Card> Deck;
    public List<Card> DiscardPile;

    //public abstract void AddCardsToBottomOfDeck();
    //public abstract void AddCardsToTopOfDeck();
    //public abstract void DrawCard();
    //public abstract void ShuffleDeck();


}
