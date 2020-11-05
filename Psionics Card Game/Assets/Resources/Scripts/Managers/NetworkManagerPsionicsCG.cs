using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class NetworkManagerPsionicsCG : NetworkManager
{
    public static event Action<string> OnServerSceneHasChanged = null;
    public static event Action<NetworkConnection> OnClientSceneHasChanged = null;
    public override void OnServerSceneChanged(string sceneName)
    {
        OnServerSceneHasChanged?.Invoke(sceneName);
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        OnClientSceneHasChanged?.Invoke(conn);
    }
}
