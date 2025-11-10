using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class CardInDeckBuilder : MonoBehaviour
{
    public SetupDeckBuilderCollection collectionManager;
    public CardVariable currentCard;
    public CardVariable defaultCard;
    public Image cardImage;
    public TextMeshProUGUI amountOfCardAvailable;
    public TextMeshProUGUI amountOfCardShadow;
    public Image standardRarityImage;
    public Image secretPackRarityImage;
    public Sprite[] raritySprites;
    public Sprite[] secretPackRaritySprites;
    public bool cardShowingBigVersion = false;

    public Sprite defaultSprite;

    public Image BigCard;

    public Color notAvailableColor;
    public GameEvent DeselectAllCards;
    public void SetCardData()
    {
        if(currentCard != null)
        {
            //cardImage.sprite = await ImageGetData.GetCardImageFromFile(currentCard.name);
            cardImage.sprite = currentCard.cardImage;
            amountOfCardAvailable.text = "x" + currentCard.amountOwned.ToString();
            amountOfCardShadow.text = "x" + currentCard.amountOwned.ToString();
            secretPackRarityImage.sprite = secretPackRaritySprites[(int)currentCard.secretPackRarity];
            standardRarityImage.sprite = raritySprites[(int)currentCard.rarity];
            if (currentCard.amountOwned > 0)
                cardImage.color = Color.white;
            else
                cardImage.color = notAvailableColor;
        }
        else
        {
           ResetCard();
        }
        
    }
    public void SetCardDataMainMenu()
    {
        if (currentCard != null)
        {
            //cardImage.sprite = await ImageGetData.GetCardImageFromFile(currentCard.name);
            cardImage.sprite = currentCard.cardImage;
            amountOfCardAvailable.text = "x" + currentCard.amountOwned.ToString();
            amountOfCardShadow.text = "x" + currentCard.amountOwned.ToString();
            secretPackRarityImage.sprite = secretPackRaritySprites[(int)currentCard.secretPackRarity];
            standardRarityImage.sprite = raritySprites[(int)currentCard.rarity];
            cardImage.color = Color.white;
        }
        else
        {
            ResetCard();
        }

    }
    public void ResetCard()
    {
        cardImage.color = notAvailableColor;
        //cardImage.sprite = await ImageGetData.GetCardImageFromFile("CardBack");
        cardImage.sprite = defaultCard.cardImage;
        amountOfCardAvailable.text = "x0";
        amountOfCardShadow.text = "x0";
        secretPackRarityImage.sprite = secretPackRaritySprites[0];
        standardRarityImage.sprite = raritySprites[0];
        currentCard = null;
    }
    public void ShowCardInfoBig()
    {
        if (cardShowingBigVersion && currentCard != null)
        {
            AddCardToDeck();
        }
        else
        {
            DeselectAllCards.Raise();
            BigCard.sprite = cardImage.sprite;
            collectionManager.currentCardDisplayed = currentCard;
            cardShowingBigVersion = true;
        }
    }
    public void ShowCardInfoBigMainMenu()
    {
        if (currentCard != null)
        {
            BigCard.sprite = cardImage.sprite;
        }
    }
    public void AddCardToDeck()
    {
        if (currentCard != null)
        {
            collectionManager.AddCardToDeck(currentCard);
            collectionManager.RefreshDeckView();
        }
    }
    public void RemoveCardFromDeck()
    {
        if (currentCard != null)
        {
            collectionManager.RemoveCardFromDeck(currentCard);
            ResetCard();
            collectionManager.RefreshDeckView();
        }
    }
    public void ToggleSecretPackRarity()
    {
        secretPackRarityImage.gameObject.SetActive(!secretPackRarityImage.gameObject.activeSelf);
        standardRarityImage.gameObject.SetActive(!standardRarityImage.gameObject.activeSelf);
    }
    public void DeselectCard()
    {
        cardShowingBigVersion = false;
    }
}
