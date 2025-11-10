using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class CardForBuildingDeckView : MonoBehaviour
{
    public SetupDeckBuilderCollection collectionManager;
    public CardVariable currentCard;
    public CardVariable defaultCard;
    public Image cardImage;
    public Image standardRarityImage;
    public Image secretPackRarityImage;
    public Sprite[] raritySprites;
    public Sprite[] secretPackRaritySprites;

    public Sprite defaultSprite;

    public Image BigCard;

    public Color notAvailableColor;

    public GameEvent DeselectAllCards;
    public void SetCardData()
    {
        if (currentCard != null)
        {
            cardImage.sprite = currentCard.cardImage;
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
        cardImage.sprite = defaultCard.cardImage;
        secretPackRarityImage.sprite = secretPackRaritySprites[0];
        standardRarityImage.sprite = raritySprites[0];
        currentCard = null;
    }
    public void ShowCardInfoBig()
    {
        if (currentCard != null)
        {
            DeselectAllCards.Raise();
            BigCard.sprite = cardImage.sprite;
            collectionManager.currentCardDisplayed = currentCard;
        }
           
    }
    public void AddCardToDeck()
    {
        if(currentCard != null)
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

    }
    /*public async void GetCardImageFromFile()
    {
        if (File.Exists(CardPath))
        {
            byte[] imageBuff = await ReadFile(CardPath);
            Texture2D tex = BinaryToTexture(imageBuff);

            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            cardImage.sprite = sprite;
        }
    }*/
}
