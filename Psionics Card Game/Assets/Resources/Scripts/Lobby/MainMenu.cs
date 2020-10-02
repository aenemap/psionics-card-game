using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private NetworkManagerLobby networkManager = null;

    [Header("UI")]
    [SerializeField] private GameObject landingPagePanel = null;


    public void HostLoby()
    {
        networkManager.StartHost();
        landingPagePanel.SetActive(false);
    }
}
