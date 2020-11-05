using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class ExtensionMethods 
{
    public static void SetCardLocation(this GameObject gameObject, Enums.CardLocation cardLocation)
    {
        CardDisplay cardDisplay = gameObject.transform.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            cardDisplay.card.LocationOfCard = cardLocation;
        }
    }

    public static Card GetCardAsset(this GameObject gameObject)
    {
        CardDisplay cardDisplay = gameObject.transform.GetComponent<CardDisplay>();
        if (cardDisplay != null)
        {
            return cardDisplay.card;
        }
        return null;
    }

    public static void StartRotation(this GameObject gameObject, Enums.CardState cardState)
    {
        CardRotation cardRotation = gameObject.transform.GetComponent<CardRotation>();
        if(cardRotation != null)
        {
            if (cardRotation.cardState == Enums.CardState.FaceDown && cardState == Enums.CardState.FaceUp && gameObject.GetCardAsset().LocationOfCard == Enums.CardLocation.Deck)
                cardRotation.FromDeckStartFaceUp();
            if (cardRotation.cardState == Enums.CardState.FaceUp && cardState == Enums.CardState.FaceDown)
                cardRotation.StartFaceDown();
            if (cardRotation.cardState == Enums.CardState.FaceDown && cardState == Enums.CardState.FaceUp)
                cardRotation.StartFaceUp();
        }
    }

    public static Transform FindChildInHierarchy(this GameObject gameObject, Transform parent, string childToFind)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childToFind)
            {
                return child;
            }
            else if (child.childCount > 0)
            {
                var result =  FindChildInHierarchy(gameObject, child, childToFind);
                if (result != null)
                    return result;
            }
        }
        return null;
    }

    public static Guid ToGuid(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hashBytes = provider.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}
