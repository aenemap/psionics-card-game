using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using System.Collections;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;

    [SerializeField] private CardManager cardManager = null;
    [SerializeField] private DeckManager deckManager = null;
    [SerializeField] private GameObject samplePrefab = null;

    private int initialDealOfCards = 5;

    private List<Card> cardsInDeck = new List<Card>();
    private SyncListGameObject cardsInHand = new SyncListGameObject();

    NetworkMatchChecker networkMatchChecker;
    [SyncVar]
    public string MatchID;    
    [SyncVar]
    private string displayName;
    [SyncVar]
    private int deckId;
    [SyncVar]
    private NetworkIdentityReference _card = new NetworkIdentityReference();

    private void OnEnable()
    {
        NetworkManagerPsionicsCG.OnClientSceneHasChanged += HandleClientSceneHasChange;
    }


    private void OnDisable()
    {
        NetworkManagerPsionicsCG.OnClientSceneHasChanged -= HandleClientSceneHasChange;
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        networkMatchChecker = GetComponent<NetworkMatchChecker>();

        if (isLocalPlayer)
        {
            string name = displayName;
            localPlayer = this;
        }
    }


    //Host Game
    public void HostGame(string playerDisplayName)
    {
        string matchId = MatchMaker.GetRandomMatchID();        
        CmdHostGame(matchId, playerDisplayName);
    }


    [Command]
    private void CmdHostGame(string matchId, string playerDisplayName)
    {
        Match match = null;
        List<GameObject> players = new List<GameObject>();
        MatchID = matchId;
        if (MatchMaker.instance.HostGame(matchId, gameObject))
        {
            Debug.Log($"Game Hosted successfully ");
            SetDisplayName(playerDisplayName);
            this.deckId = 10;
            match = MatchMaker.instance.matches.Where(w => w.MatchId == matchId).FirstOrDefault();
            foreach (var player in match.players)
            {
                players.Add(player);
            }
            networkMatchChecker.matchId = matchId.ToGuid();
            TargetHostGame(true, matchId, playerDisplayName, players);
        }
        else
        {
            Debug.Log($"Game Hosted failed ");
            TargetHostGame(false, matchId, playerDisplayName, players);
        }
    }

    [TargetRpc]
    private void TargetHostGame(bool success, string matchId, string playerDisplayName, List<GameObject> players)
    {
        MatchID = matchId;
        Debug.Log($"MatchId => {matchId} == {MatchID}");
        SetDisplayName(playerDisplayName);
        UILobby.instance.HostSuccess(success, matchId, players);
    }

    //Join Game

    public void JoinGame(string matchId, string playerDisplayName)
    {
        CmdJoinGame(matchId, playerDisplayName);
    }

    [Command]
    private void CmdJoinGame(string matchId, string playerDisplayName)
    {
        Match match = null;
        List<GameObject> players = new List<GameObject>();
        MatchID = matchId;
        if (MatchMaker.instance.JoinGame(matchId, gameObject))
        {
            Debug.Log($"Game Joined successfully ");
            SetDisplayName(playerDisplayName);
            this.deckId = 11;
            match = MatchMaker.instance.matches.Where(w => w.MatchId == matchId).FirstOrDefault();
            foreach (var player in match.players)
            {
                players.Add(player);
            }
            UILobby.instance.UpdateDisplay(players);
            networkMatchChecker.matchId = matchId.ToGuid();
            RpcJoinGame(true, matchId, playerDisplayName, players);
        }
        else
        {
            Debug.Log($"Game Hosted failed ");
            RpcJoinGame(false, matchId, playerDisplayName, players);
        }
    }

    [ClientRpc]
    private void RpcJoinGame(bool success, string matchId, string playerDisplayName, List<GameObject> players)
    {
        MatchID = matchId;
        int playerCount = players.Count;
        Debug.Log($"MatchId => {matchId} == {MatchID}");
        SetDisplayName(playerDisplayName);
        UILobby.instance.JoinSuccess(success, MatchID, this, players);
    }

    //BeginGame

    public void BeginGame()
    {
        CmdBeginGame();
    }

    [Command]
    private void CmdBeginGame()
    {
        MatchMaker.instance.BeginGame(MatchID);
        Debug.Log($"Game Beginning");
        
        
    }

    public void StartGame()
    {
        TargetBeginGame();
        //NetworkManager.singleton.ServerChangeScene(Utilities.SceneName);
    }

    [TargetRpc]
    private void TargetBeginGame()
    {
        //Debug.Log($"MatchId => {matchId} == {MatchID}");
        //SceneManager.LoadScene(2, LoadSceneMode.Additive);        
        //SceneManager.LoadScene(2);
        NetworkManager.singleton.ServerChangeScene(Utilities.MainGameSceneName);
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(2);
        //asyncOperation.completed += AsyncOperation_completed;
    }

    //private void AsyncOperation_completed(AsyncOperation obj)
    //{
    //    if (isLocalPlayer)
    //    {

    //        DealCards();
    //    }
    //}

    private void HandleClientSceneHasChange(NetworkConnection conn)
    {
        if (isLocalPlayer)
        {

            DealCards();
        }

    }

    private void DealCards()
    {
        CmdFillDecksWithCards();
        for (int i = 0; i < initialDealOfCards; i++)
        {
            CmdSpawnCards();
        }
        //CmdMessageToDebug();
    }

    [Command]
    private void CmdFillDecksWithCards()
    {
        var playerDeck = deckManager.GetDeckById(this.deckId);
        foreach (Card crd in playerDeck.DeckList)
        {
            cardsInDeck.Add(crd);
        }
    }

    [Command]
    private void CmdSpawnCards(NetworkConnectionToClient sender = null)
    {
        if (_card.Value != null)
            return;
        GameObject card = cardManager.GetGameObjectCard(cardsInDeck[0].CardId);
        card.GetComponent<NetworkMatchChecker>().matchId = this.MatchID.ToGuid();

        Debug.Log($"PLAYER NAME => {this.GetDisplayName()} - CardName => {card.GetCardAsset().CardName}");
        if (cardsInDeck.Count > 0)
            cardsInDeck.RemoveAt(0);
        cardsInHand.Add(card);

        GameObject playerCard = Instantiate(card);

        Debug.Log($"CardsInHand => {cardsInHand.Count}");
        NetworkServer.Spawn(playerCard, connectionToClient);
        _card = new NetworkIdentityReference(playerCard.GetComponent<NetworkIdentity>());
        TargetSetUpCard(playerCard);
    }

    [TargetRpc]
    private void TargetSetUpCard(GameObject card)
    {
        //TextMeshProUGUI debugText = GameObject.Find("DebugText").GetComponent<TextMeshProUGUI>();
        //debugText.text += $"PLAYER NAME => {this.GetDisplayName()} CardNetId => {card.GetComponent<NetworkIdentity>().netId}\n";
        GameManagerUI.singleton.SetUpCard(card, this.isLocalPlayer);
    }



    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    public List<GameObject> GetCardsInHand()
    {
        return cardsInHand.ToList();
    }

    public List<Card> GetCardsInDeck()
    {
        return cardsInDeck;
    }

}
