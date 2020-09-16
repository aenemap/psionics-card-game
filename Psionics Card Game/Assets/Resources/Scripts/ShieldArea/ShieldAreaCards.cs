using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldAreaCards : MonoBehaviour
{
    public List<GameObject> shieldCards = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ShieldAreaEvents.current.onAddCardToShields += OnAddCardToShields;
    }

    private void OnAddCardToShields(GameObject shieldCard)
    {
        shieldCards.Add(shieldCard);
        Debug.Log("Cards in Shields => " + shieldCards.Count.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
