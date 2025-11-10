using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class RenameAllStarterDeckCards : MonoBehaviour
{
    public CardSet starterCards;
    public string currentCardName;
    private void Awake()
    {
        for (int i = 0; i < starterCards.ListOfCardsInSet.Count; i++)
        {
            currentCardName = starterCards.ListOfCardsInSet[i].name;
            currentCardName = currentCardName.Remove(currentCardName.Length - 3, 1);
            //Debug.Log(currentCardName);
            starterCards.ListOfCardsInSet[i].name = currentCardName;
#if UNITY_EDITOR
            EditorUtility.SetDirty(starterCards.ListOfCardsInSet[i]);
#endif
        }
    }
}
