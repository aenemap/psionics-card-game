using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.EventSystems;

public class HoverPreview: MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
    // PUBLIC FIELDS
    public GameObject TurnThisOffWhenPreviewing;  // if this is null, will not turn off anything 
    public GameObject TurnArtPreviewOffWhenPreviewing;
    public Vector3 TargetPosition;
    public float TargetScale;
    public GameObject previewGameObject;
    public bool ActivateInAwake = false;
    private static int objIndex;    

    Ray ray;
    RaycastHit hit;

    // PRIVATE FIELDS
    private static HoverPreview currentlyViewing = null;

    private BoxCollider col;

    // PROPERTIES WITH UNDERLYING PRIVATE FIELDS
    private static bool _PreviewsAllowed = true;
    public static bool PreviewsAllowed
    {
        get { return _PreviewsAllowed;}

        set 
        { 
            //Debug.Log("Hover Previews Allowed is now: " + value);
            _PreviewsAllowed= value;
            if (!_PreviewsAllowed)
                StopAllPreviews();
        }
    }

    private bool _thisPreviewEnabled = false;
    public bool ThisPreviewEnabled
    {
        get { return _thisPreviewEnabled;}

        set 
        { 
            _thisPreviewEnabled = value;
            if (!_thisPreviewEnabled)
                StopThisPreview();
        }
    }

    public bool OverCollider { get; set;}
 
    // MONOBEHVIOUR METHODS
    void Awake()
    {
        ThisPreviewEnabled = ActivateInAwake;
    }

    void Start()
    {
        col = transform.GetComponent<BoxCollider>();
    }

    void OnMouseEnter()
    {
        OverCollider = true;

        if (PreviewsAllowed && ThisPreviewEnabled && this.gameObject.GetCardAsset().LocationOfCard != Enums.CardLocation.Discards)
            PreviewThisObject();
    }

    void OnMouseExit()
    {
        OverCollider = false;

        if (!PreviewingSomeCard())
            StopAllPreviews();
    }

    // OTHER METHODS
    void PreviewThisObject()
    {
        // 1) clone this card 
        // first disable the previous preview if there is one already
        StopAllPreviews();
        // 2) save this HoverPreview as curent
        currentlyViewing = this;
        // 3) enable Preview game object
        previewGameObject.SetActive(true);
        // 4) disable if we have what to disable
        if (TurnThisOffWhenPreviewing != null)
        {
            TurnThisOffWhenPreviewing.SetActive(false);
        }
        if (TurnArtPreviewOffWhenPreviewing != null)
        {
            TurnArtPreviewOffWhenPreviewing.SetActive(false);
        }
        // 5) tween to target position
        previewGameObject.transform.localPosition = Vector3.zero;
        previewGameObject.transform.localScale = Vector3.one;
        GameObject parent = previewGameObject.transform.parent.gameObject;
        objIndex = parent.transform.GetSiblingIndex();
        parent.transform.SetAsLastSibling();
        previewGameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        previewGameObject.transform.DOMove(new Vector3(transform.position.x, transform.position.y + 150f, 0), 1f).SetEase(Ease.OutQuint);        
        previewGameObject.transform.DOScale(TargetScale, 1f).SetEase(Ease.OutQuint);
    }

    void StopThisPreview()
    {
        previewGameObject.SetActive(false);
        previewGameObject.transform.localScale = Vector3.one;
        previewGameObject.transform.localPosition = Vector3.zero;
        GameObject parent = previewGameObject.transform.parent.gameObject;
        parent.transform.SetSiblingIndex(objIndex);

        if (TurnThisOffWhenPreviewing != null)
        {
            CardDisplay cardDisplay = this.transform.GetComponent<CardDisplay>();
            if (cardDisplay != null)
            {
                if (cardDisplay.card.LocationOfCard == Enums.CardLocation.ShieldsArea || cardDisplay.card.LocationOfCard == Enums.CardLocation.TalentArea)
                {
                    TurnThisOffWhenPreviewing.SetActive(false);
                    if (TurnArtPreviewOffWhenPreviewing != null)
                        TurnArtPreviewOffWhenPreviewing.SetActive(true);
                }
                else
                {
                    TurnThisOffWhenPreviewing.SetActive(true);
                    if (TurnArtPreviewOffWhenPreviewing != null)
                        TurnArtPreviewOffWhenPreviewing.SetActive(false);
                }
            }
        }
    }

    // STATIC METHODS
    private static void StopAllPreviews()
    {
        if (currentlyViewing != null)
        {
            currentlyViewing.previewGameObject.SetActive(false);
            currentlyViewing.previewGameObject.transform.localScale = Vector3.one;
            currentlyViewing.previewGameObject.transform.localPosition = Vector3.zero;
            GameObject parent = currentlyViewing.previewGameObject.transform.parent.gameObject;
            parent.transform.SetSiblingIndex(objIndex);
            if (currentlyViewing.TurnThisOffWhenPreviewing != null)
            {
                CardDisplay cardDisplay = currentlyViewing.transform.GetComponent<CardDisplay>();
                if (cardDisplay != null)
                {
                    if (cardDisplay.card.LocationOfCard == Enums.CardLocation.ShieldsArea || cardDisplay.card.LocationOfCard == Enums.CardLocation.TalentArea)
                    {
                        currentlyViewing.TurnThisOffWhenPreviewing.SetActive(false);
                        if (currentlyViewing.TurnArtPreviewOffWhenPreviewing != null)
                            currentlyViewing.TurnArtPreviewOffWhenPreviewing.SetActive(true);
                    }
                    else
                    {
                        currentlyViewing.TurnThisOffWhenPreviewing.SetActive(true);
                        if (currentlyViewing.TurnArtPreviewOffWhenPreviewing != null)
                            currentlyViewing.TurnArtPreviewOffWhenPreviewing.SetActive(false);
                    }
                }
            }
        }
         
    }

    private static bool PreviewingSomeCard()
    {
        if (!PreviewsAllowed)
            return false;

        HoverPreview[] allHoverBlowups = GameObject.FindObjectsOfType<HoverPreview>();

        foreach (HoverPreview hb in allHoverBlowups)
        {
            if (hb.OverCollider && hb.ThisPreviewEnabled)
                return true;
        }

        return false;
    }
}
