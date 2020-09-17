using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShieldAreaCards : MonoBehaviour
{
    public List<GameObject> shieldCards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ShieldAreaEvents.current.onAddCardToShields += OnAddCardToShields;
        ShieldAreaEvents.current.onRemoveCardFromShields += OnRemoveCardFromShields;
    }

    private void OnRemoveCardFromShields(int cardId)
    {
        //TODO: Check if the card needs to be destroyed.
        shieldCards = shieldCards.Where(w =>
        {
            CardDisplay cardDisplay = w.transform.GetComponent<CardDisplay>();
            return cardDisplay.CardId.text != cardId.ToString();
        }).ToList();
    }

    private void OnAddCardToShields(GameObject shieldCard)
    {
        shieldCards.Add(shieldCard);
        Debug.Log("Cards in Shields => " + shieldCards.Count.ToString());
        VisualsEvents.current.SameDistanceCalculate(shieldCards);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
