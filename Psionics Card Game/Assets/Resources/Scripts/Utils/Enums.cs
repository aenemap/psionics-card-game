using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public static class Enums 
{
    public enum CardType
    {
        Shield,
        Event,
        Talent,
        Test
    }

    public enum CardSubType
    {
        Basic,
        Absorbing,
        Attack
    }

    public enum DrawCardFrom
    {
        TopOfDeck,
        BottomOfDeck
    }

    public enum CardState
    {
        FaceUp,
        FaceDown
    }

    public enum CardLocation
    {
        Deck,
        ShieldsArea,
        TalentArea,
        HandArea,
        Discards
    }

    public enum PlayerPrefKeys
    {
        PlayerName,
        PlayerAvatar
    }

}
