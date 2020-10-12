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

    private GameObject playerNameText;
    private GameObject playerAvatarImage;
    public override void OnStartClient()
    {
        SetUpPlayer();
    }

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
    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
    }

    public void SetIsOpponent(bool isOpponent)
    {
        this.isOpponent = isOpponent;
    }

    public void SetAvatarName(string avatarName)
    {
        this.avatarName = avatarName;
    }

}
