using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameInput : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameInputField = null;
    [SerializeField] private Button continueButton = null;
    [SerializeField] private GameObject landingPage = null;

    public static string DisplayName { get; private set; }
    public static string AvatarName { get; private set; }

    private void Start()
    {
        SetUpInputField();
    }

    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(Enums.PlayerPrefKeys.PlayerName.ToString())) { return; }

        string defaultName = PlayerPrefs.GetString(Enums.PlayerPrefKeys.PlayerName.ToString());
        nameInputField.text = defaultName;
        SetPlayerName(defaultName);
    }

    public void SetPlayerName(string defaultName)
    {
        continueButton.interactable = !string.IsNullOrEmpty(name);
    }

    public void SavePlayerName()
    {
        DisplayName = nameInputField.text;
        PlayerPrefs.SetString(Enums.PlayerPrefKeys.PlayerName.ToString(), DisplayName);
        if (!PlayerPrefs.HasKey(Enums.PlayerPrefKeys.PlayerAvatar.ToString()) || PlayerPrefs.GetString(Enums.PlayerPrefKeys.PlayerAvatar.ToString()) == string.Empty)
        {
            Debug.Log("Please choose an avatar");
            return;
        }
        else
        {
            AvatarName = PlayerPrefs.GetString(Enums.PlayerPrefKeys.PlayerAvatar.ToString());
            landingPage.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
