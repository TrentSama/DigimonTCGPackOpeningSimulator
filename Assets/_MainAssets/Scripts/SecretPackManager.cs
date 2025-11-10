using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEditor;
using NaughtyAttributes;

public class SecretPackManager : MonoBehaviour 
{

    public CardSet MasterPack;

    public CardWhenMakingSecretPack[] cardsToDisplay;

    public List<CardVariable> CardsAddingToSecretPack;

    public CardWhenMakingSecretPack[] Keycards;

    public CardSet SecretPackBeingMade;

    public CardVariable currentCardDisplayed;

    public TMP_InputField secretPackInput;

    public bool createBigSecretPack = false;
    public bool createMasterPackRed = false;
    public bool createMasterPackBlue = false;
    public bool createMasterPackYellow = false;
    public bool createMasterPackGreen = false;
    public bool createMasterPackBlack = false;
    public bool createMasterPackPurple = false;
    public bool createMasterPackWhite = false;

    public string[] allCards;
    private void Awake()
    {
        if (createMasterPackRed)
            CreateMasterPackRed();
        else if (createMasterPackBlue)
            CreateMasterPackBlue();
        else if (createMasterPackYellow)
            CreateMasterPackYellow();
        else if (createMasterPackGreen)
            CreateMasterPackGreen();
        else if (createMasterPackBlack)
            CreateMasterPackBlack();
        else if (createMasterPackPurple)
            CreateMasterPackPurple();
        else if (createMasterPackWhite)
            CreateMasterPackWhite();
        //SetupSecretPackKeyCards();
    }

    public void CreateMasterPackRed()
    {
        for (int i = 0; i < MasterPack.ListOfCardsInSet.Count; i++)
        {
            if (MasterPack.ListOfCardsInSet[i].color1 == CardVariable.CardColor.Red ||
                MasterPack.ListOfCardsInSet[i].color2 == CardVariable.SecondCardColor.Red)
            {
                SecretPackBeingMade.ListOfCardsInSet.Add(MasterPack.ListOfCardsInSet[i]);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  
    }
    public void CreateMasterPackBlue()
    {
        for (int i = 0; i < MasterPack.ListOfCardsInSet.Count; i++)
        {
            if (MasterPack.ListOfCardsInSet[i].color1 == CardVariable.CardColor.Blue ||
                MasterPack.ListOfCardsInSet[i].color2 == CardVariable.SecondCardColor.Blue)
            {
                SecretPackBeingMade.ListOfCardsInSet.Add(MasterPack.ListOfCardsInSet[i]);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  
    }
    public void CreateMasterPackYellow()
    {
        for (int i = 0; i < MasterPack.ListOfCardsInSet.Count; i++)
        {
            if (MasterPack.ListOfCardsInSet[i].color1 == CardVariable.CardColor.Yellow ||
                MasterPack.ListOfCardsInSet[i].color2 == CardVariable.SecondCardColor.Yellow)
            {
                SecretPackBeingMade.ListOfCardsInSet.Add(MasterPack.ListOfCardsInSet[i]);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  
    }
    public void CreateMasterPackGreen()
    {
        for (int i = 0; i < MasterPack.ListOfCardsInSet.Count; i++)
        {
            if (MasterPack.ListOfCardsInSet[i].color1 == CardVariable.CardColor.Green ||
                MasterPack.ListOfCardsInSet[i].color2 == CardVariable.SecondCardColor.Green)
            {
                SecretPackBeingMade.ListOfCardsInSet.Add(MasterPack.ListOfCardsInSet[i]);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  
    }
    public void CreateMasterPackBlack()
    {
        for (int i = 0; i < MasterPack.ListOfCardsInSet.Count; i++)
        {
            if (MasterPack.ListOfCardsInSet[i].color1 == CardVariable.CardColor.Black ||
                MasterPack.ListOfCardsInSet[i].color2 == CardVariable.SecondCardColor.Black)
            {
                SecretPackBeingMade.ListOfCardsInSet.Add(MasterPack.ListOfCardsInSet[i]);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  
    }
    public void CreateMasterPackPurple()
    {
        for (int i = 0; i < MasterPack.ListOfCardsInSet.Count; i++)
        {
            if (MasterPack.ListOfCardsInSet[i].color1 == CardVariable.CardColor.Purple ||
                MasterPack.ListOfCardsInSet[i].color2 == CardVariable.SecondCardColor.Purple)
            {
                SecretPackBeingMade.ListOfCardsInSet.Add(MasterPack.ListOfCardsInSet[i]);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  
    }
    public void CreateMasterPackWhite()
    {
        for (int i = 0; i < MasterPack.ListOfCardsInSet.Count; i++)
        {
            if (MasterPack.ListOfCardsInSet[i].color1 == CardVariable.CardColor.White ||
                MasterPack.ListOfCardsInSet[i].color2 == CardVariable.SecondCardColor.White)
            {
                SecretPackBeingMade.ListOfCardsInSet.Add(MasterPack.ListOfCardsInSet[i]);
            }
        }
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  
    }
    public void SetupSecretPackCards()
    {
        for (int i = 0; i < cardsToDisplay.Length; i++)
        {
            cardsToDisplay[i].ResetCard();
        }
        for (int i = 0; i < CardsAddingToSecretPack.Count; i++)
        {
            cardsToDisplay[i].currentCard = CardsAddingToSecretPack[i];
            cardsToDisplay[i].SetCardData();
        }
    }

    public void AddNameMetaData()
    {
        string inputFromFile;
        
        inputFromFile = secretPackInput.text;
        inputFromFile = inputFromFile.Replace("\n", string.Empty);
        //inputFromFile = inputFromFile.Replace("]", string.Empty);
        //inputFromFile = inputFromFile.Replace("\"", string.Empty);
        allCards = inputFromFile.Split(" 1");
        
        //allCards = inputFromFile.SplitEveryN(37);


        for (int i = 0; i < allCards.Length - 1; i++)
        {
            allCards[i] = allCards[i].Remove(0, 7);
            //allCards[i] = allCards[i].Remove(allCards[i].Length - 2);
            //            SecretPackBeingMade.ListOfCardsInSet[i].CardName = allCards[i];
            

        }
        /*for (int k = 0; k < MasterPack.ListOfCardsInSet.Count; k++)
        {
            if (allCards[i] == MasterPack.ListOfCardsInSet[k].name)
            {
                CardsAddingToSecretPack.Add(MasterPack.ListOfCardsInSet[k]);
            }
        }*/
        //}
    }

    [Button]
    public void SetNames()
    {
        for (int i = 0; i < SecretPackBeingMade.ListOfCardsInSet.Count; i++)
        {
            SecretPackBeingMade.ListOfCardsInSet[i].CardName = allCards[i];
 #if UNITY_EDITOR
            EditorUtility.SetDirty(SecretPackBeingMade.ListOfCardsInSet[i]);
 #endif  

        }
    }


    public void SortSecretPack()
    {
        List<CardVariable> SortedSecretPack = CardsAddingToSecretPack.OrderBy(card => card.cardCatagory).ToList();
        //cardsInCollection = SortedList;
        CardsAddingToSecretPack = CardsAddingToSecretPack.OrderBy(card => card.color1).ToList();
        CardsAddingToSecretPack = CardsAddingToSecretPack.OrderByDescending(card => card.secretPackRarity).ToList();
        
        SetupSecretPackCards();
    }
   
    public void SortCardsInDeck()
    {
        CardsAddingToSecretPack = CardsAddingToSecretPack.OrderByDescending(card => card.name).ToList();
        CardsAddingToSecretPack = CardsAddingToSecretPack.OrderByDescending(card => card.cardCatagory).ToList();
        CardsAddingToSecretPack = CardsAddingToSecretPack.OrderBy(card => card.color1).ToList();
        CardsAddingToSecretPack = CardsAddingToSecretPack.OrderBy(card => card.cardLevel).ToList();
        CardsAddingToSecretPack = CardsAddingToSecretPack.OrderBy(card => card.cardCatagory).ToList();
        SetupSecretPackCards();
    }


    public void CraftCard()
    {
        currentCardDisplayed.amountOwned += 1;
        if (currentCardDisplayed.amountOwned > 99)
            currentCardDisplayed.amountOwned = 99;
    }
    public void DeCraftCard()
    {
        currentCardDisplayed.amountOwned -= 1;
        if (currentCardDisplayed.amountOwned < 0)
            currentCardDisplayed.amountOwned = 0;
    }

    public void SaveSecretPack()
    {


        SecretPackBeingMade.ListOfCardsInSet.Clear();
        SecretPackBeingMade.ListOfCardsInSet = CardsAddingToSecretPack;
#if UNITY_EDITOR

        /*for (int i = 0; i < cardsToDisplay.Length; i++)
        {
            if(cardsToDisplay[i].currentCard != null)
            {
                if(cardsToDisplay[i].currentCard.secretPackRarity == CardVariable.SecretPackCardRarity.SuperRare
                    || cardsToDisplay[i].currentCard.secretPackRarity == CardVariable.SecretPackCardRarity.UltraRare)
                {
                    cardsToDisplay[i].currentCard.secretPackToUnlock = SecretPackBeingMade;
                    EditorUtility.SetDirty(cardsToDisplay[i].currentCard);
                }
            }
        }*/

        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  

    }
    public void SaveBigSecretPack()
    {

        SecretPackBeingMade.ListOfCardsInSet.Clear();
        SecretPackBeingMade.ListOfCardsInSet = CardsAddingToSecretPack;
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  

    }
    public void LoadSecretPackForEditing()
    {
        for (int i = 0; i < SecretPackBeingMade.ListOfCardsInSet.Count; i++)
        {
            cardsToDisplay[i].currentCard = SecretPackBeingMade.ListOfCardsInSet[i];
            cardsToDisplay[i].SetCardData();
        }
        CardsAddingToSecretPack = SecretPackBeingMade.ListOfCardsInSet;
        SetupSecretPackKeyCards();
    }
    public void LoadSecretPack()
    {
        string inputFromFile;
        string[] allCards;
        inputFromFile = secretPackInput.text;
        inputFromFile = inputFromFile.Replace("[", string.Empty);
        inputFromFile = inputFromFile.Replace("]", string.Empty);
        inputFromFile = inputFromFile.Replace("\"", string.Empty);
        allCards = inputFromFile.Split(',');
        for (int i = 1; i < allCards.Length; i++)
        {
            //Debug.Log(allCards[i]);
            for (int k = 0; k < MasterPack.ListOfCardsInSet.Count; k++)
            {
                if (allCards[i] == MasterPack.ListOfCardsInSet[k].name)
                {
                    CardsAddingToSecretPack.Add(MasterPack.ListOfCardsInSet[k]);
                }
            }
        }
        //CardsAddingToSecretPack = CardsAddingToSecretPack.Distinct().ToList();
        if (!createBigSecretPack)
            SortSecretPack();
        //SetupSecretPackCards();
    }
    public void SetSecretPackRarity(int rarity)
    {
        if(currentCardDisplayed != null)
        {
            switch (rarity)
            {
                case 0:
                    currentCardDisplayed.secretPackRarity = CardVariable.SecretPackCardRarity.Common;
                    break;
                case 1:
                    currentCardDisplayed.secretPackRarity = CardVariable.SecretPackCardRarity.Rare;
                    break;
                case 2:
                    currentCardDisplayed.secretPackRarity = CardVariable.SecretPackCardRarity.SuperRare;
                    break;
                case 3:
                    currentCardDisplayed.secretPackRarity = CardVariable.SecretPackCardRarity.UltraRare;
                    break;
                default:
                    break;
            }
            //SetupSecretPackCards();
            //SortCardsInDeck();
#if UNITY_EDITOR
            EditorUtility.SetDirty(currentCardDisplayed);
#endif  
            SortSecretPack();
        }
        
    }

    public void SetupSecretPackKeyCards()
    {
        for (int i = 0; i < SecretPackBeingMade.KeyCards.Count; i++)
        {
            Keycards[i].currentCard = SecretPackBeingMade.KeyCards[i];
            Keycards[i].SetCardData();
        }
    }
    public void ResetSecretPackKeyCards()
    {
        SecretPackBeingMade.KeyCards.Clear();
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif 
    }
    public void SetSecretPackKeyCard(int position)
    {
        SecretPackBeingMade.KeyCards[position] = currentCardDisplayed;
        SetupSecretPackKeyCards();
#if UNITY_EDITOR
        EditorUtility.SetDirty(currentCardDisplayed);
#endif 
    }
    public void AddSecretPackKeyCard()
    {
        SecretPackBeingMade.KeyCards.Add(currentCardDisplayed);
        SetupSecretPackKeyCards();
#if UNITY_EDITOR
        EditorUtility.SetDirty(currentCardDisplayed);
#endif 
    }
    public static string[] Chop(string value, int length)
    {
        int strLength = value.Length;
        int strCount = (strLength + length - 1) / length;
        string[] result = new string[strCount];
        for (int i = 0; i < strCount; ++i)
        {
            if(i == 0)
                result[i] = value.Substring(0, length);
            else
                result[i] = value.Substring(i * length, Mathf.Min(length, strLength));
            strLength -= length;
        }

        return result;
    }   
}
public static class StringExtensions
{
    public static List<string> SplitEveryN(this string str, int n = 1024)
    {
        List<string> ret = new List<string>();

        int chunkIterator = 0;
        while (chunkIterator < str.Length)
        {
            int currentChunkSize = Mathf.Min(n, str.Length - chunkIterator);
            ret.Add(str.Substring(chunkIterator, currentChunkSize));
            // Debug.Log(str.Substring(chunkIterator, currentChunkSize));
            chunkIterator += currentChunkSize;
        }
        return ret;
    }
}

