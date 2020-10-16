using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Button joinButton = null;
    [SerializeField] private Button hostButton = null;
    [SerializeField] private Canvas lobbyCanvas = null;

    [Header("Lobby")]
    [SerializeField] private Transform UIPlayerParent = null;
    [SerializeField] private GameObject UIPlayerPrefab = null;
    [SerializeField] private TextMeshProUGUI matchIdText = null;
    [SerializeField] private GameObject beginGameButton = null;




    public void Host()
    {
        joinMatchInput.interactable = false;
        joinButton.interactable = false;
        hostButton.interactable = false;

        Player.localPlayer.HostGame();
    }

    public void HostSuccess(bool success)
    {
        if (success)
        {
            lobbyCanvas.gameObject.SetActive(true);
            SpawnPlayerUIPrefab(Player.localPlayer);
            matchIdText.text = Player.localPlayer.MatchID;
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

        Player.localPlayer.JoinGame(joinMatchInput.text.ToUpper());
    }

    public void JoinSuccess(bool success, string matchId)
    {
        if (success)
        {
            lobbyCanvas.gameObject.SetActive(true);
            SpawnPlayerUIPrefab(Player.localPlayer);
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
}
