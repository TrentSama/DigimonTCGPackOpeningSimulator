using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardFile", menuName ="CardVariable")]
public class CardVariable : ScriptableObject
{
    [System.Serializable]
    public enum CardRarity
    {
        Common,
        Uncommon,
        Rare,
        SuperRare,
        SecretRare,
        Promo,
        LimitedPack,
        AlternateArt
    }
    [System.Serializable]
    public enum SecretPackCardRarity
    {
        Common,
        Rare,
        SuperRare,
        UltraRare
    }
    [System.Serializable]
    public enum CardColor
    {
        Red,
        Blue,
        Yellow,
        Green,
        Black,
        Purple,
        White
    }
    [System.Serializable]
    public enum SecondCardColor
    {
        None,
        Red,
        Blue,
        Yellow,
        Green,
        Black,
        Purple,
        White
    }
    [System.Serializable]
    public enum CardType
    {
        Digimon,
        Digitama,
        Tamer,
        Option
    }

    public Sprite cardImage;
    public int setNumber;
    public string CardName;
    public bool hasAnAltArt = false;
    public CardRarity rarity;
    public SecretPackCardRarity secretPackRarity;
    public CardColor color1;
    public SecondCardColor color2;
    public CardType cardCatagory;
    public int playCost = 0;
    public int DPOfCard = 1000;
    public int cardLevel = 3;
    public List<CardSet> secretPackToUnlock = new();
    public int amountOwned = 0;
    public int amountInCurrentDeck = 0;

}
