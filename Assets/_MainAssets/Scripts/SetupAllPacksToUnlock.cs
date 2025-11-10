using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetupAllPacksToUnlock : MonoBehaviour
{


    public bool SetupUnlocksOnAwake = false;
    public bool resetAllUnlocks = false;
    public bool ResetAmountOfCardsOwnedValues;
    public List<CardSet> SecretPacks = new();
    public CardSet CompletePack;

    private void Awake()
    {
        if (ResetAmountOfCardsOwnedValues)
        {
            for (int i = 0; i < CompletePack.ListOfCardsInSet.Count; i++)
            {
                CompletePack.ListOfCardsInSet[i].amountOwned = 0;
#if UNITY_EDITOR
                EditorUtility.SetDirty(CompletePack.ListOfCardsInSet[i]);
#endif  
            }
        }
        if (resetAllUnlocks)
        {
            for (int i = 0; i < CompletePack.ListOfCardsInSet.Count; i++)
            {
                CompletePack.ListOfCardsInSet[i].secretPackToUnlock.Clear();
#if UNITY_EDITOR
                EditorUtility.SetDirty(CompletePack.ListOfCardsInSet[i]);
#endif  
            }



        }
        if (SetupUnlocksOnAwake)
        {
            for (int i = 0; i < SecretPacks.Count; i++)
            {
                for (int k = 0; k < SecretPacks[i].ListOfCardsInSet.Count; k++)
                {
                    SecretPacks[i].ListOfCardsInSet[k].secretPackToUnlock.Clear();
#if UNITY_EDITOR
                    EditorUtility.SetDirty(SecretPacks[i].ListOfCardsInSet[k]);
#endif  
                }
            }
            for (int i = 0; i < SecretPacks.Count; i++)
            {
                for (int k = 0; k < SecretPacks[i].ListOfCardsInSet.Count; k++)
                {
                    if(SecretPacks[i].ListOfCardsInSet[k].secretPackRarity == CardVariable.SecretPackCardRarity.SuperRare ||
                        SecretPacks[i].ListOfCardsInSet[k].secretPackRarity == CardVariable.SecretPackCardRarity.UltraRare)
                    {
                        SecretPacks[i].ListOfCardsInSet[k].secretPackToUnlock.Add(SecretPacks[i]);

#if UNITY_EDITOR
                        EditorUtility.SetDirty(SecretPacks[i].ListOfCardsInSet[k]);
#endif  

                    }
                }
            }
        }
    }
}
