using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : NetworkBehaviour
{

    List<Player> players = new List<Player>();

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }
}
