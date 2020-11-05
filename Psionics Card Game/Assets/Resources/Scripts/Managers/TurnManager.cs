using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using System.Linq;

public class TurnManager : NetworkBehaviour
{

    public static TurnManager singleton = null;
    [SerializeField] private CardManager cardManager = null;
    [SerializeField] private DeckManager deckManager = null;

    //List<Player> players = new List<Player>();
    public SyncListGameObject players = new SyncListGameObject();

    public List<Card> cardsInDeck = new List<Card>();

    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        //DontDestroyOnLoad(this);        
    }



    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }
}
