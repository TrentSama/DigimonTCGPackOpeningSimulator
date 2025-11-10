using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardWhenMakingSecretPack : MonoBehaviour
{
    public SecretPackManager collectionManager;
    public CardVariable currentCard;
    public Image cardImage;
    public Image standardRarityImage;
    public Image secretPackRarityImage;
    public Sprite[] raritySprites;
    public Sprite[] secretPackRaritySprites;

    public Sprite defaultSprite;

    public Image BigCard;

    //public Color notAvailableColor;

    public GameEvent DeselectAllCards;
    public async void SetCardData()
    {
        if (currentCard != null)
        {
            //cardImage.sprite = currentCard.cardImage;
            cardImage.sprite = await ImageGetData.GetCardImageFromFile(currentCard.name);
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
        //cardImage.color = notAvailableColor;
        cardImage.sprite = defaultSprite;
        secretPackRarityImage.sprite = secretPackRaritySprites[0];
        standardRarityImage.sprite = raritySprites[0];
        currentCard = null;
    }
    public async void ShowCardInfoBig()
    {
        if (currentCard != null)
        {
            DeselectAllCards.Raise();
            //BigCard.sprite = currentCard.cardImage;
            BigCard.sprite = await ImageGetData.GetCardImageFromFile(currentCard.name);
            collectionManager.currentCardDisplayed = currentCard;
        }

    }

    public void ToggleSecretPackRarity()
    {
        secretPackRarityImage.gameObject.SetActive(!secretPackRarityImage.gameObject.activeSelf);
        standardRarityImage.gameObject.SetActive(!standardRarityImage.gameObject.activeSelf);
    }
    public void DeselectCard()
    {

    }
}
