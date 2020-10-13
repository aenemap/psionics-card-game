using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManagerCardGame : NetworkManager
{
    [SerializeField] private int minPlayers = 2;
    //private string menuScene = "LobbyScene";

    [Scene] [SerializeField] private string menuScene = string.Empty;
    [Scene] [SerializeField] private string mainGameScene = string.Empty;

    [Header("Room")]
    [SerializeField] private NetworkRoomPlayerLobby roomPlayerPrefab = null;

    [Header("Game")]
    [SerializeField] private NetworkGamePlayerLobby gamePlayerPrefab = null;
    //[SerializeField] private GameObject playerSpawnSystem = null;
    [SerializeField] private GameObject mainGameManagerPrefab = null;

    [Header("Players")]
    [SerializeField] private Player prefabForPlayer = null;


    public static event Action OnClientConnected;
    public static event Action OnClientDisconnected;
    public static event Action<NetworkConnection> OnServerReadied;

    public List<NetworkRoomPlayerLobby> RoomPlayers { get; } = new List<NetworkRoomPlayerLobby>();
    public List<NetworkGamePlayerLobby> GamePlayers { get; } = new List<NetworkGamePlayerLobby>();    

    public override void OnStartServer()
    {
        spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
    }

    public override void OnStartClient()
    {
        var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs");

        foreach (var prefab in spawnablePrefabs)
        {
            ClientScene.RegisterPrefab(prefab);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        OnClientConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        OnClientDisconnected?.Invoke();
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if(numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if (SceneManager.GetActiveScene().path != menuScene)
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            bool isLeader = RoomPlayers.Count == 0;
            NetworkRoomPlayerLobby roomPlayerInstance = Instantiate(roomPlayerPrefab);
            roomPlayerInstance.IsLeader = isLeader;
            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            var player = conn.identity.GetComponent<NetworkRoomPlayerLobby>();
            RoomPlayers.Remove(player);
            NotifyPlayersOfReadyState();
        }

        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        RoomPlayers.Clear();
        GamePlayers.Clear();
    }

    public void NotifyPlayersOfReadyState()
    {
        foreach(var player in RoomPlayers)
        {
            player.HandleReadyToStart(IsReadyToStart());
        }
    }

    private bool IsReadyToStart()
    {
        if (numPlayers < minPlayers) { return false; }
        foreach(var player in RoomPlayers)
        {
            if (!player.IsReady) { return false; }
        }

        return true;
    }

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            if (!IsReadyToStart()) { return; }

            ServerChangeScene("MainGameScene");

        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        if (SceneManager.GetActiveScene().path == menuScene)
        {
            for (int i = RoomPlayers.Count - 1; i >= 0; i--)
            {
                var conn = RoomPlayers[i].connectionToClient;
                var gamePlayerInstance = Instantiate(gamePlayerPrefab);
                gamePlayerInstance.SetDisplayName(RoomPlayers[i].DisplayName);
                gamePlayerInstance.IsLeader = RoomPlayers[i].IsLeader;
                gamePlayerInstance.SetAvatarName(RoomPlayers[i].AvatarName);
                NetworkServer.Destroy(conn.identity.gameObject);
                NetworkServer.ReplacePlayerForConnection(conn, gamePlayerInstance.gameObject, true);
            }
        }
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string newSceneName)
    {
        if (newSceneName.Equals("MainGameScene"))
        {
            GameObject mainGameManagerInstance = Instantiate(mainGameManagerPrefab);
            NetworkServer.Spawn(mainGameManagerInstance);
            for (int i = 0; i < GamePlayers.Count; i++)
            {
                var conn = GamePlayers[i].connectionToClient;
                var playerInstance = Instantiate(prefabForPlayer);
                playerInstance.SetPlayerName(GamePlayers[i].GetDisplayName());
                playerInstance.SetAvatarName(GamePlayers[i].GetAvatarName());
                playerInstance.SetDeckId(1);
                NetworkServer.Spawn(playerInstance.gameObject, conn);
            }

        }
    }

    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);

        OnServerReadied?.Invoke(conn);
    }

}
