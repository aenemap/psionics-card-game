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

    [SerializeField] GameObject turnManagerPrefab;

    public MatchMaker()
    {
        instance = this;
    }

    public bool HostGame(string matchId, GameObject player, out int playerIndex)
    {
        playerIndex = -1;
        if (!matchIds.Contains(matchId))
        {
            matchIds.Add(matchId);
            matches.Add(new Match(matchId, player));
            Debug.Log($"Match Generated");
            playerIndex = 1;
            return true;
        }
        else
        {
            Debug.Log($"Match Id already exists");
            return false;
        }

    }

    public bool JoinGame(string matchId, GameObject player, out int playerIndex)
    {
        playerIndex = -1;
        if (matchIds.Contains(matchId))
        {
            matchIds.Add(matchId);

            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].MatchId == matchId)
                {
                    matches[i].players.Add(player);
                    playerIndex = matches[i].players.Count;
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
        GameObject turnManagerInstance = Instantiate(turnManagerPrefab);
        NetworkServer.Spawn(turnManagerInstance);
        turnManagerInstance.GetComponent<NetworkMatchChecker>().matchId = matchId.ToGuid();
        TurnManager turnManager = turnManagerInstance.GetComponent<TurnManager>();

        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].MatchId == matchId)
            {
                foreach (var player in matches[i].players)
                {
                    Player plr = player.GetComponent<Player>();
                    turnManager.AddPlayer(plr);
                    plr.StartGame();
                }
                break;
            }
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
