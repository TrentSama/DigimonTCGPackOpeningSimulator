using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetFile", menuName = "SetVariable")]
public class CardSet : ScriptableObject
{
    public List<CardVariable> ListOfCardsInSet = new();
    public string SetName;
    [TextAreaAttribute]
    public string SetDescription;
    public CardVariable SetCover;
    public bool SecretPack = false;
    public bool PromoPack = false;
    public bool hasBeenUnlocked = false;
    public bool hasBeenCompleted = false;
    public bool isRebootBooster = false;
    public List<CardVariable> KeyCards = new();
}
