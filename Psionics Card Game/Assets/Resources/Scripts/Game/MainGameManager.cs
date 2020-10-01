using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public GameObject cardManager;

    private float delayBeforeDealCards = 1f;
    private float timeElapsed;
    private bool initialDeal = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().isLoaded)
        //{
        //    timeElapsed += Time.deltaTime;
        //    if (timeElapsed > delayBeforeDealCards && !initialDeal)
        //    {
        //        initialDeal = true;
        //        var playerDeck = GetDeckById(1);
        //        if (playerDeck != null)
        //        {
        //            foreach (Card crd in playerDeck.DeckList)
        //            {
        //                crd.LocationOfCard = Enums.CardLocation.Deck;
        //                cardsInDeck.Add(crd);
        //            }
        //            StartCoroutine(DealInitialCards());
        //        }

        //    }
        //}
    }
}
