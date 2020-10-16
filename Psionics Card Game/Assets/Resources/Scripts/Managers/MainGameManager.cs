using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct CardItem
{
    public int CardId;
    public string CardName;
}

public class MainGameManager : NetworkBehaviour
{
    [SerializeField] private CardManager cardManager = null;
    [SerializeField] private DeckManager deckManager = null;

    SyncList<CardItem> playerDeck = new SyncList<CardItem>();
    SyncList<CardItem> opponentPlayerDeck = new SyncList<CardItem>();

    List<GameObject> playerCardsInHand = new List<GameObject>();
    List<GameObject> opponentPlayerCardsInHand = new List<GameObject>();

    private int initialDealOfCards = 5;

    #region Server
    public override void OnStartServer()
    {
        List<NetworkIdentity> networkIdentities = GameObject.FindObjectsOfType<NetworkIdentity>().ToList();
        foreach (NetworkIdentity ni in networkIdentities)
        {
            GamePlayer player = ni.GetComponent<GamePlayer>();
            if (player != null)
            {
                var playerDeck = deckManager.GetDeckById(player.GetDeckId());
                if (playerDeck != null)
                {
                    foreach (Card crd in playerDeck.DeckList)
                    {
                        CardItem cardItem = new CardItem
                        {
                            CardId = crd.CardId,
                            CardName = crd.CardName
                        };
                        if (player.hasAuthority)
                        {
                            this.playerDeck.Add(cardItem);
                        }
                        else
                        {
                            this.opponentPlayerDeck.Add(cardItem);
                        }

                        //GameObject card = cardManager.GetCard(crd.CardId);
                        //NetworkServer.Spawn(card, connectionToClient);
                        //RpcSetUpCard(card, player.hasAuthority);

                    }
                }

            }
        }
    }

    [ClientRpc]
    public void RpcSetUpCard(GameObject card, bool playerHasAuthority)
    {
        GameObject parent = null;
        if (playerHasAuthority)
        {
            parent = GameObject.Find("HandPivot");
            HandAreaEvents.current.AddCardBendHand(playerCardsInHand, parent.transform);
        }
        else
        {
            parent = GameObject.Find("OpHandPivot");
            HandAreaEvents.current.AddCardBendHand(opponentPlayerCardsInHand, parent.transform);
        }
    }

    [Command]
    public void CmdSpawnCards(bool playerHasAuthority)
    {
        if (playerHasAuthority)
        {
            for (int i = 0; i < initialDealOfCards; i++)
            {
                GameObject card = cardManager.GetCard(playerDeck[i].CardId);
                NetworkServer.Spawn(card, connectionToClient);
                playerCardsInHand.Add(card);
                RpcSetUpCard(card, playerHasAuthority);
            }
        }
        else
        {
            for (int i = 0; i < initialDealOfCards; i++)
            {
                GameObject card = cardManager.GetCard(opponentPlayerDeck[i].CardId);
                NetworkServer.Spawn(card, connectionToClient);
                opponentPlayerCardsInHand.Add(card);
                RpcSetUpCard(card, playerHasAuthority);
            }
        }

    }

    #endregion

    #region Client
    public override void OnStartClient()
    {
        List<NetworkIdentity> networkIdentities = GameObject.FindObjectsOfType<NetworkIdentity>().ToList();

        foreach (NetworkIdentity ni in networkIdentities)
        {
            GamePlayer player = ni.GetComponent<GamePlayer>();
            if (player != null)
            {
                if (player.hasAuthority)
                {
                    CmdSpawnCards(true);
                }
                else
                {
                    CmdSpawnCards(false);
                }
            }
        }

    }


    #endregion

    #region Methods


    #endregion
}
