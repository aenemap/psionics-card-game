using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardsManager : MonoBehaviour, IPointerDownHandler
{
    public GameObject pivot;
    private List<GameObject> cardsInDiscards = new List<GameObject>();

    private void OnEnable()
    {
        VisualsEvents.current.onAddCardToDiscards += AddCardToDiscards;
    }


    private void AddCardToDiscards(GameObject card)
    {
        card.transform.position = pivot.transform.position;
        card.transform.SetParent(pivot.transform);
        cardsInDiscards.Add(card);
        Card cardAsset = card.GetCardAsset();
        cardAsset.LocationOfCard = Enums.CardLocation.Discards;
        HandAreaEvents.current.RemoveCardFromHand(cardAsset.CardId, true);
    }

    private void OnDisable()
    {
        VisualsEvents.current.onAddCardToDiscards -= AddCardToDiscards;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Discards");
    }
}
