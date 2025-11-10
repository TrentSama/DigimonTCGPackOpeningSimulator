using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEditor;

public class CardPullManagerThemePacks : MonoBehaviour
{
    public Animator cardAnimator;
    [Header("Main Pack Variables")]
    public GlobalSetPullingFrom SetData;
    //public CardSet SetPullingFrom;
    public List<CardVariable> NormalCards = new List<CardVariable>();
    public List<CardVariable> RareCards = new List<CardVariable>();
    public List<CardVariable> SRCards = new List<CardVariable>();
    public List<CardVariable> SSRCards = new List<CardVariable>();


    public CardSet CompletePack;
    public List<CardVariable> GlobalNormalCards = new List<CardVariable>();
    public List<CardVariable> GlobalRareCards = new List<CardVariable>();
    public List<CardVariable> GlobalSRCards = new List<CardVariable>();
    public List<CardVariable> GlobalSSRCards = new List<CardVariable>();

    public RevealCardObject[] ThemePackCards;
    public TextMeshProUGUI currentPackOpeningText;

    public int chanceForNormalThreshold = 55;
    public int chanceForRareThreshold = 90;
    public int chanceForSRThreshold = 97;

    public float timeBetweenReveals = 0.8f;
    public int currentPack = 0;
    public int totalPacksBeingOpened = 10;

    public List<CardVariable> CardsPulledThisOpening = new();
    public List<CardInCollection> CardsPulledObjects = new();
    public List<CardSet> ThemePacksUnlocked = new();
    public List<MenuThemePackButton> ThemePackButtons = new();

    public GameObject cardPullingMainObject;
    public GameObject FinishedPullingButton;
    public GameObject PullNextPackButton;

    public int cardsRevealed = 0;

    bool startedOpeningAnimation = false;
    void Awake()
    {
        SeperateSetIntoRarities();
        totalPacksBeingOpened = SetData.amountOfPacksPulled;
        PullThemePack();
        currentPackOpeningText.text = "Pack " + ((currentPack + 1).ToString() + " / " + totalPacksBeingOpened.ToString());

    }

    public void PullPackStandardMethod()
    {
        if (!startedOpeningAnimation)
        {
            StartCoroutine(PullStandardPackAuto());
            startedOpeningAnimation = true;
        }
            
    }

    private IEnumerator PullStandardPackAuto()
    {
        for (int i = 0; i < ThemePackCards.Length; i++)
        {
            if (!ThemePackCards[i].hasBeenRevealed)
            {
                if (ThemePackCards[i].URAnim)
                    ThemePackCards[i].GetComponent<Animator>().Play("CardPullUR");
                else if (ThemePackCards[i].SRAnim)
                    ThemePackCards[i].GetComponent<Animator>().Play("CardPullSR");
                else
                    ThemePackCards[i].GetComponent<Animator>().Play("CardPullNormal");
                ThemePackCards[i].SRAnim = false;
                ThemePackCards[i].URAnim = false;
                yield return new WaitForSeconds(timeBetweenReveals);
            }  
        }
       /* if (currentPack + 1 == SetData.amountOfPacksPulled)
            FinishedPullingButton.SetActive(true);
        else
            PullNextPackButton.SetActive(true);
       */
    }

    public void SetupNextPack()
    {
        if (currentPack < totalPacksBeingOpened)
        {
            for (int i = 0; i < ThemePackCards.Length; i++)
            {
                ThemePackCards[i].ResetCard();
            }
            currentPack += 1;
            currentPackOpeningText.text = "Pack " + ((currentPack + 1).ToString() + " / " + totalPacksBeingOpened.ToString());
            PullThemePack();
            cardsRevealed = 0; 
        }

    }
    public void SetupPackAnimation()
    {
        StartCoroutine(PackSwapAnimation());
    }
    public IEnumerator PackSwapAnimation()
    {
        cardAnimator.Play("MenuFadeOut", 0, 0);
        yield return new WaitForSeconds(0.3f);
        SetupNextPack();
        yield return new WaitForSeconds(0.3f);
        cardAnimator.Play("MenuFadeIn",0 ,0);
        yield return new WaitForSeconds(0.2f);
        startedOpeningAnimation = false;
    }
    public void IncreaseCardsPulled()
    {
        cardsRevealed += 1;
        if (cardsRevealed == 10)
        {
            if (currentPack + 1 == SetData.amountOfPacksPulled)
                FinishedPullingButton.SetActive(true);
            else
                PullNextPackButton.SetActive(true);
        }
    }
    public void SeperateSetIntoRarities()
    {
        for (int i = 0; i < CompletePack.ListOfCardsInSet.Count; i++)
        {
            switch (CompletePack.ListOfCardsInSet[i].secretPackRarity)
            {
                case CardVariable.SecretPackCardRarity.Common:
                    GlobalNormalCards.Add(CompletePack.ListOfCardsInSet[i]);
                    break;
                case CardVariable.SecretPackCardRarity.Rare:
                    GlobalRareCards.Add(CompletePack.ListOfCardsInSet[i]);
                    break;
                case CardVariable.SecretPackCardRarity.SuperRare:
                    GlobalSRCards.Add(CompletePack.ListOfCardsInSet[i]);
                    break;
                case CardVariable.SecretPackCardRarity.UltraRare:
                    GlobalSSRCards.Add(CompletePack.ListOfCardsInSet[i]);
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i < SetData.SetPullingFrom.ListOfCardsInSet.Count; i++)
        {
            switch (SetData.SetPullingFrom.ListOfCardsInSet[i].secretPackRarity)
            {
                case CardVariable.SecretPackCardRarity.Common:
                    NormalCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.SecretPackCardRarity.Rare:
                    RareCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.SecretPackCardRarity.SuperRare:
                    SRCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.SecretPackCardRarity.UltraRare:
                    SSRCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                default:
                    break;
            }
        }
    }

    public void PullThemePack()
    {
        int ran = 0;
        int cardRarityRan = 0;
        if (SetData.openingSecretPack)
        {
            // Pulling the first half of the pack (This is the Complete pack side.)
            for (int i = 0; i < 5; i++)
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= chanceForNormalThreshold)
                {
                    ran = Random.Range(0, GlobalNormalCards.Count);
                    ThemePackCards[i].setCardForObject = GlobalNormalCards[ran];
                }
                else if (cardRarityRan > chanceForNormalThreshold && cardRarityRan <= chanceForRareThreshold)
                {
                    ran = Random.Range(0, GlobalRareCards.Count);
                    ThemePackCards[i].setCardForObject = GlobalRareCards[ran];
                }
                else if (cardRarityRan > chanceForRareThreshold && cardRarityRan <= chanceForSRThreshold)
                {
                    ran = Random.Range(0, GlobalSRCards.Count);
                    ThemePackCards[i].setCardForObject = GlobalSRCards[ran];
                    ThemePackCards[i].SRAnim = true;
                    if (GlobalSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < GlobalSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(GlobalSRCards[ran].secretPackToUnlock[j]);
                        }
                        
                    }
                       
                }
                else
                {
                    ran = Random.Range(0, GlobalSSRCards.Count);
                    ThemePackCards[i].setCardForObject = GlobalSSRCards[ran];
                    ThemePackCards[i].URAnim = true;
                    if (GlobalSSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < GlobalSSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(GlobalSSRCards[ran].secretPackToUnlock[j]);
                        }

                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[i].setCardForObject);
            }
            // Pulling the second half of the pack (Which is the Theme pack side.)
            for (int i = 5; i < 9; i++)
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= chanceForNormalThreshold)
                {
                    ran = Random.Range(0, NormalCards.Count);
                    ThemePackCards[i].setCardForObject = NormalCards[ran];
                }
                else if (cardRarityRan > chanceForNormalThreshold && cardRarityRan <= chanceForRareThreshold)
                {
                    ran = Random.Range(0, RareCards.Count);
                    ThemePackCards[i].setCardForObject = RareCards[ran];
                }
                else if (cardRarityRan > chanceForRareThreshold && cardRarityRan <= chanceForSRThreshold)
                {
                    ran = Random.Range(0, SRCards.Count);
                    ThemePackCards[i].setCardForObject = SRCards[ran];
                    ThemePackCards[i].SRAnim = true;
                    if (SRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SRCards[ran].secretPackToUnlock[j]);
                        }

                    }

                }
                else
                {
                    ran = Random.Range(0, SSRCards.Count);
                    ThemePackCards[i].setCardForObject = SSRCards[ran];
                    ThemePackCards[i].URAnim = true;
                    if (SSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SSRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[i].setCardForObject);
            }
            if ((currentPack + 1) % 10 == 0)
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= 65)
                {
                    ran = Random.Range(0, SRCards.Count);
                    ThemePackCards[9].setCardForObject = SRCards[ran];
                    ThemePackCards[9].SRAnim = true;
                    if (SRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                else if (cardRarityRan > 65 && cardRarityRan <= 100)
                {
                    ran = Random.Range(0, SSRCards.Count);
                    ThemePackCards[9].setCardForObject = SSRCards[ran];
                    ThemePackCards[9].URAnim = true;
                    if (SSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SSRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[9].setCardForObject);
            }
            else
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= chanceForNormalThreshold)
                {
                    ran = Random.Range(0, NormalCards.Count);
                    ThemePackCards[9].setCardForObject = NormalCards[ran];
                }
                else if (cardRarityRan > chanceForNormalThreshold && cardRarityRan <= chanceForRareThreshold)
                {
                    ran = Random.Range(0, RareCards.Count);
                    ThemePackCards[9].setCardForObject = RareCards[ran];
                }
                else if (cardRarityRan > chanceForRareThreshold && cardRarityRan <= chanceForSRThreshold)
                {
                    ran = Random.Range(0, SRCards.Count);
                    ThemePackCards[9].setCardForObject = SRCards[ran];
                    ThemePackCards[9].SRAnim = true;
                    if (SRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                else
                {
                    ran = Random.Range(0, SSRCards.Count);
                    ThemePackCards[9].setCardForObject = SSRCards[ran];
                    ThemePackCards[9].URAnim = true;
                    if (SSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SSRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[9].setCardForObject);
            }
        }
        else
        {
            // Pulling the first half of the pack (This is the Complete pack side.)
            for (int i = 0; i < 5; i++)
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= chanceForNormalThreshold)
                {
                    ran = Random.Range(0, NormalCards.Count);
                    ThemePackCards[i].setCardForObject = NormalCards[ran];
                }
                else if (cardRarityRan > chanceForNormalThreshold && cardRarityRan <= chanceForRareThreshold)
                {
                    ran = Random.Range(0, RareCards.Count);
                    ThemePackCards[i].setCardForObject = RareCards[ran];
                }
                else if (cardRarityRan > chanceForRareThreshold && cardRarityRan <= chanceForSRThreshold)
                {
                    ran = Random.Range(0, SRCards.Count);
                    ThemePackCards[i].setCardForObject = SRCards[ran];
                    ThemePackCards[i].SRAnim = true;
                    if (SRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                else
                {
                    ran = Random.Range(0, SSRCards.Count);
                    ThemePackCards[i].setCardForObject = SSRCards[ran];
                    ThemePackCards[i].URAnim = true;
                    if (SSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SSRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[i].setCardForObject);
            }
            // Pulling the second half of the pack (Which is the Theme pack side.)
            for (int i = 5; i < 9; i++)
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= chanceForNormalThreshold)
                {
                    ran = Random.Range(0, NormalCards.Count);
                    ThemePackCards[i].setCardForObject = NormalCards[ran];
                }
                else if (cardRarityRan > chanceForNormalThreshold && cardRarityRan <= chanceForRareThreshold)
                {
                    ran = Random.Range(0, RareCards.Count);
                    ThemePackCards[i].setCardForObject = RareCards[ran];
                }
                else if (cardRarityRan > chanceForRareThreshold && cardRarityRan <= chanceForSRThreshold)
                {
                    ran = Random.Range(0, SRCards.Count);
                    ThemePackCards[i].setCardForObject = SRCards[ran];
                    ThemePackCards[i].SRAnim = true;
                    if (SRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                else
                {
                    ran = Random.Range(0, SSRCards.Count);
                    ThemePackCards[i].setCardForObject = SSRCards[ran];
                    ThemePackCards[i].URAnim = true;
                    if (SSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SSRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[i].setCardForObject);
            }
            if((currentPack + 1) % 10 == 0)
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= 65)
                {
                    ran = Random.Range(0, SRCards.Count);
                    ThemePackCards[9].setCardForObject = SRCards[ran];
                    ThemePackCards[9].SRAnim = true;
                    if (SRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                else if (cardRarityRan > 65 && cardRarityRan <= 100)
                {
                    ran = Random.Range(0, SSRCards.Count);
                    ThemePackCards[9].setCardForObject = SSRCards[ran];
                    ThemePackCards[9].URAnim = true;
                    if (SSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SSRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[9].setCardForObject);
            }
            else
            {
                cardRarityRan = Random.Range(0, 100);
                if (cardRarityRan >= 0 && cardRarityRan <= chanceForNormalThreshold)
                {
                    ran = Random.Range(0, NormalCards.Count);
                    ThemePackCards[9].setCardForObject = NormalCards[ran];
                }
                else if (cardRarityRan > chanceForNormalThreshold && cardRarityRan <= chanceForRareThreshold)
                {
                    ran = Random.Range(0, RareCards.Count);
                    ThemePackCards[9].setCardForObject = RareCards[ran];
                }
                else if (cardRarityRan > chanceForRareThreshold && cardRarityRan <= chanceForSRThreshold)
                {
                    ran = Random.Range(0, SRCards.Count);
                    ThemePackCards[9].setCardForObject = SRCards[ran];
                    ThemePackCards[9].SRAnim = true;
                    if (SRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                else
                {
                    ran = Random.Range(0, SSRCards.Count);
                    ThemePackCards[9].setCardForObject = SSRCards[ran];
                    ThemePackCards[9].URAnim = true;
                    if (SSRCards[ran].secretPackToUnlock.Count > 0)
                    {
                        for (int j = 0; j < SSRCards[ran].secretPackToUnlock.Count; j++)
                        {
                            ThemePacksUnlocked.Add(SSRCards[ran].secretPackToUnlock[j]);
                        }
                    }
                }
                CardsPulledThisOpening.Add(ThemePackCards[9].setCardForObject);
            }
        }
        
    }


    public void GenerateTheLastPacks()
    {
        for (int i = currentPack + 1; i < totalPacksBeingOpened; i++)
        {
            PullThemePack();
        }
    }
    public void SetupCardsPulled()
    {
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.name).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.color1).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.cardCatagory).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderByDescending(card => card.secretPackRarity).ToList();
        
        for (int i = 0; i < CardsPulledThisOpening.Count; i++)
        {
            CardsPulledObjects[i].gameObject.SetActive(true);
            CardsPulledObjects[i].currentCard = CardsPulledThisOpening[i];
            CardsPulledObjects[i].currentCard.amountOwned += 1;
#if UNITY_EDITOR
            EditorUtility.SetDirty(CardsPulledObjects[i].currentCard);
#endif
            CardsPulledObjects[i].SetCardData();
        }

    }
    public void SetupThemePacksUnlocked()
    {
        ThemePacksUnlocked = ThemePacksUnlocked.Distinct().ToList();
        if (ThemePacksUnlocked.Count <= 20)
        {
            for (int i = 0; i < ThemePacksUnlocked.Count; i++)
            {
                ThemePackButtons[i].themePackFile = ThemePacksUnlocked[i];
                ThemePacksUnlocked[i].hasBeenUnlocked = true;
                ThemePackButtons[i].SetPackData();
                
                ThemePackButtons[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                ThemePackButtons[i].themePackFile = ThemePacksUnlocked[i];
                ThemePacksUnlocked[i].hasBeenUnlocked = true;
                ThemePackButtons[i].SetPackData();
                ThemePackButtons[i].gameObject.SetActive(true);
            }
        }
    }
}

