using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private Player player = null;

    public void SetPlayer(Player player)
    {
        this.player = player;
        text.text = player.GetDisplayName();
    }
}
