using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SetPullingFrom", menuName = "SetPullingFrom")]
public class GlobalSetPullingFrom : ScriptableObject
{
    public bool openingSecretPack = false;
    public bool openingJPPacks = false;
    public bool openingFullBox = false;
    public bool openingCompleteSecretPack = false;

    public CardSet SetPullingFrom;
    public int amountOfPacksPulled = 10;

    public int amountOfSRsInBox = 7;
    public int amountOfSecsInBox = 2;
    public int amountOfCardsInPromoPack = 2;

    public DeckFile currentDeckEditing;

}
