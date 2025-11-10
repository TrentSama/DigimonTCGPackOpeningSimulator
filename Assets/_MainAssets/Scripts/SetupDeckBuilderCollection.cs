using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEditor;
using System.Threading.Tasks;

public class SetupDeckBuilderCollection : MonoBehaviour
{
    public CardCollection collectionToPullFrom;

    public GlobalSetPullingFrom SetFileGlobal;

    public List<CardVariable> cardsInCollection = new List<CardVariable>();

    public CardInDeckBuilder[] cardsToDisplay;

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

    public TMP_InputField DeckName;
    
    public List<CardVariable> FilteredList = new List<CardVariable>();

    public List<CardVariable> CardVariablesInDeck = new List<CardVariable>();
    public List<CardVariable> CardVariablesInEggDeck = new List<CardVariable>();

    public List<CardForBuildingDeckView> CardsCurrentlyInDeck = new List<CardForBuildingDeckView>();
    public List<CardForBuildingDeckView> CardsCurrentlyInEggDeck = new List<CardForBuildingDeckView>();

    public CardVariable currentCardDisplayed;

    public CardSet SecretPackBeingMade;
    public CardForBuildingDeckView[] keycardsForPack;

    public int amountOfEggsInDeck = 0;
    public int amountOfLvl3InDeck = 0;
    public int amountOfLvl4InDeck = 0;
    public int amountOfLvl5InDeck = 0;
    public int amountOfLvl6InDeck = 0;
    public int amountOfLvl7InDeck = 0;
    public int amountOfTamersInDeck = 0;
    public int amountOfOptionsInDeck = 0;

    public TextMeshProUGUI[] amountInDeckStrings;

    void Awake()
    {
        defaultEndingIndex = endingIndexForGroup;

        if (SetFileGlobal.currentDeckEditing != null)
        {
            for (int i = 0; i < SetFileGlobal.currentDeckEditing.CardsInDeck.Count; i++)
            {
                CardVariablesInDeck.Add(SetFileGlobal.currentDeckEditing.CardsInDeck[i]);
            }
            for (int i = 0; i < SetFileGlobal.currentDeckEditing.EggCardsInDeck.Count; i++)
            {
                CardVariablesInEggDeck.Add(SetFileGlobal.currentDeckEditing.EggCardsInDeck[i]);
            }
            DeckName.text = SetFileGlobal.currentDeckEditing.DeckName;
        }

        SetAmountOwned();
        RefreshDeckView();
        SetupCollection();
        DisplayCardsOwnedChunk();
    }


    public void DisplayCardsOwnedChunk()
    {
        totalCardsDisplayed = 0;
        if (endingIndexForGroup > cardsInCollection.Count)
        {
            for (int i = 0; i < cardsInCollection.Count; i++)
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

        if (totalCardsDisplayed < amountToIncreaseandDecrease)
        {
            for (int i = totalCardsDisplayed; i < amountToIncreaseandDecrease; i++)
            {
                cardsToDisplay[i].ResetCard();
            }
        }
        totalCardsDisplayed = 0;

    }
    public void GoForwardPage()
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
    public void GoBackwardsInPage()
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
        if(usingRegularRarity)
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

    public void RefreshDeckView()
    {
        /*for (int i = 0; i < CardsCurrentlyInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].ResetCard();
        }
        for (int i = 0; i < CardsCurrentlyInEggDeck.Count; i++)
        {
            CardsCurrentlyInEggDeck[i].ResetCard();
        }*/
        amountOfEggsInDeck = 0;
        amountOfLvl3InDeck = 0;
        amountOfLvl4InDeck = 0;
        amountOfLvl5InDeck = 0;
        amountOfLvl6InDeck = 0;
        amountOfLvl7InDeck = 0;
        amountOfTamersInDeck = 0;
        amountOfOptionsInDeck = 0;

        for (int i = 0; i < CardVariablesInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].currentCard = CardVariablesInDeck[i];
            switch (CardVariablesInDeck[i].cardCatagory)
            {
                case CardVariable.CardType.Digimon:
                    if (CardVariablesInDeck[i].cardLevel == 3)
                        amountOfLvl3InDeck += 1;
                    else if(CardVariablesInDeck[i].cardLevel == 4)
                        amountOfLvl4InDeck += 1;
                    else if (CardVariablesInDeck[i].cardLevel == 5)
                        amountOfLvl5InDeck += 1;
                    else if (CardVariablesInDeck[i].cardLevel == 6)
                        amountOfLvl6InDeck += 1;
                    else if (CardVariablesInDeck[i].cardLevel == 7)
                        amountOfLvl7InDeck += 1;
                    break;
                case CardVariable.CardType.Tamer:
                    amountOfTamersInDeck += 1;
                    break;
                case CardVariable.CardType.Option:
                    amountOfOptionsInDeck += 1;
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < CardVariablesInEggDeck.Count; i++)
        {
            CardsCurrentlyInEggDeck[i].currentCard = CardVariablesInEggDeck[i];
            amountOfEggsInDeck += 1;
        }

        for (int i = CardVariablesInDeck.Count; i < CardsCurrentlyInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].ResetCard();
        }
        for (int i = CardVariablesInEggDeck.Count; i < CardsCurrentlyInEggDeck.Count; i++)
        {
            CardsCurrentlyInEggDeck[i].ResetCard();
        }

        for (int i = 0; i < CardsCurrentlyInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].SetCardData();
        }
        for (int i = 0; i < CardsCurrentlyInEggDeck.Count; i++)
        {
            CardsCurrentlyInEggDeck[i].SetCardData();
        }
        RefreshAmountOfCards();
    }

    public void RefreshAmountOfCards()
    {
        amountInDeckStrings[0].text = amountOfEggsInDeck.ToString();
        amountInDeckStrings[1].text = amountOfLvl3InDeck.ToString();
        amountInDeckStrings[2].text = amountOfLvl4InDeck.ToString();
        amountInDeckStrings[3].text = amountOfLvl5InDeck.ToString();
        amountInDeckStrings[4].text = amountOfLvl6InDeck.ToString();
        amountInDeckStrings[5].text = amountOfLvl7InDeck.ToString();
        amountInDeckStrings[6].text = amountOfTamersInDeck.ToString();
        amountInDeckStrings[7].text = amountOfOptionsInDeck.ToString();
    }

    public void AddCardToDeck(CardVariable CardToAdd)
    {
        if (CardToAdd.amountInCurrentDeck < CardToAdd.amountOwned && CardToAdd.amountInCurrentDeck < 4)
        {
            if (CardToAdd.cardCatagory == CardVariable.CardType.Digitama)
            {
                if (CardVariablesInEggDeck.Count < 5)
                {
                    CardVariablesInEggDeck.Add(CardToAdd);
                    CardToAdd.amountInCurrentDeck += 1;
                }
            }

            else
            {
                if (CardVariablesInDeck.Count < 50)
                {
                    CardVariablesInDeck.Add(CardToAdd);
                    CardToAdd.amountInCurrentDeck += 1;
                }
            }
        }
        //RefreshDeckView();
    }

    
    public void AddCurrentSelectedCardToDeck()
    {
        if (currentCardDisplayed.amountInCurrentDeck < currentCardDisplayed.amountOwned && currentCardDisplayed.amountInCurrentDeck < 4)
        {
            if (currentCardDisplayed.cardCatagory == CardVariable.CardType.Digitama)
            {
                if (CardVariablesInEggDeck.Count < 5)
                {
                    CardVariablesInEggDeck.Add(currentCardDisplayed);
                    currentCardDisplayed.amountInCurrentDeck += 1;
                }
            }

            else
            {
                if (CardVariablesInDeck.Count < 50)
                {
                    CardVariablesInDeck.Add(currentCardDisplayed);
                    currentCardDisplayed.amountInCurrentDeck += 1;
                }
            }
        }
        RefreshDeckView();
    }

    public void RemoveCardFromDeck(CardVariable CardToAdd)
    {
        CardVariablesInDeck.Remove(CardToAdd);
        if(CardToAdd.amountInCurrentDeck > 0)
            CardToAdd.amountInCurrentDeck -= 1;
        //RefreshDeckView();
    }
    public void RemoveCurrentSelectedCardFromDeck()
    {
        if(currentCardDisplayed.cardCatagory == CardVariable.CardType.Digitama)
        {
            CardVariablesInEggDeck.Remove(currentCardDisplayed);
            if (currentCardDisplayed.amountInCurrentDeck > 0)
                currentCardDisplayed.amountInCurrentDeck -= 1;
        }
        else
        {
            CardVariablesInDeck.Remove(currentCardDisplayed);
            if (currentCardDisplayed.amountInCurrentDeck > 0)
                currentCardDisplayed.amountInCurrentDeck -= 1;
        }
        
        RefreshDeckView();
    }
    public void SetAmountOwned()
    {
        for (int i = 0; i < collectionToPullFrom.ListOfCardsInCollection.Length; i++)
        {
            collectionToPullFrom.ListOfCardsInCollection[i].amountInCurrentDeck = 0;
        }
        for (int i = 0; i < CardVariablesInDeck.Count; i++)
        {
            CardVariablesInDeck[i].amountInCurrentDeck += 1;
        }
    }
    public void ResetDeck()
    {
        for (int i = 0; i < CardsCurrentlyInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].ResetCard();
        }
        for (int i = 0; i < CardsCurrentlyInEggDeck.Count; i++)
        {
            CardsCurrentlyInEggDeck[i].ResetCard();
        }
        CardVariablesInDeck.Clear();
        CardVariablesInEggDeck.Clear();
        RefreshDeckView();
        SetAmountOwned();
    }
    public void SortCardsInDeck()
    {
        CardVariablesInDeck = CardVariablesInDeck.OrderByDescending(card => card.name).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderByDescending(card => card.cardCatagory).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderBy(card => card.color1).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderBy(card => card.cardLevel).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderBy(card => card.cardCatagory).ToList();
        RefreshDeckView();
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

    public void ExportDeck()
    {
        collectionString = "[";

        for (int i = 0; i < CardVariablesInEggDeck.Count; i++)
        {
            collectionString += "\"" + CardVariablesInEggDeck[i].name + "\",";
        }
        for (int i = 0; i < CardVariablesInDeck.Count; i++)
        {
            collectionString += "\"" + CardVariablesInDeck[i].name + "\",";
        }
        collectionString = collectionString.Substring(0, collectionString.Length - 1);
        collectionString += "]";
        collectionString.CopyToClipboard();
    }
    public void SaveDeck()
    {
        SetFileGlobal.currentDeckEditing.DeckName = DeckName.text;
        SetFileGlobal.currentDeckEditing.CardsInDeck = CardVariablesInDeck;
        SetFileGlobal.currentDeckEditing.EggCardsInDeck = CardVariablesInEggDeck;
#if UNITY_EDITOR
        EditorUtility.SetDirty(SetFileGlobal.currentDeckEditing);
#endif  
    }
    public void SetDeckMainCard()
    {
        SetFileGlobal.currentDeckEditing.CurrentDeckPicture = currentCardDisplayed;
#if UNITY_EDITOR
        EditorUtility.SetDirty(SetFileGlobal.currentDeckEditing);
#endif  
    }

    public void AddCardToSecretPack(CardVariable CardToAdd)
    {
        if (CardToAdd.amountInCurrentDeck < 1)
        {
            if (CardVariablesInDeck.Count < 40)
            {
                CardVariablesInDeck.Add(CardToAdd);
                CardToAdd.amountInCurrentDeck += 1;
            }
        }
        RefreshDeckView();
    }
    public void AddCurrentSelectedCardToSecretPack()
    {
        if (currentCardDisplayed.amountInCurrentDeck < 1)
        {
            if (CardVariablesInDeck.Count < 40)
            {
                CardVariablesInDeck.Add(currentCardDisplayed);
                currentCardDisplayed.amountInCurrentDeck += 1;
            }
        }
        RefreshSecretPackView();
    }
    public void RemoveCurrentSelectedCardFromSecretPack()
    {
        CardVariablesInDeck.Remove(currentCardDisplayed);
        if (currentCardDisplayed.amountInCurrentDeck > 0)
            currentCardDisplayed.amountInCurrentDeck -= 1;
        RefreshSecretPackView();
    }
    public void RefreshSecretPackView()
    {
        amountOfEggsInDeck = 0;
        amountOfLvl3InDeck = 0;
        amountOfLvl4InDeck = 0;
        amountOfLvl5InDeck = 0;
        amountOfLvl6InDeck = 0;
        amountOfLvl7InDeck = 0;
        amountOfTamersInDeck = 0;
        amountOfOptionsInDeck = 0;

        for (int i = 0; i < CardVariablesInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].currentCard = CardVariablesInDeck[i];
            switch (CardVariablesInDeck[i].cardCatagory)
            {
                case CardVariable.CardType.Digimon:
                    if (CardVariablesInDeck[i].cardLevel == 3)
                        amountOfLvl3InDeck += 1;
                    else if (CardVariablesInDeck[i].cardLevel == 4)
                        amountOfLvl4InDeck += 1;
                    else if (CardVariablesInDeck[i].cardLevel == 5)
                        amountOfLvl5InDeck += 1;
                    else if (CardVariablesInDeck[i].cardLevel == 6)
                        amountOfLvl6InDeck += 1;
                    else if (CardVariablesInDeck[i].cardLevel == 7)
                        amountOfLvl7InDeck += 1;
                    break;
                case CardVariable.CardType.Tamer:
                    amountOfTamersInDeck += 1;
                    break;
                case CardVariable.CardType.Option:
                    amountOfOptionsInDeck += 1;
                    break;
                case CardVariable.CardType.Digitama:
                    amountOfEggsInDeck += 1;
                    break;
                default:
                    break;
            }
        }


        for (int i = 0; i < CardVariablesInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].currentCard = CardVariablesInDeck[i];
        }
        for (int i = CardVariablesInDeck.Count; i < CardsCurrentlyInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].ResetCard();
        }
        for (int i = 0; i < CardsCurrentlyInDeck.Count; i++)
        {
            CardsCurrentlyInDeck[i].SetCardData();
        }
        RefreshAmountOfCards();

    }
    public void SortCardsInSecretPack()
    {
        CardVariablesInDeck = CardVariablesInDeck.OrderByDescending(card => card.name).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderByDescending(card => card.cardCatagory).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderBy(card => card.color1).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderBy(card => card.cardLevel).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderBy(card => card.cardCatagory).ToList();
        CardVariablesInDeck = CardVariablesInDeck.OrderByDescending(card => card.secretPackRarity).ToList();
        RefreshSecretPackView();
    }
    public void SaveSecretPack()
    {
        SecretPackBeingMade.ListOfCardsInSet.Clear();
        for (int i = 0; i < CardVariablesInDeck.Count; i++)
        {
            SecretPackBeingMade.ListOfCardsInSet.Add(CardVariablesInDeck[i]);
        }
#if UNITY_EDITOR

        /*for (int i = 0; i < CardVariablesInDeck.Count; i++)
        {
            if (CardVariablesInDeck[i] != null)
            {
                if (CardVariablesInDeck[i].secretPackRarity == CardVariable.SecretPackCardRarity.SuperRare
                    || CardVariablesInDeck[i].secretPackRarity == CardVariable.SecretPackCardRarity.UltraRare)
                {
                    CardVariablesInDeck[i].secretPackToUnlock = SecretPackBeingMade;
                    EditorUtility.SetDirty(CardVariablesInDeck[i]);
                }
            }
        }*/

        EditorUtility.SetDirty(SecretPackBeingMade);
#endif  

    }
    public void AddKeyCard()
    {
        SecretPackBeingMade.KeyCards.Add(currentCardDisplayed);
        SetupSecretPackKeyCards();
#if UNITY_EDITOR
        EditorUtility.SetDirty(currentCardDisplayed);
#endif 

    }
    public void SetRarityOfCurrentCard(int rarity)
    {
        if (currentCardDisplayed != null)   
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
#if UNITY_EDITOR
            EditorUtility.SetDirty(currentCardDisplayed);
#endif
            SortCardsInSecretPack();
        }
    }
    public void LoadSecretPack()
    {
        CardVariablesInDeck.Clear();
        for (int i = 0; i < SecretPackBeingMade.ListOfCardsInSet.Count; i++)
        {
            CardVariablesInDeck.Add(SecretPackBeingMade.ListOfCardsInSet[i]);
            //CardsCurrentlyInDeck[i].SetCardData();
        }
        SetupSecretPackKeyCards();
        RefreshSecretPackView();
        SortCardsInSecretPack();
    }
    public void ResetKeycards()
    {
        SecretPackBeingMade.KeyCards.Clear();
#if UNITY_EDITOR
        EditorUtility.SetDirty(SecretPackBeingMade);
#endif 
        SetupSecretPackKeyCards();
    }
    public void SetupSecretPackKeyCards()
    {
        for (int i = 0; i < SecretPackBeingMade.KeyCards.Count; i++)
        {
            keycardsForPack[i].currentCard = SecretPackBeingMade.KeyCards[i];
            keycardsForPack[i].SetCardData();
        }
    }
    public void SetSecretPackKeyCard(int position)
    {
        SecretPackBeingMade.KeyCards[position] = currentCardDisplayed;
        SetupSecretPackKeyCards();
#if UNITY_EDITOR
        EditorUtility.SetDirty(currentCardDisplayed);
#endif 
    }
}
