using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayer : NetworkBehaviour
{
    [SerializeField] private CardManager cardManager = null;
    [SerializeField] private DeckManager deckManager = null;

    [SyncVar]
    private string playerName;
    [SyncVar]
    private bool isOpponent;
    [SyncVar]
    private string avatarName;
    [SyncVar]
    private int deckId;

    private GameObject playerNameText;
    private GameObject playerAvatarImage;
    
    private GameObject playerHandPivot;

    public List<Card> cardsInDeck = new List<Card>();

    private int initialDealOfCards = 5;


    #region Server
    public override void OnStartServer()
    {
        //Debug.Log("Server playerName=>" + playerName);
        //Debug.Log("Server hasAuthority => " + hasAuthority);
        //Debug.Log("Server isLocalPlayer=>" + isLocalPlayer);
        //Debug.Log("Server isCLient=>" + isClient);
        //Debug.Log("Server isCLientOnly=>" + isClientOnly);
        //Debug.Log("Server isserver=>" + isServer);
        //Debug.Log("Server isserveronly=>" + isServerOnly);
        var playerDeck = deckManager.GetDeckById(deckId);
        foreach (Card crd in playerDeck.DeckList)
        {
            cardsInDeck.Add(crd);
        }


    }
    [Command]
    private void CmdSpawnCards()
    {
        GameObject parentHandPivot = null;
        for (int i = 0; i < initialDealOfCards; i++)
        {
            GameObject card = cardManager.GetCard(cardsInDeck[i].CardId);
            List<NetworkIdentity> networkIdentities = GameObject.FindObjectsOfType<NetworkIdentity>().ToList();
            if (hasAuthority)
            {
                foreach (NetworkIdentity ni in networkIdentities)
                {
                    HandPivots handPivot = ni.GetComponent<HandPivots>();
                    if (handPivot != null)
                    {
                        if (ni.netId == handPivot.playerHandPivotNetId)
                        {
                            parentHandPivot = handPivot.gameObject;
                            Debug.Log("HandPivot netId => " + handPivot.netIdentity.netId);
                        }
                    }
                        
                }
            }
            else
            {
                foreach (NetworkIdentity ni in networkIdentities)
                {
                    HandPivots handPivot = ni.GetComponent<HandPivots>();
                    if (handPivot != null)
                    {
                        if (ni.netId == handPivot.opPlayerHandPivotNetId)
                        {
                            parentHandPivot = handPivot.gameObject;
                            Debug.Log("HandPivot netId => " + handPivot.netIdentity.netId);
                        }
                    }
                        
                }
            }
            
            card.transform.SetParent(parentHandPivot.transform);
            NetworkServer.Spawn(card, connectionToClient);
            RpcSetUpCard(card, parentHandPivot);
        }
    }

    [ClientRpc]
    private void RpcSetUpCard(GameObject card, GameObject parent)
    {
        card.transform.SetParent(parent.transform, false);
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        //Debug.Log("Client playerName=>" + playerName);
        //Debug.Log("Client hasAuthority => " + hasAuthority);
        //Debug.Log("Client isLocalPlayer=>" + isLocalPlayer);
        //Debug.Log("Client isCLient=>" + isClient);
        //Debug.Log("Client isCLientOnly=>" + isClientOnly);
        //Debug.Log("Client isserver=>" + isServer);
        //Debug.Log("Client isserveronly=>" + isServerOnly);
        SetUpPlayer();
        if (hasAuthority)
            CmdSpawnCards();
    }

    #endregion

    private void SetUpPlayer()
    {
        if (hasAuthority)
        {
            playerNameText = GameObject.Find("PlayerName");
            playerAvatarImage = GameObject.Find("PlayerAvatar");
            //playerHandPivot = GameObject.Find("HandPivot");
        }
        else
        {
            playerNameText = GameObject.Find("OpPlayerName");
            playerAvatarImage = GameObject.Find("OpPlayerAvatar");
            //playerHandPivot = GameObject.Find("OpHandPivot");
        }
            

        if (playerNameText != null)
        {
            TextMeshProUGUI playerText = playerNameText.transform.GetComponent<TextMeshProUGUI>();
            playerText.text = playerName;
        }

        if (playerAvatarImage != null)
        {
            var avatar = Resources.LoadAll<Sprite>("Images/PlayerAvatars").Where(w => w.name == avatarName).FirstOrDefault();
            Image avatarImage = playerAvatarImage.transform.GetComponent<Image>();
            avatarImage.sprite = avatar;
        }
    }

    [Server]
    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    [Server]
    public void SetAvatarName(string avatarName)
    {
        this.avatarName = avatarName;
    }

    [Server]
    public void SetDeckId(int deckId)
    {
        this.deckId = deckId;
    }

    public int GetDeckId()
    {
        return deckId;
    }

}
