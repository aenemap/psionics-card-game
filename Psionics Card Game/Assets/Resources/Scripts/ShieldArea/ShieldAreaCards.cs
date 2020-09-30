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
        shieldCards = shieldCards.Where(w =>
        {
            return w.GetCardAsset().CardId != cardId;
        }).ToList();
    }

    private void OnAddCardToShields(GameObject shieldCard)
    {

        //shieldCards.Insert(0, shieldCard);
        shieldCards.Add(shieldCard);
        var shieldsWidth = Utilities.CalcAreaWidth(shieldCards.Count, cardWidth, spacing);
        var offSetX = pivot.position.x - shieldsWidth / 2;

        for (int i = 0; i < shieldCards.Count; i++)
        {
            var card = shieldCards[i];
            card.SetCardLocation(Enums.CardLocation.ShieldsArea);
            if (card.GetCardAsset().IsFaceDown)
            {
                card.StartRotation(Enums.CardState.FaceDown);
            }
            var xPos = offSetX + cardWidth / 2;
            var yPos = pivot.position.y;
            //var rotation = card.transform.rotation;
            var position = new Vector3(xPos, yPos, card.transform.position.z);
            card.transform.position = position;
            card.transform.SetParent(this.pivot.transform);
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
