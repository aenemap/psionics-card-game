using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums 
{
    public enum CardType
    {
        Shield,
        Event,
        Talent
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

}
