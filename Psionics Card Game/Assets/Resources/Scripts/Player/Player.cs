using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;

    NetworkMatchChecker networkMatchChecker;
    [SyncVar]
    public string MatchID;
    [SyncVar]
    public int PlayerIndex;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        networkMatchChecker = GetComponent<NetworkMatchChecker>();

        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            UILobby.instance.SpawnPlayerUIPrefab(this);
        }
    }


    //Host Game
    public void HostGame()
    {
        string matchId = MatchMaker.GetRandomMatchID();        
        CmdHostGame(matchId);
    }

    [Command]
    private void CmdHostGame(string matchId)
    {
        MatchID = matchId;
        if (MatchMaker.instance.HostGame(matchId, gameObject, out PlayerIndex))
        {
            Debug.Log($"Game Hosted successfully ");
            networkMatchChecker.matchId = matchId.ToGuid();
            TargetHostGame(true, matchId);
        }
        else
        {
            Debug.Log($"Game Hosted failed ");
            TargetHostGame(false, matchId);
        }
    }

    [TargetRpc]
    private void TargetHostGame(bool success, string matchId)
    {
        Debug.Log($"MatchId => {matchId} == {MatchID}");
        UILobby.instance.HostSuccess(success);
    }

    //Join Game

    public void JoinGame(string matchId)
    {
        CmdJoinGame(matchId);
    }

    [Command]
    private void CmdJoinGame(string matchId)
    {
        MatchID = matchId;
        if (MatchMaker.instance.JoinGame(matchId, gameObject, out PlayerIndex))
        {
            Debug.Log($"Game Hosted successfully ");
            networkMatchChecker.matchId = matchId.ToGuid();
            TargetJoinGame(true, matchId);
        }
        else
        {
            Debug.Log($"Game Hosted failed ");
            TargetJoinGame(false, matchId);
        }
    }

    [TargetRpc]
    private void TargetJoinGame(bool success, string matchId)
    {
        MatchID = matchId;
        Debug.Log($"MatchId => {matchId} == {MatchID}");
        UILobby.instance.JoinSuccess(success, MatchID);
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
    }

    [TargetRpc]
    private void TargetBeginGame()
    {
        //Debug.Log($"MatchId => {matchId} == {MatchID}");
        //SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(2);


    }
}
