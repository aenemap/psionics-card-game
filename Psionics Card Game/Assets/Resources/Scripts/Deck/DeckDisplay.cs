using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckDisplay : MonoBehaviour
{
    public Deck deck;

    public Text CardsInDeck;
    void Start()
    {
        foreach(var card in deck.DeckList)
        {
            CardsInDeck.text += card.CardName + "\n";
        }
    }

}
