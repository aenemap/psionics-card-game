using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    Player player;

    public void SetPlayer(Player player)
    {
        this.player = player;
        text.text = "Player" + player.PlayerIndex.ToString();
    }
}
