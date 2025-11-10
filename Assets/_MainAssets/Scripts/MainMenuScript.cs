using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class MainMenuScript : MonoBehaviour
{
    public GlobalSetPullingFrom setData;
    public TMP_InputField amountOfPacksInput;
    public TMP_InputField amountOfThemePacksInput;
    public TMP_InputField amountOfPromoPacksInput;
    bool openingFullPacks = true;
    public TextMeshProUGUI typeOfPacksOpeningText;

    public GameObject currentMenu;
    public GameObject nextMenu;

    public CardSet StarterDeckToAdd;


    public CardInDeckBuilder[] allDeckBuilderCards;

    private void Awake()
    {
        setData.openingJPPacks = false;
        openingFullPacks = true;
        typeOfPacksOpeningText.text = "Opening full packs (12 Cards)";
    }

    public void SetupOpeningBoxes()
    {
        setData.openingFullBox = true;
        setData.openingSecretPack = false;
    }
    public void SetupOpeningPacks()
    {
        if (openingFullPacks)
        {
            SetupOpeningRegularPacks();
        }
        else
        {
            SetupOpeningRegularJPPacks();
        }
    }
    public void SetupOpeningEnglishBox()
    {
        setData.openingFullBox = true;
        setData.openingSecretPack = false;
    }
    public void SetupOpeningJPBox()
    {
        setData.openingFullBox = true;
        setData.openingSecretPack = false;
    }
    public void SetupOpeningRegularPacks()
    {
        setData.openingFullBox = false;
        setData.openingJPPacks = false;
        setData.openingSecretPack = false;
        if(amountOfPacksInput.text == "")
        {
            setData.amountOfPacksPulled = 6;
        }
        else
        {
            setData.amountOfPacksPulled = int.Parse(amountOfPacksInput.text);
        }
        if (setData.amountOfPacksPulled > 24)
            setData.amountOfPacksPulled = 24;
    }
    public void SetupOpeningRegularJPPacks()
    {
        setData.openingFullBox = false;
        setData.openingJPPacks = true;
        setData.openingCompleteSecretPack = false;
        setData.openingSecretPack = false;
        if (amountOfPacksInput.text == "")
        {
            setData.amountOfPacksPulled = 6;
        }
        else
        {
            setData.amountOfPacksPulled = int.Parse(amountOfPacksInput.text);
        }
        if (setData.amountOfPacksPulled > 24)
            setData.amountOfPacksPulled = 24;
    }
    public void SetupSecretPacks()
    {
        setData.openingFullBox = false;
        setData.openingJPPacks = false;
        if (setData.openingCompleteSecretPack)
        {
            setData.openingSecretPack = false;
            setData.openingCompleteSecretPack = false;
        }
        else
            setData.openingSecretPack = true;

        setData.amountOfPacksPulled = 10;

        if (setData.amountOfPacksPulled > 24)
            setData.amountOfPacksPulled = 24;
    }
    public void SetupSecretPacksVariable()
    {
        setData.openingFullBox = false;
        setData.openingJPPacks = false;
        if (setData.openingCompleteSecretPack)
        {
            setData.openingSecretPack = false;
            setData.openingCompleteSecretPack = false;
        }
        else
            setData.openingSecretPack = true;

        if (amountOfThemePacksInput.text == "")
        {
            setData.amountOfPacksPulled = 5;
        }
        else
        {
            setData.amountOfPacksPulled = int.Parse(amountOfThemePacksInput.text);
        }
        if (setData.amountOfPacksPulled > 24)
            setData.amountOfPacksPulled = 24;
    }
    public void SetupPromoPacks()
    {
        setData.openingFullBox = false;
        setData.openingJPPacks = false;
        setData.openingSecretPack = false;
        setData.openingCompleteSecretPack = false;
        if (amountOfPromoPacksInput.text == "")
        {
            setData.amountOfPacksPulled = 1;
        }
        else
        {
            setData.amountOfPacksPulled = int.Parse(amountOfPromoPacksInput.text);
        }
        if (setData.amountOfPacksPulled > 24)
            setData.amountOfPacksPulled = 24;
    }

    public void TogglePackTypeToOpen()
    {
        if (openingFullPacks)
        {
            setData.openingJPPacks = true;
            openingFullPacks = false;
            typeOfPacksOpeningText.text = "Opening half packs (6 Cards)";
            //SetupOpeningRegularJPPacks();
        }
        else
        {
            setData.openingJPPacks = false;
            openingFullPacks = true;
            typeOfPacksOpeningText.text = "Opening full packs (12 Cards)";
        }
    }

    public void SetPackToOpenFrom(CardSet currentSet)
    {
        setData.SetPullingFrom = currentSet;
    }
    public void WhatTypeOfSet(int setType)
    {
        if(setType == 0)
        {
            if(openingFullPacks)
            {
                setData.amountOfSRsInBox = 7;
                setData.amountOfSecsInBox = 2;
                setData.openingCompleteSecretPack = false;
            }
            else
            {
                setData.amountOfSRsInBox = 4;
                setData.amountOfSecsInBox = 1;
                setData.openingCompleteSecretPack = false;
            }
        }
        else if(setType == 1)
        {
            if (openingFullPacks)
            {
                setData.amountOfSRsInBox = 7;
                setData.amountOfSecsInBox = 3;
                setData.openingCompleteSecretPack = false;
            }
            else
            {
                setData.amountOfSRsInBox = 4;
                setData.amountOfSecsInBox = 2;
                setData.openingCompleteSecretPack = false;
            }
        }
        else if (setType == 2)
        {
            if (openingFullPacks)
            {
                setData.amountOfSRsInBox = 7;
                setData.amountOfSecsInBox = 1;
                setData.openingCompleteSecretPack = false;
            }
            else
            {
                setData.amountOfSRsInBox = 4;
                setData.amountOfSecsInBox = 1;
                setData.openingCompleteSecretPack = false;
            }
        }
        else if (setType == 3)
        {
            if (openingFullPacks)
            {
                setData.amountOfSRsInBox = 7;
                setData.amountOfSecsInBox = 0;
                setData.openingCompleteSecretPack = false;
            }
            else
            {
                setData.amountOfSRsInBox = 4;
                setData.amountOfSecsInBox = 0;
                setData.openingCompleteSecretPack = false;
            }
        }
        else if(setType == 4)
        {
            setData.amountOfSRsInBox = 4;
            setData.amountOfSecsInBox = 0;
            setData.openingCompleteSecretPack = true;
        }
        else if (setType == 4)
        {
            setData.amountOfSRsInBox = 18;
            setData.amountOfSecsInBox = 2;
            setData.openingCompleteSecretPack = true;
        }
        else
        {
            setData.amountOfSRsInBox = 4;
            setData.amountOfSecsInBox = 1;
        }
    }

    public void WhatDeckToEdit(DeckFile deck)
    {
        setData.currentDeckEditing = deck;
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public void SetAmountOfCardsInPack(int cardsInPack)
    {
        setData.amountOfCardsInPromoPack = cardsInPack;
    }

    public void GoToNextMenu(GameObject menuToGoTo)
    {
        nextMenu = menuToGoTo;
        currentMenu.GetComponent<Animator>().Play("MenuFadeOut");
        
        StartCoroutine(NextMenuSequence());
    }
    public void GoToNextMenuLoadingScreen(string sceneToLoad)
    {
        currentMenu.GetComponent<Animator>().Play("MenuFadeOut");
        StartCoroutine(NextMenuSequenceLoadScreen(sceneToLoad));
    }
    public IEnumerator NextMenuSequence()
    {
        yield return new WaitForSeconds(0.2f);
        currentMenu.SetActive(false);
        nextMenu.SetActive(true);
        currentMenu.GetComponent<Animator>().Play("MenuFadeIn");
        currentMenu = nextMenu;
    }
    public IEnumerator NextMenuSequenceLoadScreen(string sceneToLoad)
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(sceneToLoad);
    }
    public void SwapRarities()
    {
        for (int i = 0; i < allDeckBuilderCards.Length; i++)
        {
            allDeckBuilderCards[i].ToggleSecretPackRarity();
        }
    }
    public void DisableCurrentMenu()
    {
        currentMenu.SetActive(false);
    }
    public void SetCurrentMenu(GameObject menu)
    {
        currentMenu = menu;
    }


    public void AddCurrentStarterDeckToCollection()
    {
        for (int i = 0; i < StarterDeckToAdd.ListOfCardsInSet.Count; i++)
        {
            StarterDeckToAdd.ListOfCardsInSet[i].amountOwned += 1;
            if (StarterDeckToAdd.ListOfCardsInSet[i].amountOwned > 9)
                StarterDeckToAdd.ListOfCardsInSet[i].amountOwned = 9;
#if UNITY_EDITOR
            EditorUtility.SetDirty(StarterDeckToAdd.ListOfCardsInSet[i]);
#endif
        }
    }


    public void QuitProgram()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
