using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;

public class MenuStarterDeckButton : MonoBehaviour
{

    private MainMenuScript mainMenu;
    public bool GetMainMenuOnAwake = false;

    public TextMeshProUGUI buttonStarterDeckName;
    public Image buttonStarterDeckImage;
    public CardSet starterDeckFile;
    private void Awake()
    {
        if (starterDeckFile.SetCover != null)
            buttonStarterDeckImage.sprite = starterDeckFile.SetCover.cardImage;
        buttonStarterDeckName.text = starterDeckFile.SetName;
        if (GetMainMenuOnAwake)
            mainMenu = FindObjectOfType<MainMenuScript>();
    }

    private void OnEnable()
    {
        if (starterDeckFile.SetCover != null)
            buttonStarterDeckImage.sprite = starterDeckFile.SetCover.cardImage;
    }

    public void AddStarterDeckToSelection()
    {
        for (int i = 0; i < starterDeckFile.ListOfCardsInSet.Count; i++)
        {
            starterDeckFile.ListOfCardsInSet[i].amountOwned += 1;
            if (starterDeckFile.ListOfCardsInSet[i].amountOwned > 9)
                starterDeckFile.ListOfCardsInSet[i].amountOwned = 9;
#if UNITY_EDITOR
            EditorUtility.SetDirty(starterDeckFile.ListOfCardsInSet[i]);
#endif
        }
    }

    public void SetMainMenuStarterDeck()
    {
        mainMenu.StarterDeckToAdd = starterDeckFile;
    }

    public void SetCorrectPackInMenu()
    {
        mainMenu.SetPackToOpenFrom(starterDeckFile);
    }


}
