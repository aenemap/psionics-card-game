using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsEvents : MonoBehaviour
{
    public static VisualsEvents current;

    public void Awake()
    {
        current = this;
    }

    public event Action onSameDistanceCalculate;

    public void SameDistanceCalculate()
    {
        if (onSameDistanceCalculate != null)
        {
            onSameDistanceCalculate();
        }
    }
}
