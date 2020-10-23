using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;

    [SerializeField] private CardManager cardManager = null;
    [SerializeField] private DeckManager deckManager = null;

    private int initialDealOfCards = 5;

    public List<Card> cardsInDeck = new List<Card>();

    NetworkMatchChecker networkMatchChecker;
    [SyncVar]
    public string MatchID;    
    [SyncVar]
    private string displayName;


    private void OnEnable()
    {
        NetworkManagerPsionicsCG.OnServerSceneHasChanged += HandleServerSceneHasChanged;
    }


    private void OnDisable()
    {
        NetworkManagerPsionicsCG.OnServerSceneHasChanged -= HandleServerSceneHasChanged;
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
            Debug.Log($"Game Hosted successfully ");
            SetDisplayName(playerDisplayName);
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
        NetworkManager.singleton.ServerChangeScene(Utilities.SceneName);
    }


    private void HandleServerSceneHasChanged(string sceneName)
    {
        if (sceneName == Utilities.SceneName)
        {
            Debug.Log($"Player On Change Scene => {sceneName} => isLocalPlayer = {isLocalPlayer.ToString()}");

        }

        var playerDeck = deckManager.GetDeckById(1);
        foreach (Card crd in playerDeck.DeckList)
        {
            cardsInDeck.Add(crd);
        }
        CmdSpawnCards();

    }

    //public override void OnStartServer()
    //{
    //    var playerDeck = deckManager.GetDeckById(1);
    //    foreach (Card crd in playerDeck.DeckList)
    //    {
    //        cardsInDeck.Add(crd);
    //    }
    //}

    //public override void OnStartClient()
    //{
    //    if (isLocalPlayer)
    //        CmdSpawnCards();
    //}

    [Command(ignoreAuthority = true)]
    private void CmdSpawnCards()
    {
        for (int i = 0; i < initialDealOfCards; i++)
        {
            GameObject card = cardManager.GetCard(cardsInDeck[i].CardId);

            GameObject parent = null;
            if (isLocalPlayer)
                parent = GameObject.Find("HandPivot");
            else
            {
                parent = GameObject.Find("OpHandPivot");
            }
                


            if (parent != null)
            {
                Debug.Log($"Setting the parent in Server => {parent.name}");
                card.transform.SetParent(parent.transform);
                card.transform.position = parent.transform.position;
            }
            NetworkServer.Spawn(card, connectionToClient);
            Debug.Log(card.GetComponent<NetworkIdentity>().netId);
            TargetSetUpCard(card);
        }
    }

    [TargetRpc]
    private void TargetSetUpCard(GameObject card)
    {
        if(card != null)
        {
            Debug.Log("THE CARD IS NOT NULL");
            GameObject parent = GameObject.Find("HandPivot");
            card.transform.SetParent(parent.transform);
            card.transform.position = parent.transform.position;
        }
    }


    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    public string GetDisplayName()
    {
        return displayName;
    }
}
