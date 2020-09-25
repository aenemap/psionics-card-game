using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalentAreaEvents : MonoBehaviour
{
    public static TalentAreaEvents current;

    public TalentAreaEvents()
    {
        current = this;
    }

    public event Action<GameObject> onAddCardToTalents;
    public event Action<int> onRemoveCardFromTalents;

    public void AddCardToTalents(GameObject talentCard)
    {
        if (onAddCardToTalents != null)
        {
            onAddCardToTalents(talentCard);
        }
    }

    public void RemoveCardFromTalents(int cardId)
    {
        if (onRemoveCardFromTalents != null)
        {
            onRemoveCardFromTalents(cardId);
        }
    }





}
