using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public class Match
{
    public string MatchId;
    //public SyncList<GameObject> players = new SyncList<GameObject>();
    public SyncListGameObject players = new SyncListGameObject();

    public Match(string matchId, GameObject player)
    {
        this.MatchId = matchId;
        players.Add(player);

    }

    public Match()
    {

    }
}

[System.Serializable]
public class SyncListGameObject : SyncList<GameObject> { }

[System.Serializable]
public class SyncListMatch : SyncList<Match> { }

public class MatchMaker : NetworkBehaviour
{
    public static MatchMaker instance;

    //public SyncList<Match> matches = new SyncList<Match>();
    public SyncListMatch matches = new SyncListMatch();
    public SyncList<string> matchIds = new SyncList<string>();
    //SyncListString matchIds = new SyncListString();


    [SerializeField] private GameObject turnManagerPrefab = null;
    [SerializeField] private GameObject visualEventsPrefab = null;

    public MatchMaker()
    {
        instance = this;
    }

    private void OnEnable()
    {
        NetworkManagerPsionicsCG.OnServerSceneHasChanged += HandleServerSceneHasChanged;
    }

    private void OnDisable()
    {
        NetworkManagerPsionicsCG.OnServerSceneHasChanged -= HandleServerSceneHasChanged;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public bool HostGame(string matchId, GameObject player)
    {
        if (!matchIds.Contains(matchId))
        {
            matchIds.Add(matchId);
            matches.Add(new Match(matchId, player));
            Debug.Log($"Match Generated");
            return true;
        }
        else
        {
            Debug.Log($"Match Id already exists");
            return false;
        }

    }

    public bool JoinGame(string matchId, GameObject player)
    {
        if (matchIds.Contains(matchId))
        {
            matchIds.Add(matchId);

            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].MatchId == matchId)
                {
                    matches[i].players.Add(player);
                    break;
                }
            }

            Debug.Log($"Match Joined");
            return true;
        }
        else
        {
            Debug.Log($"Match Id does not exists");
            return false;
        }

    }

    public void BeginGame(string matchId)
    {
        //GameObject turnManagerInstance = Instantiate(turnManagerPrefab);

        //GameObject visualEventsInstance = Instantiate(visualEventsPrefab);
        //NetworkServer.Spawn(visualEventsInstance);
        //turnManagerInstance.GetComponent<NetworkMatchChecker>().matchId = matchId.ToGuid();
        //TurnManager turnManager = turnManagerInstance.GetComponent<TurnManager>();

        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].MatchId == matchId)
            {
                foreach (var player in matches[i].players)
                {
                    Player plr = player.GetComponent<Player>();
                    //turnManager.AddPlayer(plr);
                    plr.StartGame();
                }
                break;
            }
        }
        //NetworkServer.Spawn(turnManagerInstance);
    }

    private void HandleServerSceneHasChanged(string sceneName)
    {
        if (sceneName == Utilities.SceneName)
        {
            Debug.Log($"HandleServerSceneHasChanged Scene has Changed => SceneName => {sceneName}");
            GameObject turnManagerInstance = Instantiate(turnManagerPrefab);
            turnManagerInstance.GetComponent<NetworkMatchChecker>().matchId = Player.localPlayer.MatchID.ToGuid();
            TurnManager turnManager = turnManagerInstance.GetComponent<TurnManager>();
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].MatchId == Player.localPlayer.MatchID)
                {
                    foreach (var player in matches[i].players)
                    {
                        Player plr = player.GetComponent<Player>();
                        turnManager.AddPlayer(plr);                        
                    }
                    break;
                }
            }
            NetworkServer.Spawn(turnManagerInstance);
            GameObject visualEventsInstance = Instantiate(visualEventsPrefab);
            NetworkServer.Spawn(visualEventsInstance);
        }
    }


    public static string GetRandomMatchID()
    {
        string id = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26)
            {
                id += (char)(random + 65);
            }
            else
            {
                id += (random - 26).ToString();
            }
        }

        Debug.Log($"GetRandoMatchID => MatchID => {id}");
        return id;
    }
}
