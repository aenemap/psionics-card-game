using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainGameManager : NetworkBehaviour
{
    [SerializeField] private CardManager cardManager;
    [SerializeField] private DeckManager deckManager;

    #region Server
    public override void OnStartServer()
    {
        //List<NetworkIdentity> networkIdentities = GameObject.FindObjectsOfType<NetworkIdentity>().ToList();
        //foreach (NetworkIdentity ni in networkIdentities)
        //{
        //    Player player = ni.GetComponent<Player>();
        //    if (player)
        //    {
        //        if (player.hasAuthority)
        //        {
        //            var playerDeck = deckManager.GetDeckById(player.GetDeckId());
        //            if (playerDeck != null)
        //            {
        //                foreach (Card crd in playerDeck.DeckList)
        //                {
        //                    this.playerDeck.Add(crd);
        //                }
        //            }
        //        }
        //    }
        //}
    }

    #endregion

    #region Client
    public override void OnStartClient()
    {
        List<NetworkIdentity> networkIdentities = GameObject.FindObjectsOfType<NetworkIdentity>().ToList();

        foreach (NetworkIdentity ni in networkIdentities)
        {
            Player player = ni.GetComponent<Player>();
            if (player)
            {
                if (player.hasAuthority)
                {
                    var playerDeck = deckManager.GetDeckById(player.GetDeckId());
                    if (playerDeck != null)
                    {
                        foreach (Card crd in playerDeck.DeckList)
                        {
                            Debug.Log(crd.CardName);
                        }
                    }
                }
            }
        }

    }
    #endregion
}
