using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
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

    #region Server
    public override void OnStartServer()
    {
        
    }
    #endregion

    #region Client
    public override void OnStartClient()
    {
        SetUpPlayer();
    }

    #endregion

    private void SetUpPlayer()
    {
        if (hasAuthority)
        {
            playerNameText = GameObject.Find("PlayerName");
            playerAvatarImage = GameObject.Find("PlayerAvatar");
        }
        else
        {
            playerNameText = GameObject.Find("OpPlayerName");
            playerAvatarImage = GameObject.Find("OpPlayerAvatar");
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
