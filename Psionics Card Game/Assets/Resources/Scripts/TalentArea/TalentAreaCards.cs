using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TalentAreaCards : MonoBehaviour
{
    public List<GameObject> talentCards = new List<GameObject>();
    public Transform pivot;

    [Header("Placement Info")]
    public float cardWidth;
    public float spacing;
    public float height;

    private void OnEnable()
    {
        TalentAreaEvents.current.onAddCardToTalents += OnAddCardToTalents;
        TalentAreaEvents.current.onRemoveCardFromTalents += OnRemoveCardFromTalents;
    }

    void Start()
    {

    }

    private void OnRemoveCardFromTalents(int cardId)
    {
        //TODO: Check if the card needs to be destroyed.
        talentCards = talentCards.Where(w =>
        {
            CardDisplay cardDisplay = w.transform.GetComponent<CardDisplay>();
            return cardDisplay.CardId.text != cardId.ToString();
        }).ToList();
    }

    private void OnAddCardToTalents(GameObject talentCard)
    {

        talentCards.Add(talentCard);

        var talentWidth = Utilities.CalcAreaWidth(talentCards.Count, cardWidth, spacing);
        var offSetX = pivot.position.x - talentWidth / 2;

        for (int i = 0; i < talentCards.Count; i++)
        {
            var card = talentCards[i];

            CardDisplay cardDisplay = card.transform.GetComponent<CardDisplay>();
            CardRotation cardRotation = card.transform.GetComponent<CardRotation>();
            if (cardRotation.cardState == Enums.CardState.FaceUp)
            {
                cardRotation.StartFaceDown();
            }
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
        ShieldAreaEvents.current.onAddCardToShields -= OnAddCardToTalents;
        ShieldAreaEvents.current.onRemoveCardFromShields -= OnRemoveCardFromTalents;
    }
}
