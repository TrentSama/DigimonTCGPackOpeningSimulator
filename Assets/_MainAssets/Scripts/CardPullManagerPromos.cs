using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEditor;

public class CardPullManagerPromos : MonoBehaviour
{
    [Header("Main Pack Variables")]
    public GlobalSetPullingFrom SetData;

    public RevealCardObject[] PromoPackCards;
    public TextMeshProUGUI currentPackOpeningText;

    public List<CardVariable> TempCards = new();

    public int cardsInPack = 2;
    public float timeBetweenReveals = 0.8f;
    public int currentPack = 0;
    public int totalPacksBeingOpened = 10;

    public List<CardVariable> CardsPulledThisOpening = new();
    public List<CardInCollection> CardsPulledObjects = new();

    public GameObject cardPullingMainObject;
    public GameObject FinishedPullingButton;
    public GameObject PullNextPackButton;

    public int cardsRevealed = 0;
    bool startedOpeningAnimation = false;

    void Awake()
    {
        cardsInPack = SetData.amountOfCardsInPromoPack;
        EnableCorrectAmountOfCards();
        ReloadPromoCards();
        totalPacksBeingOpened = SetData.amountOfPacksPulled;
        PullPromoPack();
        currentPackOpeningText.text = "Pack " + ((currentPack + 1).ToString() + " / " + totalPacksBeingOpened.ToString());

    }

    public void EnableCorrectAmountOfCards()
    {
        for (int i = 0; i < cardsInPack; i++)
        {
            PromoPackCards[i].gameObject.SetActive(true);
        }
    }

    public void SetupPackAnimation()
    {
        StartCoroutine(PackSwapAnimation());
    }
    public IEnumerator PackSwapAnimation()
    {
        cardPullingMainObject.GetComponent<Animator>().Play("MenuFadeOut");
        yield return new WaitForSeconds(0.3f);
        SetupNextPack();
        yield return new WaitForSeconds(0.2f);
        cardPullingMainObject.GetComponent<Animator>().Play("MenuFadeIn");
        yield return new WaitForSeconds(0.2f);
        startedOpeningAnimation = false;
    }
    public void IncreaseCardsPulled()
    {
        cardsRevealed += 1;
        if (cardsRevealed == SetData.amountOfCardsInPromoPack)
        {
            if (currentPack + 1 == SetData.amountOfPacksPulled)
                FinishedPullingButton.SetActive(true);
            else
                PullNextPackButton.SetActive(true);
        }
    }

    public void PullPackStandardMethod()
    {
        if (!startedOpeningAnimation)
        {
            StartCoroutine(PullStandardPackAuto());
            startedOpeningAnimation = true;
        }
        
    }

    public void ReloadPromoCards()
    {
        TempCards.Clear();
        for (int i = 0; i < SetData.SetPullingFrom.ListOfCardsInSet.Count; i++)
        {
            TempCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
        }
        
    }

    private IEnumerator PullStandardPackAuto()
    {
        for (int i = 0; i < cardsInPack; i++)
        {
            if (!PromoPackCards[i].hasBeenRevealed)
            {
                if (PromoPackCards[i].URAnim)
                    PromoPackCards[i].GetComponent<Animator>().Play("CardPullUR");
                else if (PromoPackCards[i].SRAnim)
                    PromoPackCards[i].GetComponent<Animator>().Play("CardPullSR");
                else
                    PromoPackCards[i].GetComponent<Animator>().Play("CardPullNormal");
                PromoPackCards[i].SRAnim = false;
                PromoPackCards[i].URAnim = false;
                yield return new WaitForSeconds(timeBetweenReveals);
            }
        }
        /*if (currentPack + 1 == SetData.amountOfPacksPulled)
            FinishedPullingButton.SetActive(true);
        else
            PullNextPackButton.SetActive(true);
        cardsRevealed = 0;
        */
    }

    public void SetupNextPack()
    {
        if (currentPack < totalPacksBeingOpened)
        {
            for (int i = 0; i < PromoPackCards.Length; i++)
            {
                PromoPackCards[i].ResetCard();
            }
            currentPack += 1;
            currentPackOpeningText.text = "Pack " + ((currentPack + 1).ToString() + " / " + totalPacksBeingOpened.ToString());
            PullPromoPack();
            cardsRevealed = 0;
        }

    }

    public void GenerateTheLastPacks()
    {
        for (int i = currentPack + 1; i < totalPacksBeingOpened; i++)
        {
            Debug.Log(i);
            PullPromoPack();
        }
    }

    public void PullPromoPack()
    {
        int ran = 0;
        // Pulling the first half of the pack (This is the Complete pack side.)
        for (int i = 0; i < cardsInPack; i++)
        {
            ran = Random.Range(0, TempCards.Count);
            PromoPackCards[i].setCardForObject = TempCards[ran];
            if (PromoPackCards[i].setCardForObject.rarity == CardVariable.CardRarity.SuperRare)
                PromoPackCards[i].SRAnim = true;
            else if (PromoPackCards[i].setCardForObject.rarity == CardVariable.CardRarity.SecretRare)
                PromoPackCards[i].URAnim = true;
            CardsPulledThisOpening.Add(TempCards[ran]);
            TempCards.RemoveAt(ran);
        }
        ReloadPromoCards();

    }

    public void SetupCardsPulled()
    {
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.name).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.color1).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.cardCatagory).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderByDescending(card => card.rarity).ToList();

        for (int i = 0; i < CardsPulledThisOpening.Count; i++)
        {
            CardsPulledObjects[i].gameObject.SetActive(true);
            CardsPulledObjects[i].currentCard = CardsPulledThisOpening[i];
            CardsPulledObjects[i].currentCard.amountOwned += 1;
            if (CardsPulledObjects[i].currentCard.amountOwned > 9)
                CardsPulledObjects[i].currentCard.amountOwned = 9;
#if UNITY_EDITOR
            EditorUtility.SetDirty(CardsPulledObjects[i].currentCard);
#endif
            CardsPulledObjects[i].SetCardData();
        }

    }
}
