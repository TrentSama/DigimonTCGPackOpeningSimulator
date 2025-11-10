using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System.Threading.Tasks;

public class CardFileEditor : MonoBehaviour
{
    [Header("Main Pack Variables")]
    public CardSet SetEditingFrom;

    public Image cardImage;
    public TextMeshProUGUI cardColorText;
    public TextMeshProUGUI cardRarityText;
    public TextMeshProUGUI cardSecretPackRarityText;

    public TMP_InputField inputCardNumber;

    int currentCardIndex = 0;

    public bool useKeyboardControls = false;
    public bool EditPlayCost = false;
    public bool EditCardType = false;
    public bool EditDP = false;
    public bool EditLevel = false;
    public bool EditRarity = false;

    public async void Awake()
    {
        await SetupCardFile();
    }
    private void Update()
    {
        if (useKeyboardControls)
        {
             if (EditCardType)
            {
                EditingCardType();
            }
            else if (EditRarity)
            {
                EditingRarityOfCard();
            }
            else
            {
                EditNumberOfCard();
            }
        }
    }
    public void EditNumberOfCard()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (EditPlayCost)
            {
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].playCost = int.Parse(inputCardNumber.text);
                //SetupCardFile();
                SaveScriptableObject();
                NextCard(1);
                inputCardNumber.text = "";
                inputCardNumber.ActivateInputField();
            }
            else if (EditDP)
            {
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].DPOfCard = (int.Parse(inputCardNumber.text) * 1000);
                //SetupCardFile();
                SaveScriptableObject();
                NextCard(1);
                inputCardNumber.text = "";
                inputCardNumber.ActivateInputField();
            }
            else if (EditLevel)
            {
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].cardLevel = int.Parse(inputCardNumber.text);
                //SetupCardFile();
                SaveScriptableObject();
                NextCard(1);
                inputCardNumber.text = "";
                inputCardNumber.ActivateInputField();
            }
        }
    }
    public void EditingCardType()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetCardType(0);
            NextCard(1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetCardType(1);
            NextCard(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCardType(2);
            NextCard(1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetCardType(3);
            NextCard(1);
        }
    } 
    public void EditingRarityOfCard()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetRarity(0);
            NextCard(1);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetRarity(1);
            NextCard(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetRarity(2);
            NextCard(1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            SetRarity(3);
            NextCard(1);
        }
    }
    public void SaveScriptableObject()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(SetEditingFrom.ListOfCardsInSet[currentCardIndex]);
#endif 
    }
    public void SaveEveryScriptableObject()
    {
#if UNITY_EDITOR
        for (int i = 0; i < SetEditingFrom.ListOfCardsInSet.Count; i++)
        {
            EditorUtility.SetDirty(SetEditingFrom.ListOfCardsInSet[i]);
        }
#endif

    }
    public async Task SetupCardFile()
    {
        cardImage.sprite = await ImageGetData.GetCardImageFromFile(SetEditingFrom.ListOfCardsInSet[currentCardIndex].name);
        cardColorText.text = SetEditingFrom.ListOfCardsInSet[currentCardIndex].color1.ToString();
        cardRarityText.text = SetEditingFrom.ListOfCardsInSet[currentCardIndex].rarity.ToString();
        cardSecretPackRarityText.text = SetEditingFrom.ListOfCardsInSet[currentCardIndex].secretPackRarity.ToString();
    }
    public async void NextCard(int amountToIncrease)
    {
        currentCardIndex += amountToIncrease;
        if (currentCardIndex > SetEditingFrom.ListOfCardsInSet.Count - 1)
            currentCardIndex = 0;
        else if (currentCardIndex < 0)
            currentCardIndex = SetEditingFrom.ListOfCardsInSet.Count - 1;
        await SetupCardFile();
        SaveScriptableObject();
    }
    public async void SetRarity(int cardRarity)
    {
        switch (cardRarity)
        {
            case 0:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].rarity = CardVariable.CardRarity.Common;
                break;
            case 1:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].rarity = CardVariable.CardRarity.Uncommon;
                break;
            case 2:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].rarity = CardVariable.CardRarity.Rare;
                break;
            case 3:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].rarity = CardVariable.CardRarity.SuperRare;
                break;
            case 4:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].rarity = CardVariable.CardRarity.SecretRare;
                break;
            default:
                break;
        }
        await SetupCardFile();
        SaveScriptableObject();
    }
    public async void SetCardType(int cardRarity)
    {
        switch (cardRarity)
        {
            case 0:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].cardCatagory = CardVariable.CardType.Digimon;
                break;
            case 1:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].cardCatagory = CardVariable.CardType.Digitama;
                break;
            case 2:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].cardCatagory = CardVariable.CardType.Tamer;
                break;
            case 3:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].cardCatagory = CardVariable.CardType.Option;
                break;
            default:
                break;
        }
        await SetupCardFile();
        SaveScriptableObject();
    }
    public async void SetSecretPackRarity(int cardRarity)
    {
        switch (cardRarity)
        {
            case 0:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].secretPackRarity = CardVariable.SecretPackCardRarity.Common;
                break;
            case 1:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].secretPackRarity = CardVariable.SecretPackCardRarity.Rare;
                break;
            case 2:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].secretPackRarity = CardVariable.SecretPackCardRarity.SuperRare;
                break;
            case 3:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].secretPackRarity = CardVariable.SecretPackCardRarity.UltraRare;
                break;
            default:
                break;
        }
        await SetupCardFile();
        SaveScriptableObject();
        NextCard(1);
    }
    public async void SetCardColor(int CardColor)
    {
        switch (CardColor)
        {
            case 0:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].color2 = CardVariable.SecondCardColor.Red;
                break;
            case 1:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].color2 = CardVariable.SecondCardColor.Blue;
                break;
            case 2:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].color2 = CardVariable.SecondCardColor.Yellow;
                break;
            case 3:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].color2 = CardVariable.SecondCardColor.Green;
                break;
            case 4:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].color2 = CardVariable.SecondCardColor.Black;
                break;
            case 5:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].color2 = CardVariable.SecondCardColor.Purple;
                break;
            case 6:
                SetEditingFrom.ListOfCardsInSet[currentCardIndex].color2 = CardVariable.SecondCardColor.White;
                break;
            default:
                break;
        }
        await SetupCardFile();
        SaveScriptableObject();
        NextCard(1);

    }

}
