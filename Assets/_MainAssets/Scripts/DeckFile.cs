using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckFile", menuName = "DeckVariable")]
public class DeckFile : ScriptableObject
{
    public string DeckName;
    public CardVariable CurrentDeckPicture;
    public List<CardVariable> CardsInDeck = new List<CardVariable>();
    public List<CardVariable> EggCardsInDeck = new List<CardVariable>();

}
