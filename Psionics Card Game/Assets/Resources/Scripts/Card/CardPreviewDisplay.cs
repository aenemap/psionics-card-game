using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPreviewDisplay : MonoBehaviour
{
    public Card card;

    public Text CardName;
    public Text CardType;
    public Text CardSubType;
    public Text EnergyCost;
    public Text ConsistencyValue;
    public Text DefenceValue;
    public Text AbsorbingValue;
    public Text TrashCost;

    public Image Consistency;
    public Image Defence;
    public Image Absorbing;
    public Image Trash;


    // Start is called before the first frame update
    void Start()
    {
        if (card != null)
        {
            CardName.text = card.CardName;
            CardType.text = card.CardType.ToString();
            CardSubType.text = card.CardSubType.ToString();
            EnergyCost.text = card.EnergyCost.ToString();

            GameObject defenceImage = GameObject.Find("Defence");
            defenceImage.SetActive(false);
            GameObject concistencyImage = GameObject.Find("Concistency");
            concistencyImage.SetActive(false);
            GameObject absorbingImage = GameObject.Find("Absorbing");
            absorbingImage.SetActive(false);
            GameObject trashImage = GameObject.Find("Trash");
            trashImage.SetActive(false);

            if (card.CardType == Enums.CardType.Shield)
            {
                if (!concistencyImage.activeSelf)
                    concistencyImage.SetActive(true);
                ConsistencyValue.text = card.ConcistencyValue.ToString();
                if (card.CardSubType == Enums.CardSubType.Basic)
                {
                    if (absorbingImage.activeSelf)
                        absorbingImage.SetActive(false);
                    if (trashImage.activeSelf)
                        trashImage.SetActive(false);
                    if (!defenceImage.activeSelf)
                        defenceImage.SetActive(true);
                    DefenceValue.text = card.DefenceValue.ToString();
                }

                if (card.CardSubType == Enums.CardSubType.Absorbing)
                {
                    if (defenceImage.activeSelf)
                        defenceImage.SetActive(false);
                    if (trashImage.activeSelf)
                        trashImage.SetActive(false);
                    if (!absorbingImage.activeSelf)
                        absorbingImage.SetActive(true);
                    AbsorbingValue.text = card.AbsorbingValue.ToString();
                }
            }

            if (card.CardType == Enums.CardType.Talent)
            {
                if (defenceImage.activeSelf)
                    defenceImage.SetActive(false);

                if (absorbingImage.activeSelf)
                    absorbingImage.SetActive(false);

                if (concistencyImage.activeSelf)
                    concistencyImage.SetActive(false);

                if (!trashImage.activeSelf)
                    trashImage.SetActive(true);

                TrashCost.text = card.TrashCost.ToString();
            }
        }




    }
}
