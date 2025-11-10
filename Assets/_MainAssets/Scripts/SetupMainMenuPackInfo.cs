using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SetupMainMenuPackInfo : MonoBehaviour
{
    public GlobalSetPullingFrom SetInfo;
    public TextMeshProUGUI SetName;
    public TextMeshProUGUI SetNameSecretPack;
    public TextMeshProUGUI SetNamePromoPack;
    public GameObject BoosterBoxObjects;
    public Image[] KeyCardImages;
    //public TextMeshProUGUI SetDescription;
    public TextMeshProUGUI SetDescriptionSecretPack;
    public GameObject SecretPackObjects;
    public Image BigCardImage;
    public Image BigCardImageAllCards;

    public GameObject PromoPackObjects;

    public bool tooManyCardsInSet = false;
    public GameObject buttonForDisplayAllCards;
    public bool activateOnEnable = true;

    public CardInDeckBuilder[] cardsInPackToDisplay;
    private void OnEnable()
    {
        if (activateOnEnable)
        {
            if (SetInfo.SetPullingFrom != null)
            {
                if (SetInfo.SetPullingFrom.SecretPack || SetInfo.openingCompleteSecretPack)
                {
                    BoosterBoxObjects.SetActive(false);
                    SecretPackObjects.SetActive(true);
                    PromoPackObjects.SetActive(false);
                    SetNameSecretPack.text = "Theme Pack: " + SetInfo.SetPullingFrom.SetName;
                    SetDescriptionSecretPack.text = SetInfo.SetPullingFrom.SetDescription;
                }
                else if (SetInfo.SetPullingFrom.PromoPack)
                {
                    PromoPackObjects.SetActive(true);
                    SecretPackObjects.SetActive(false);
                    BoosterBoxObjects.SetActive(false);
                    SetNamePromoPack.text = SetInfo.SetPullingFrom.SetName;
                    //SetDescription.text = SetInfo.SetPullingFrom.SetDescription;
                }
                else
                {
                    BoosterBoxObjects.SetActive(true);
                    SecretPackObjects.SetActive(false);
                    PromoPackObjects.SetActive(false);
                    SetName.text = SetInfo.SetPullingFrom.SetName;
                    //SetDescription.text = SetInfo.SetPullingFrom.SetDescription;
                }

                for (int i = 0; i < KeyCardImages.Length; i++)
                {
                    KeyCardImages[i].gameObject.SetActive(false);
                }

                if (SetInfo.SetPullingFrom.KeyCards.Count > 0)
                {
                    BigCardImage.sprite = SetInfo.SetPullingFrom.KeyCards[0].cardImage;
                    for (int i = 0; i < SetInfo.SetPullingFrom.KeyCards.Count; i++)
                    {
                        KeyCardImages[i].gameObject.SetActive(true);
                        KeyCardImages[i].sprite = SetInfo.SetPullingFrom.KeyCards[i].cardImage;
                    }
                }
                if (!tooManyCardsInSet)
                {
                    buttonForDisplayAllCards.SetActive(true);
                    DisplayAllCardsInSet();
                }
                else
                {
                    buttonForDisplayAllCards.SetActive(false);
                    tooManyCardsInSet = false;
                }
            }
        }
    }
    public void SetTooManyCardsInPack(bool status)
    {
        tooManyCardsInSet = status;
    }
    public void SetBigImage(int cardIndex)
    {
        BigCardImage.sprite = KeyCardImages[cardIndex].sprite;
    }

   public void DisplayAllCardsInSet()
    {
        for (int i = 0; i < cardsInPackToDisplay.Length; i++)
        {
            cardsInPackToDisplay[i].gameObject.SetActive(false);
            cardsInPackToDisplay[i].ResetCard();
        }
        for (int i = 0; i < SetInfo.SetPullingFrom.ListOfCardsInSet.Count; i++)
        {
            cardsInPackToDisplay[i].gameObject.SetActive(true);
            cardsInPackToDisplay[i].currentCard = SetInfo.SetPullingFrom.ListOfCardsInSet[i];
            cardsInPackToDisplay[i].SetCardDataMainMenu();
        }
        BigCardImageAllCards.sprite = SetInfo.SetPullingFrom.ListOfCardsInSet[0].cardImage;
    }
    public void DisplayAllCardsInPassedInSet(CardSet setToRead)
    {
        for (int i = 0; i < cardsInPackToDisplay.Length; i++)
        {
            cardsInPackToDisplay[i].gameObject.SetActive(false);
            cardsInPackToDisplay[i].ResetCard();
        }
        for (int i = 0; i < setToRead.ListOfCardsInSet.Count; i++)
        {
            cardsInPackToDisplay[i].gameObject.SetActive(true);
            cardsInPackToDisplay[i].currentCard = setToRead.ListOfCardsInSet[i];
            cardsInPackToDisplay[i].SetCardDataMainMenu();
        }
        BigCardImageAllCards.sprite = setToRead.ListOfCardsInSet[0].cardImage;
    }
    public void ShowBigImageAllCardsInSet(Image imageToPass)
    {
        BigCardImageAllCards.sprite = imageToPass.sprite;
    }

}
