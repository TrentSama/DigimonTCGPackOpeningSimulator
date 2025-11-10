using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectionFile", menuName = "CollectionVariable")]
public class CardCollection : ScriptableObject
{
    public CardVariable[] ListOfCardsInCollection;
    public string CollectionName;
    public Sprite CollectionCover;
}
