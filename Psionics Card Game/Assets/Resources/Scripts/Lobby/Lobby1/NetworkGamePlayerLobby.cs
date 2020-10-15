using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.Video;
using System;

public class NetworkGamePlayerLobby : NetworkBehaviour
{

    [SyncVar]
    private string displayName = "Loading...";
    [SyncVar]
    private string avatarName = string.Empty;

    private bool _isLeader;
    public bool IsLeader
    {
        get { return _isLeader; }
        set { _isLeader = value; }
    }




    private NetworkManagerCardGame room;

    private NetworkManagerCardGame Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerCardGame;
        }
    }


    public override void OnStartClient()
    {
        DontDestroyOnLoad(gameObject);
        Room.GamePlayers.Add(this);
    }

    [Obsolete]
    public override void OnNetworkDestroy()
    {
        Room.GamePlayers.Remove(this);
    }

    [Server]
    public void SetDisplayName(string displayName)
    {
        this.displayName = displayName;
    }

    public string GetDisplayName()
    {
        return displayName;
    }

    [Server]
    public void SetAvatarName(string avatarName)
    {
        this.avatarName = avatarName;
    }

    public string GetAvatarName()
    {
        return avatarName;
    }
}
