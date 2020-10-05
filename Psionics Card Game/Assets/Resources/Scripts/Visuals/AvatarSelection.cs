using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject highlightAvatar = null;

    public string PlayerAvatarKey = "PlayerAvatar";
    private bool _isSelected;

    public static string AvatarName { get; set; }
    public bool IsSelected
    {
        get { return _isSelected; }
        set 
        { 
            _isSelected = value;
            SetSelected();
        }
    }
    private Color originalColor;

    private void Start()
    {
        originalColor = highlightAvatar.GetComponent<Image>().color;
        if (PlayerPrefs.HasKey(PlayerAvatarKey))
        {
            AvatarName = PlayerPrefs.GetString(PlayerAvatarKey);
            if (GetAvatarName(this.transform) == AvatarName)
                IsSelected = true;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach(Transform child in this.transform.parent)
        {
            if (child != this.transform)
                child.transform.GetComponent<AvatarSelection>().IsSelected = false;
        }
        IsSelected = !IsSelected;
    }

    private void SetSelected()
    {
        if (IsSelected)
        {
            highlightAvatar.GetComponent<Image>().color = new Color32(125, 255, 107, 255);
            highlightAvatar.SetActive(true);
            string avatarName = GetAvatarName(this.transform);
            PlayerPrefs.SetString(PlayerAvatarKey, avatarName);
        }
        else
        {
            highlightAvatar.SetActive(false);
            highlightAvatar.GetComponent<Image>().color = originalColor;
            PlayerPrefs.SetString(PlayerAvatarKey, string.Empty);
        }
    }

    private string GetAvatarName(Transform transform)
    {
        return transform.Find("AvatarImage").transform.GetComponent<Image>().sprite.name.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!highlightAvatar.activeSelf && !IsSelected)
            highlightAvatar.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (highlightAvatar.activeSelf && !IsSelected)
            highlightAvatar.SetActive(false);
    }
}
