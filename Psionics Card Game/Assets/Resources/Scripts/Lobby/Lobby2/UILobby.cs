using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    public static UILobby instance;

    public UILobby()
    {
        instance = this;
    }

    [Header("Host Join")]
    [SerializeField] private TMP_InputField joinMatchInput = null;
    [SerializeField] private TMP_InputField displayName = null;
    [SerializeField] private Button joinButton = null;
    [SerializeField] private Button hostButton = null;
    [SerializeField] private Canvas lobbyCanvas = null;

    [Header("Lobby")]
    [SerializeField] private Transform UIPlayerParent = null;
    [SerializeField] private GameObject UIPlayerPrefab = null;
    [SerializeField] private TextMeshProUGUI matchIdText = null;
    [SerializeField] private GameObject beginGameButton = null;

    public string playerName;

    private void Start()
    {
        SetUpPlayer();
    }

    private void SetUpPlayer()
    {
        //if (!PlayerPrefs.HasKey(Enums.PlayerPrefKeys.PlayerName.ToString())) { return; }

        //string defaultName = PlayerPrefs.GetString(Enums.PlayerPrefKeys.PlayerName.ToString());
        //displayName.text = defaultName;
    }

    public void Host()
    {
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;

        //PlayerPrefs.SetString(Enums.PlayerPrefKeys.PlayerName.ToString(), displayName.text);
        string playerName = string.Empty;
        if (displayName.text == string.Empty)
        {
            playerName = "Player-" + MatchMaker.GetRandomMatchID();
        }
        else
        {
            playerName = displayName.text;
        }
        Player.localPlayer.HostGame(playerName);
    }

    public void HostSuccess(bool success, string matchId, List<GameObject> players)
    {
        if (success)
        {
            lobbyCanvas.gameObject.SetActive(true);            
            UpdateDisplay(players);
            matchIdText.text = matchId;
            beginGameButton.SetActive(true);
        }
        else
        {
            joinMatchInput.interactable = true;
            joinButton.interactable = true;
            hostButton.interactable = true;
        }
    }

    public void Join()
    {
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;

        //PlayerPrefs.SetString(Enums.PlayerPrefKeys.PlayerName.ToString(), displayName.text);
        string playerName = string.Empty;
        if (displayName.text == string.Empty)
        {
            playerName = "Player-" + MatchMaker.GetRandomMatchID();
        }
        else
        {
            playerName = displayName.text;
        }
        Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper(), playerName);
    }

    public void JoinSuccess(bool success, string matchId, Player player, List<GameObject> players)
    {
        if (success)
        {
            lobbyCanvas.gameObject.SetActive(true);
            UpdateDisplay(players);
            matchIdText.text = matchId;
        }
        else
        {
            joinMatchInput.interactable = true;
            joinButton.interactable = true;
            hostButton.interactable = true;
        }
    }

    public void SpawnPlayerUIPrefab(Player player)
    {
        GameObject newUIPlayer = Instantiate(UIPlayerPrefab, UIPlayerParent);
        newUIPlayer.GetComponent<UIPlayer>().SetPlayer(player);
    }

    public void BeginGame()
    {
        Player.localPlayer.BeginGame();
    }

    public void UpdateDisplay(List<GameObject> players)
    {
        foreach(Transform child in UIPlayerParent)
        {
            Destroy(child.gameObject);
        }

        players = players.OrderBy(o => !o.GetComponent<Player>().isLocalPlayer).ToList();

        foreach (var player in players)
        {
            Player plr = player.GetComponent<Player>();
            SpawnPlayerUIPrefab(plr);
        }    
    }
}
