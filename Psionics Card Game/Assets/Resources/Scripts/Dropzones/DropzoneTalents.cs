﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropzoneTalents : MonoBehaviour, IDropHandler
{
    private void Start()
    {
    }
    public void OnDrop(PointerEventData eventData)
    {
        CardDisplay cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
        cardDisplay.card.LocationOfCard = Enums.CardLocation.TalentArea;
        if (cardDisplay.card.CardType == Enums.CardType.Talent)
        {
            TalentAreaEvents.current.AddCardToTalents(eventData.pointerDrag);
            HandAreaEvents.current.RemoveCardFromHand(cardDisplay.card.CardId, false);
            HandAreaEvents.current.BendHand();
        }

    }
}
