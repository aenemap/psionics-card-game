using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities 
{
    public static string MainGameSceneName = "MainGameSceneBKP2";
    public static float CalcAreaWidth(int quantityOfCards, float cardWidth, float spacing )
    {
        var widthCards = quantityOfCards * cardWidth;
        var widthSpacing = (quantityOfCards - 1) * spacing;
        return widthCards + widthSpacing;
    }

    public static float CalcFirstAngle(float fullAngle)
    {
        var magicMathFactor = 0.1f;
        return -(fullAngle / 2) + fullAngle * magicMathFactor;
    }
}
