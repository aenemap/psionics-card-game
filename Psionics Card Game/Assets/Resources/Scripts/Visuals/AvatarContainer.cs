using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarContainer : MonoBehaviour
{
    [SerializeField] private GameObject avatarSelectionImage = null;
    void Start()
    {
        PopulateAvatars();
    }

    private void PopulateAvatars()
    {
        var avatars = Resources.LoadAll<Sprite>("Images/PlayerAvatars");
        foreach(var avatar in avatars)
        {
            GameObject avatarImage = Instantiate(avatarSelectionImage);
            var avImage = avatarImage.transform.Find("AvatarImage");
            if (avImage != null)
            {
                avImage.transform.GetComponent<Image>().sprite = avatar;
            }
            avatarImage.transform.SetParent(this.transform);
        }
    }
}
