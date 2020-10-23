using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : NetworkBehaviour
{
    [SerializeField] private CardManager cardManager = null;
    [SerializeField] private DeckManager deckManager = null;

    List<Player> players = new List<Player>();
    private int initialDealOfCards = 5;

    public List<Card> cardsInDeck = new List<Card>();

    private void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        foreach(Player player in players)
        {
            Debug.Log($"TurnManager Player => {player.GetDisplayName()}");
        }
    }

    [TargetRpc]
    private void TargetJustAMessage()
    {
        Debug.Log("GET THIS MESSAGE MOTHERFUCKER");
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    //public override void OnStartServer()
    //{
    //    //Debug.Log($"OnStartServer Players in TurnManager => {players.Count}");
    //    //foreach(Player player in players)
    //    //{
    //    //    Debug.Log($"Player has authority => {player.hasAuthority}");
    //    //}
    //    Debug.Log($"OnStartServer Turn Manager Has Authority => {hasAuthority.ToString()}");
    //    var playerDeck = deckManager.GetDeckById(1);
    //    foreach (Card crd in playerDeck.DeckList)
    //    {
    //        cardsInDeck.Add(crd);
    //    }

    //}

    //public override void OnStartClient()
    //{
    //    //Debug.Log($"OnStartClient Players in TurnManager => {players.Count}");
    //    //foreach (Player player in players)
    //    //{
    //    //    Debug.Log($"Player has authority => {player.hasAuthority}");
    //    //}
    //    CmdSpawnCards();
    //}


    //[Command(ignoreAuthority = true)]
    //private void CmdSpawnCards()
    //{
    //    Debug.Log($"CmdSpawnCards Players in TurnManager => {players.Count}");
    //    foreach(Player player in players)
    //    {
    //        Debug.Log($"CmdSpawnCards player.islocalPlayer => {player.isLocalPlayer}");
    //        if (player.isLocalPlayer)
    //        {
    //            for (int i = 0; i < initialDealOfCards; i++)
    //            {
    //                GameObject card = cardManager.GetCard(cardsInDeck[i].CardId);
    //                NetworkServer.Spawn(card, connectionToClient);
    //                RpcSetUpCard(card, player.isLocalPlayer);
    //            }
    //        }
    //    }
    //}



    //[ClientRpc]
    //public void RpcSetUpCard(GameObject card, bool isLocalPlayer)
    //{
    //    GameObject handpivot = null;
    //    if (isLocalPlayer)
    //        handpivot = GameObject.Find("HandPivot");
    //    else
    //        handpivot = GameObject.Find("OpHandPivot");

    //    card.transform.SetParent(handpivot.transform);


    //}
}
