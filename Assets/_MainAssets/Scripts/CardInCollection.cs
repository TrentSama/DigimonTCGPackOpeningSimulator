using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class CardInCollection : MonoBehaviour
{
    public CardVariable currentCard;
    public CardVariable defaultCard;
    public Image cardImage;
    public TextMeshProUGUI amountOfCardAvailable;
    public TextMeshProUGUI amountOfCardShadow;
    public Image standardRarityImage;
    public Image secretPackRarityImage;
    public Sprite[] raritySprites;
    public Sprite[] secretPackRaritySprites;

    public Sprite defaultSprite;

    public GameObject altArtGraphic;
    public bool containsAltArt = false;
    public void SetCardData()
    {
        cardImage.sprite = currentCard.cardImage;
        amountOfCardAvailable.text = "x" + currentCard.amountOwned.ToString();
        amountOfCardShadow.text = "x" + currentCard.amountOwned.ToString();
        secretPackRarityImage.sprite = secretPackRaritySprites[(int)currentCard.secretPackRarity];
        standardRarityImage.sprite = raritySprites[(int)currentCard.rarity];
        if (containsAltArt)
            if (altArtGraphic != null)
                altArtGraphic.SetActive(true);
    }
    public void ResetCard()
    {
        cardImage.sprite = defaultCard.cardImage;
        amountOfCardAvailable.text = "x0";
        amountOfCardShadow.text = "x0";
        secretPackRarityImage.sprite = secretPackRaritySprites[0];
        standardRarityImage.sprite = raritySprites[0];
    }
    public void ToggleSecretPackRarity()
    {
        secretPackRarityImage.gameObject.SetActive(!secretPackRarityImage.gameObject.activeSelf);
        standardRarityImage.gameObject.SetActive(!standardRarityImage.gameObject.activeSelf);
    }
}
