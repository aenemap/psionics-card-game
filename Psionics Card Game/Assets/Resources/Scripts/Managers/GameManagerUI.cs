using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{

    public static GameManagerUI singleton = null;

    public GameManagerUI()
    {
        singleton = this;
    }

    [SerializeField] private GameObject handPivot;
    [SerializeField] private GameObject opHandPivot;
    [SerializeField] private TextMeshProUGUI debugText;
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void SetUpCard(GameObject card, bool hasAuthority)
    {
        //debugText.text += card.GetCardAsset().CardName + "\n";
        //debugText.text += isLocal.ToString() + "\n";
        //debugText.text += "INSIDE SetUpCard\n";

        if (hasAuthority)
        {
            card.transform.SetParent(handPivot.transform);
            card.transform.position = handPivot.transform.position;
        }
        else
        {
            card.transform.SetParent(opHandPivot.transform);
            card.transform.position = opHandPivot.transform.position;            
        }

        //debugText.text += $"card parent => {card.transform.parent.name} \n";
        //debugText.text += $"card position=> {card.transform.position} \n";
    }

    public GameObject GetHandPivot()
    {
        return handPivot;
    }

    public GameObject GetOpHandPivot()
    {
        return opHandPivot;
    }
}
