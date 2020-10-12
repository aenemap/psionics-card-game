using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : NetworkBehaviour
{
    [SerializeField] private CardManager cardManager;
    [SerializeField] private DeckManager deckManager;
    public override void OnStartClient()
    {
        Debug.Log("MainGameManager Here");
    }
}
