using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEditor;
using System.Threading.Tasks;


public class SetupCardCollection : MonoBehaviour
{
    public CardCollection collectionToPullFrom;

    public List<CardVariable> cardsInCollection = new List<CardVariable>();

    public CardInCollection[] cardsToDisplay;

    public int startingIndexForGroup = 0;
    public int endingIndexForGroup = 39;
    private int defaultEndingIndex;

    public int totalCardsDisplayed = 0;
    public int whenNotEnoughCardsInt = 0;

    public string collectionString = "";

    public int amountToIncreaseandDecrease = 40;

    public bool showingCardsOwned = true;

    public bool useFilter = false;

    public bool useSetFilter = false;
    public CardSet setToFilter;

    public bool useColorFilter = false;
    public int ColorToFilter;

    public bool useCardTypeFilter = false;
    public CardVariable.CardType typeToDisplay;

    public bool useRarityFilter = false;
    public CardVariable.CardRarity rarityToDisplay;
    public bool useSecretPackRarityFilter = false;
    public CardVariable.SecretPackCardRarity secretPackRarityToDisplay;
    public GameEvent SwapRarity;

    public bool useLevelFilter = false;
    public int levelToDisplay = 3;

    public bool usePlayCostFilter = false;
    public int playcostToDisplay = 0;

    public bool useNameFilter = false;
    public TMP_InputField CardNameInput;
    public string cardNameToFilterBy;

    public bool usingRegularRarity = true;

    public List<CardVariable> FilteredList = new List<CardVariable>();

    bool loadingImages = false;
    void Awake()
    {
        defaultEndingIndex = endingIndexForGroup;
        SetupCollection();
        DisplayCardsOwnedChunk();
    }
    /*
    public void DisplayCardsOwnedChunk()
    {
        if(endingIndexForGroup > cardsInCollection.Count)
        {
            for (int i = 0; i < cardsInCollection.Count -1; i++)
            {
                cardsToDisplay[totalCardsDisplayed].currentCard = cardsInCollection[i];
                cardsToDisplay[totalCardsDisplayed].SetCardData();
                totalCardsDisplayed += 1;
            }
        }
        else
        {
            for (int i = startingIndexForGroup; i < endingIndexForGroup; i++)
            {
                cardsToDisplay[totalCardsDisplayed].currentCard = cardsInCollection[i];
                cardsToDisplay[totalCardsDisplayed].SetCardData();
                totalCardsDisplayed += 1;
            }
        }
        
        if(totalCardsDisplayed < amountToIncreaseandDecrease)
        {
            for (int i = totalCardsDisplayed; i < amountToIncreaseandDecrease; i++)
            {
                cardsToDisplay[i].ResetCard();
            }
        }
        totalCardsDisplayed = 0;

    }
    */

    /*public void GoForwardPage()
    {
        startingIndexForGroup += amountToIncreaseandDecrease;
        endingIndexForGroup += amountToIncreaseandDecrease;
        if(endingIndexForGroup > cardsInCollection.Count - 1)
        {
            whenNotEnoughCardsInt = endingIndexForGroup;
            endingIndexForGroup = cardsInCollection.Count;
        }
        if(startingIndexForGroup > cardsInCollection.Count - 1)
        {
            startingIndexForGroup = 0;
            endingIndexForGroup = defaultEndingIndex;
        }
        DisplayCardsOwnedChunk();
    }
    public void GoBackwardsInPage()
    {
        if(whenNotEnoughCardsInt != 0)
        {
            startingIndexForGroup -= amountToIncreaseandDecrease;
            endingIndexForGroup = whenNotEnoughCardsInt;
            endingIndexForGroup -= amountToIncreaseandDecrease;
            whenNotEnoughCardsInt = 0;
        }
        else
        {
            startingIndexForGroup -= amountToIncreaseandDecrease;
            endingIndexForGroup -= amountToIncreaseandDecrease;
        }
        
        if (startingIndexForGroup < 0)
        {
            startingIndexForGroup = 0;
            endingIndexForGroup = defaultEndingIndex;
        }

        DisplayCardsOwnedChunk();
    }
    */
    public void SavePageToClipboard()
    {
        collectionString = "";
        if (endingIndexForGroup > cardsInCollection.Count)
        {
            for (int i = 0; i < cardsInCollection.Count - 1; i++)
            {
                collectionString += cardsToDisplay[totalCardsDisplayed].currentCard.amountOwned + " " + cardsToDisplay[totalCardsDisplayed].currentCard.name + "\n";
                totalCardsDisplayed += 1;
            }
        }
        else
        {
            for (int i = startingIndexForGroup; i < endingIndexForGroup; i++)
            {
                collectionString += cardsToDisplay[totalCardsDisplayed].currentCard.amountOwned + " " + cardsToDisplay[totalCardsDisplayed].currentCard.name + "\n";
                totalCardsDisplayed += 1;
            }
        }
        totalCardsDisplayed = 0;
        collectionString.CopyToClipboard();
    }
    public void SaveCollectionToClipboard()
    {
        collectionString = "";
        for (int i = 0; i < cardsInCollection.Count - 1; i++)
        {
            collectionString += cardsInCollection[i].amountOwned + " " + cardsInCollection[i].name + "\n";
        }
        totalCardsDisplayed = 0;
        collectionString.CopyToClipboard();
    }
    /*public void SetupCollection()
    {
        for (int i = 0; i < collectionToPullFrom.ListOfCardsInCollection.Length; i++)
        {
            if (collectionToPullFrom.ListOfCardsInCollection[i].amountOwned > 0)
                cardsInCollection.Add(collectionToPullFrom.ListOfCardsInCollection[i]);
        }
    }*/



    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void DisplayCardsOwnedChunk()
    {
        totalCardsDisplayed = 0;
        if (endingIndexForGroup > cardsInCollection.Count)
        {
            for (int i = 0; i < cardsInCollection.Count; i++)
            {
                cardsToDisplay[totalCardsDisplayed].currentCard = cardsInCollection[i];
                totalCardsDisplayed += 1;
            }
        }
        else
        {
            for (int i = startingIndexForGroup; i < endingIndexForGroup; i++)
            {
                cardsToDisplay[totalCardsDisplayed].currentCard = cardsInCollection[i];
                totalCardsDisplayed += 1;
            }
        }
        DisplayCardImagesChunk();
    }
    public void DisplayCardImagesChunk()
    {
        loadingImages = true;
        totalCardsDisplayed = 0;
        if (endingIndexForGroup > cardsInCollection.Count)
        {
            for (int i = 0; i < cardsInCollection.Count; i++)
            {
                cardsToDisplay[totalCardsDisplayed].SetCardData();
                totalCardsDisplayed += 1;
            }
        }
        else
        {
            for (int i = startingIndexForGroup; i < endingIndexForGroup; i++)
            {
                cardsToDisplay[totalCardsDisplayed].SetCardData();
                totalCardsDisplayed += 1;
            }
        }

        if (totalCardsDisplayed < amountToIncreaseandDecrease)
        {
            for (int i = totalCardsDisplayed; i < amountToIncreaseandDecrease; i++)
            {
                cardsToDisplay[i].ResetCard();
            }
        }
        totalCardsDisplayed = 0;
        loadingImages = false;
    }
    public void GoForwardPage()
    {
        if (!loadingImages)
        {
            if ((startingIndexForGroup + amountToIncreaseandDecrease) < cardsInCollection.Count - 1)
            {
                startingIndexForGroup += amountToIncreaseandDecrease;
                endingIndexForGroup += amountToIncreaseandDecrease;
                if (endingIndexForGroup > cardsInCollection.Count - 1)
                {
                    whenNotEnoughCardsInt = endingIndexForGroup;
                    endingIndexForGroup = cardsInCollection.Count;
                }
                DisplayCardsOwnedChunk();
            }
        }
    }
    public void GoBackwardsInPage()
    {
        if (!loadingImages)
        {
            if (whenNotEnoughCardsInt != 0)
            {
                startingIndexForGroup -= amountToIncreaseandDecrease;
                endingIndexForGroup = whenNotEnoughCardsInt;
                endingIndexForGroup -= amountToIncreaseandDecrease;
                whenNotEnoughCardsInt = 0;
            }
            else
            {
                startingIndexForGroup -= amountToIncreaseandDecrease;
                endingIndexForGroup -= amountToIncreaseandDecrease;
            }

            if (startingIndexForGroup < 0)
            {
                startingIndexForGroup = 0;
                endingIndexForGroup = defaultEndingIndex;
            }

            DisplayCardsOwnedChunk();
        }
        
    }
   
    public void SetupCollection()
    {
        cardsInCollection.Clear();
        if (useFilter)
        {
            if (showingCardsOwned)
            {
                if (useSetFilter)
                {
                    for (int i = 0; i < setToFilter.ListOfCardsInSet.Count; i++)
                    {
                        if (setToFilter.ListOfCardsInSet[i].amountOwned > 0)
                            cardsInCollection.Add(setToFilter.ListOfCardsInSet[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < collectionToPullFrom.ListOfCardsInCollection.Length; i++)
                    {
                        if (collectionToPullFrom.ListOfCardsInCollection[i].amountOwned > 0)
                            cardsInCollection.Add(collectionToPullFrom.ListOfCardsInCollection[i]);
                    }
                }
            }
            else
            {
                if (useSetFilter)
                {
                    for (int i = 0; i < setToFilter.ListOfCardsInSet.Count; i++)
                    {
                        cardsInCollection.Add(setToFilter.ListOfCardsInSet[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < collectionToPullFrom.ListOfCardsInCollection.Length; i++)
                    {
                        cardsInCollection.Add(collectionToPullFrom.ListOfCardsInCollection[i]);
                    }
                }

            }
            if (useCardTypeFilter)
            {
                FilteredList = cardsInCollection.Where(card => card.cardCatagory == typeToDisplay).ToList();
                cardsInCollection = FilteredList;
            }
            if (useRarityFilter)
            {
                FilteredList = cardsInCollection.Where(card => card.rarity == rarityToDisplay).ToList();
                cardsInCollection = FilteredList;
            }
            else if (useSecretPackRarityFilter)
            {
                FilteredList = cardsInCollection.Where(card => card.secretPackRarity == secretPackRarityToDisplay).ToList();
                cardsInCollection = FilteredList;
            }
            if (useLevelFilter)
            {
                FilteredList = cardsInCollection.Where(card => card.cardLevel == levelToDisplay).ToList();
                cardsInCollection = FilteredList;
            }
            if (usePlayCostFilter)
            {
                FilteredList = cardsInCollection.Where(card => card.playCost == playcostToDisplay).ToList();
                cardsInCollection = FilteredList;
            }
            if (useColorFilter)
            {
                FilteredList = cardsInCollection.Where(card => card.color1 == (CardVariable.CardColor)ColorToFilter || card.color2 == (CardVariable.SecondCardColor)ColorToFilter + 1).ToList();
                cardsInCollection = FilteredList;
            }
            if (useNameFilter)
            {
                FilteredList = cardsInCollection.Where(card => card.CardName.ToLower().Contains(cardNameToFilterBy.ToLower()) == true).ToList();
                cardsInCollection = FilteredList;
            }
        }
        else
        {
            if (showingCardsOwned)
            {
                for (int i = 0; i < collectionToPullFrom.ListOfCardsInCollection.Length; i++)
                {
                    if (collectionToPullFrom.ListOfCardsInCollection[i].amountOwned > 0)
                        cardsInCollection.Add(collectionToPullFrom.ListOfCardsInCollection[i]);
                }
            }
            else
            {
                for (int i = 0; i < collectionToPullFrom.ListOfCardsInCollection.Length; i++)
                {
                    cardsInCollection.Add(collectionToPullFrom.ListOfCardsInCollection[i]);
                }
            }

        }

    }
    public void ShowAllCards()
    {
        for (int i = 0; i < collectionToPullFrom.ListOfCardsInCollection.Length; i++)
        {
            cardsInCollection.Add(collectionToPullFrom.ListOfCardsInCollection[i]);
        }
    }
    public void ToggleCardsOwned()
    {
        if (showingCardsOwned)
        {
            startingIndexForGroup = 0;
            endingIndexForGroup = defaultEndingIndex;
            showingCardsOwned = false;
            SetupCollection();
            DisplayCardsOwnedChunk();
        }
        else
        {
            startingIndexForGroup = 0;
            endingIndexForGroup = defaultEndingIndex;
            showingCardsOwned = true;
            SetupCollection();
            DisplayCardsOwnedChunk();
        }
    }
    public void SortListByColor()
    {
        cardsInCollection.Clear();
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        List<CardVariable> SortedListColor = cardsInCollection.OrderBy(card => card.cardCatagory).ToList();
        //cardsInCollection = SortedList;
        cardsInCollection = SortedListColor.OrderBy(card => card.color1).ToList();
        DisplayCardsOwnedChunk();
    }
    public void SortListByRarity()
    {
        cardsInCollection.Clear();
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        List<CardVariable> SortedListRarity = cardsInCollection.OrderBy(card => card.cardCatagory).ToList();
        //cardsInCollection = SortedList;
        //cardsInCollection = SortedList.OrderBy(card => card.color1).ToList();
        if (usingRegularRarity)
            cardsInCollection = SortedListRarity.OrderByDescending(card => card.rarity).ToList();
        else
            cardsInCollection = SortedListRarity.OrderByDescending(card => card.secretPackRarity).ToList();
        DisplayCardsOwnedChunk();
    }
    public void SortListByPlaycost()
    {
        cardsInCollection.Clear();
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        List<CardVariable> SortedListPlayCost = cardsInCollection.OrderBy(card => card.cardCatagory).ToList();
        //cardsInCollection = SortedListPlayCost.OrderBy(card => card.color1).ToList();
        cardsInCollection = SortedListPlayCost.OrderByDescending(card => card.playCost).ToList();
        DisplayCardsOwnedChunk();
    }
    public void SortListByDP()
    {
        cardsInCollection.Clear();
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        List<CardVariable> SortedLiDPst = cardsInCollection.OrderBy(card => card.cardCatagory).ToList();
        cardsInCollection = SortedLiDPst.OrderBy(card => card.color1).ToList();
        cardsInCollection = cardsInCollection.OrderByDescending(card => card.DPOfCard).ToList();
        DisplayCardsOwnedChunk();
    }
    public void SortListByAmountOwned()
    {
        cardsInCollection.Clear();
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        List<CardVariable> SortedListAmountOwned = cardsInCollection.OrderByDescending(card => card.cardCatagory).ToList();
        cardsInCollection = SortedListAmountOwned.OrderBy(card => card.color1).ToList();
        cardsInCollection = cardsInCollection.OrderByDescending(card => card.amountOwned).ToList();
        DisplayCardsOwnedChunk();
    }
    public void SortListByLevel()
    {
        cardsInCollection.Clear();
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        List<CardVariable> SortedListLevel = cardsInCollection.OrderBy(card => card.cardCatagory).ToList();
        cardsInCollection = SortedListLevel.OrderBy(card => card.color1).ToList();
        cardsInCollection = cardsInCollection.OrderByDescending(card => card.cardLevel).ToList();
        DisplayCardsOwnedChunk();
    }
    public void SortListByCardType()
    {
        cardsInCollection.Clear();
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        cardsInCollection = cardsInCollection.OrderBy(card => card.cardCatagory).ToList();
        DisplayCardsOwnedChunk();
    }
    public void ShowOnlyColor(int cardColor)
    {
        cardsInCollection.Clear();
        SetupCollection();
        List<CardVariable> SortedListColorFilter = cardsInCollection.Where(card => card.color1 == (CardVariable.CardColor)cardColor || card.color2 == (CardVariable.SecondCardColor)cardColor + 1).ToList();
        cardsInCollection = SortedListColorFilter;
        DisplayCardsOwnedChunk();
    }

    public void ResetFilters()
    {
        useFilter = false;
        useSetFilter = false;
        useCardTypeFilter = false;
        useRarityFilter = false;
        useSecretPackRarityFilter = false;
        useLevelFilter = false;
        usePlayCostFilter = false;
        useNameFilter = false;
        useColorFilter = false;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }
    public void FilterBySet(CardSet setUsedToFilter)
    {
        useFilter = true;
        useSetFilter = true;
        setToFilter = setUsedToFilter;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }
    public void FilterByColor(int colorUsedToFilter)
    {
        useFilter = true;
        useColorFilter = true;
        ColorToFilter = colorUsedToFilter;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }
    public void FilterByLevel(int levelUsedToFilter)
    {
        useFilter = true;
        useLevelFilter = true;
        levelToDisplay = levelUsedToFilter;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }
    public void FilterByPlayCost(int playCostUsedToFilter)
    {
        useFilter = true;
        usePlayCostFilter = true;
        playcostToDisplay = playCostUsedToFilter;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }
    public void FilterByCardType(int typeUsedToFilter)
    {
        useFilter = true;
        useCardTypeFilter = true;
        typeToDisplay = (CardVariable.CardType)typeUsedToFilter;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }
    public void FilterByRarity(int rarityUsedToFilter)
    {
        useFilter = true;
        useRarityFilter = true;
        useSecretPackRarityFilter = false;
        rarityToDisplay = (CardVariable.CardRarity)rarityUsedToFilter;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }
    public void FilterBySecretPackRarity(int rarityUsedToFilter)
    {
        useFilter = true;
        useSecretPackRarityFilter = true;
        useRarityFilter = false;
        secretPackRarityToDisplay = (CardVariable.SecretPackCardRarity)rarityUsedToFilter;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }

    public void FilterByCardName()
    {
        useFilter = true;
        useNameFilter = true;
        cardNameToFilterBy = CardNameInput.text;
        SetupCollection();
        startingIndexForGroup = 0;
        endingIndexForGroup = defaultEndingIndex;
        DisplayCardsOwnedChunk();
    }

    public void SwapRarityType()
    {
        if (useSecretPackRarityFilter)
        {
            SwapRarity.Raise();
            useSecretPackRarityFilter = false;
            usingRegularRarity = true;
            SetupCollection();
            DisplayCardsOwnedChunk();
        }
        else
        {
            SwapRarity.Raise();
            usingRegularRarity = false;
            useSecretPackRarityFilter = true;
            SetupCollection();
            DisplayCardsOwnedChunk();
        }
    }
}
