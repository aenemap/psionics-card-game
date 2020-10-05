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
    [SerializeField] private AvatarSelection avatarSelection = null;
    [SerializeField] private GameObject landingPage = null;

    public static string DisplayName { get; private set; }

    public const string PlayerPrefsNameKey = "PlayerName";

    private void Start()
    {
        SetUpInputField();
    }

    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsNameKey)) { return; }

        string defaultName = PlayerPrefs.GetString(PlayerPrefsNameKey);
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
        PlayerPrefs.SetString(PlayerPrefsNameKey, DisplayName);
        if (!PlayerPrefs.HasKey(avatarSelection.PlayerAvatarKey) || PlayerPrefs.GetString(avatarSelection.PlayerAvatarKey) == string.Empty)
        {
            Debug.Log("Please choose an avatar");
            return;
        }
        else
        {
            landingPage.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
