using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class Deck : ScriptableObject
{
    public int DeckId;
    public string DeckName;
    public List<Card> DeckList;
}
