using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArtCardPreviewDisplay : MonoBehaviour
{
    public Card card;

    public TextMeshProUGUI EnergyCost;
    public TextMeshProUGUI ConsistencyValue;
    public TextMeshProUGUI DefenceValue;
    public TextMeshProUGUI AbsorbingValue;
    public TextMeshProUGUI TrashCost;
    public Image ArtImage;

    // Start is called before the first frame update
    void Start()
    {
        if (card != null)
        {
            EnergyCost.text = card.EnergyCost.ToString();
            if (card.CardImage != null)
                ArtImage.sprite = card.CardImage;


            if (card.CardType == Enums.CardType.Shield || card.CardType == Enums.CardType.Test)
            {
                if (ConsistencyValue != null)
                    ConsistencyValue.text = card.ConcistencyValue.ToString();
                if (card.CardSubType == Enums.CardSubType.Basic)
                {
                    if (DefenceValue != null)
                        DefenceValue.text = card.DefenceValue.ToString();
                }

                if (card.CardSubType == Enums.CardSubType.Absorbing)
                {
                    if (AbsorbingValue != null)
                        AbsorbingValue.text = card.AbsorbingValue.ToString();
                }
            }

            if (card.CardType == Enums.CardType.Talent)
            {
                if (TrashCost != null)
                    TrashCost.text = card.TrashCost.ToString();
            }
        }




    }
}
