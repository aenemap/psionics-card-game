using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class NetworkManagerPsionicsCG : NetworkManager
{
    public static event Action<string> OnServerSceneHasChanged = null;
    public override void OnServerSceneChanged(string sceneName)
    {
        OnServerSceneHasChanged?.Invoke(sceneName);
    }
}
