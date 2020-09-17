using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// place first and last elements in children array manually
// others will be placed automatically with equal distances between first and last elements
public class SameDistanceChildren : MonoBehaviour 
{

    //public List<Transform> Children;

	// Use this for initialization
	//void Awake () 
 //   {
 //       CalculateDistance();
	//}

    private void Start()
    {
        VisualsEvents.current.onSameDistanceCalculate += CalculateDistance;
    }

    private void CalculateDistance(List<GameObject> cards)
    {
        if (cards.Count > 0)
        {
            Vector3 firstElementPos = cards[0].transform.position;
            Vector3 lastElementPos = cards[cards.Count - 1].transform.position;

            // dividing by Children.Length - 1 because for example: between 10 points that are 9 segments
            float XDist = (lastElementPos.x - firstElementPos.x) / (float)(cards.Count - 1);
            float YDist = (lastElementPos.y - firstElementPos.y) / (float)(cards.Count - 1);
            float ZDist = (lastElementPos.z - firstElementPos.z) / (float)(cards.Count - 1);

            Vector3 Dist = new Vector3(XDist, YDist, ZDist);

            for (int i = 1; i < cards.Count; i++)
            {
                cards[i].transform.position = cards[i - 1].transform.position + Dist;
            }
        }
    }
	
	
}
