using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShieldAreaCards : MonoBehaviour
{
    public List<GameObject> shieldCards = new List<GameObject>();
    public Transform pivot;

    [Header("Placement Info")]
    public float cardWidth;
    public float spacing;
    public float height;

    private void OnEnable()
    {
        ShieldAreaEvents.current.onAddCardToShields += OnAddCardToShields;
        ShieldAreaEvents.current.onRemoveCardFromShields += OnRemoveCardFromShields;
    }

    void Start()
    {
        
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

        var shieldsWidth = Utilities.CalcAreaWidth(shieldCards.Count, cardWidth, spacing);
        var offSetX = pivot.position.x - shieldsWidth / 2;

        for (int i = 0; i < shieldCards.Count; i++)
        {
            var card = shieldCards[i];

            CardDisplay cardDisplay = card.transform.GetComponent<CardDisplay>();
            CardRotation cardRotation = card.transform.GetComponent<CardRotation>();
            if (cardRotation.cardState == Enums.CardState.FaceUp)
            {
                cardRotation.StartFaceDown();
            }

            //CardRotation2 cardRotation = card.transform.GetComponent<CardRotation2>();
            //cardRotation.StartFlip();

            //CardRotation Script
            //CardRotation cardRotation = card.transform.GetComponent<CardRotation>();
            //if (cardRotation.cardState == Enums.CardState.FaceUp)
            //{
            //    cardRotation.StartFaceDown();
            //}

            ////BetterCardRotation Script
            //if (card.transform.rotation.y != 1)
            //{
            //    card.transform.rotation = Quaternion.Euler(0, 180, 0);
            //    card.transform.DOLocalRotate(new Vector3(0, 180, 0), 0.3f);
            //}
            var xPos = offSetX + cardWidth / 2;
            var yPos = pivot.position.y;
            //var rotation = card.transform.rotation;
            var position = new Vector3(xPos, yPos, card.transform.position.z);
            card.transform.position = position;
            card.transform.parent = this.pivot.transform;
            VisualsEvents.current.UpdateDraggableOriginalPosition(card, xPos, yPos);
            offSetX += cardWidth + spacing;

        }

    }

    private void OnDisable()
    {
        ShieldAreaEvents.current.onAddCardToShields -= OnAddCardToShields;
        ShieldAreaEvents.current.onRemoveCardFromShields -= OnRemoveCardFromShields;
    }
}
