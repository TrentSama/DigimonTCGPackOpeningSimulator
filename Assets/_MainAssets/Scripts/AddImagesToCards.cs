using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddImagesToCards : MonoBehaviour
{
    public Sprite[] AllSetSprites;
    public CardSet cardSet;
    public void SetupImages()
    {
        for (int i = 0; i < AllSetSprites.Length; i++)
        {
            cardSet.ListOfCardsInSet[i].cardImage = AllSetSprites[i];
            SaveScriptableObject(i);
        }

    }
    public void SaveScriptableObject(int index)
    {
        //EditorUtility.SetDirty(cardSet.ListOfCardsInSet[index]);
    }
}
