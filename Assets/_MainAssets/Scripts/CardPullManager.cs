using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEditor;

public class CardPullManager : MonoBehaviour
{
    public enum CardRarity
    {
        Common,
        Uncommon,
        Rare,
        SuperRare,
        SecretRare,
        Promo,
        LimitedPack,
        AlternateArt
    }
    [System.Serializable]
    public class SecretRareCard
    {
        public CardVariable CardVar;
        public bool altArtVarient = false;
    }
    [System.Serializable]
    public class GeneratedPack
    {
        public List<CardVariable> CardsInThePack = new List<CardVariable>();
        public int Rarity = 0;
        public bool hasAltArt = false;
    }
    [Header("Main Pack Variables")]
    public GlobalSetPullingFrom SetData;
    //public CardSet SetPullingFrom;
    public List<CardVariable> CommonCards = new List<CardVariable>();
    public List<CardVariable> UncommonCards = new List<CardVariable>();
    public List<CardVariable> RareCards = new List<CardVariable>();
    public List<CardVariable> SuperRareCards = new List<CardVariable>();
    public List<CardVariable> SecretRareCards = new List<CardVariable>();
    public List<CardVariable> AlternateArtCards = new List<CardVariable>();
    public List<CardVariable> PromoCards = new List<CardVariable>();

    public GameObject StandardPackObject;
    public RevealCardObject[] StandardPackCards;
    public TextMeshProUGUI currentPackOpeningText;

    public GameObject StandardJPPackObject;
    public RevealCardObject[] StandardJPPackCards;

    [Header("Booster Box Variables")]
    public List<GeneratedPack> packsInBox = new List<GeneratedPack>();

    public List<CardVariable> AlternateArtsPulledThisOpening = new();
    public List<CardVariable> CardsPulledThisOpening = new();
    public List<CardInCollection> CardsPulledObjects = new();

    public float timeBetweenReveals = 0.8f;
    public int currentPack = 0;
    public int totalPacksBeingOpened = 24;
    public int amountOfAltsPulled = 0;

    public bool hasPulledSecretRare = false;


    public GameObject cardPullingMainObject;
    public GameObject FinishedPullingButton;
    public GameObject PullNextPackButton;

    public int cardsRevealed = 0;
    bool startedOpeningAnimation = false;
    void Awake()
    {
        SeperateSetIntoRarities();
        
        if (SetData.openingFullBox)
        {
            totalPacksBeingOpened = 24;
            currentPackOpeningText.text = "Pack " + ((currentPack + 1).ToString() + " / " + packsInBox.Count.ToString());
        }
        else
        {
            totalPacksBeingOpened = SetData.amountOfPacksPulled;
            currentPackOpeningText.text = "Pack " + ((currentPack + 1).ToString() + " / " + totalPacksBeingOpened.ToString());
        }
            
        if (!SetData.openingJPPacks)
        {
            StandardPackObject.SetActive(true);
            StandardJPPackObject.SetActive(false);
            if (!SetData.openingFullBox)
                PullStandardPack();
            else
            {
                //totalPacksBeingOpened = packsInBox.Count;
                SetupStandardBox();
                PullStandardPackFromBox();
            }

        }
        else if (SetData.openingJPPacks)
        {
            StandardJPPackObject.SetActive(true);
            StandardPackObject.SetActive(false);
            if (!SetData.openingFullBox)
                PullStandardJPPack();
            else
            {
                //totalPacksBeingOpened = packsInBox.Count;
                SetupJPBox();
                PullStandardJPPackFromBox();

            }
        }
        

    }

    public void PullPackStandardMethod()
    {
        if (!startedOpeningAnimation)
        {
            if (SetData.openingJPPacks)
                StartCoroutine(PullStandardJPPackAuto());
            else
                StartCoroutine(PullStandardPackAuto());
            startedOpeningAnimation = true;
        }
        
    }

    private IEnumerator PullStandardPackAuto()
    {
        for (int i = 0; i < StandardPackCards.Length; i++)
        {
            //StandardPackCards[i].RevealCardMethod();
            if (!StandardPackCards[i].hasBeenRevealed)
            {
                if (StandardPackCards[i].URAnim)
                    StandardPackCards[i].GetComponent<Animator>().Play("CardPullUR");
                else if (StandardPackCards[i].SRAnim)
                    StandardPackCards[i].GetComponent<Animator>().Play("CardPullSR");
                else
                    StandardPackCards[i].GetComponent<Animator>().Play("CardPullNormal");
                StandardPackCards[i].SRAnim = false;
                StandardPackCards[i].URAnim = false;
                yield return new WaitForSeconds(timeBetweenReveals);
            }   
        }
        /*if (currentPack + 1 == totalPacksBeingOpened)
            FinishedPullingButton.SetActive(true);
        else
            PullNextPackButton.SetActive(true);
        */

    }
    private IEnumerator PullStandardJPPackAuto()
    {
        for (int i = 0; i < StandardJPPackCards.Length; i++)
        {
            //StandardPackCards[i].RevealCardMethod();
            if (!StandardJPPackCards[i].hasBeenRevealed)
            {
                if (StandardJPPackCards[i].URAnim)
                    StandardJPPackCards[i].GetComponent<Animator>().Play("CardPullUR");
                else if (StandardJPPackCards[i].SRAnim)
                    StandardJPPackCards[i].GetComponent<Animator>().Play("CardPullSR");
                else
                    StandardJPPackCards[i].GetComponent<Animator>().Play("CardPullNormal");
                StandardJPPackCards[i].SRAnim = false;
                StandardJPPackCards[i].URAnim = false;
                yield return new WaitForSeconds(timeBetweenReveals);
            }
        }
        /*if (currentPack + 1 == totalPacksBeingOpened)
            FinishedPullingButton.SetActive(true);
        else
            PullNextPackButton.SetActive(true);
        */
    }

    public void SetupNextPack()
    {
        if(currentPack < totalPacksBeingOpened)
        {
            if (SetData.openingJPPacks)
            {
                for (int i = 0; i < StandardJPPackCards.Length; i++)
                {
                    StandardJPPackCards[i].ResetCard();
                }
            }
            else
            {
                for (int i = 0; i < StandardPackCards.Length; i++)
                {
                    StandardPackCards[i].ResetCard();
                }
            }
            
            currentPack += 1;
            currentPackOpeningText.text = "Pack " + ((currentPack + 1).ToString() + " / " + totalPacksBeingOpened.ToString());
            if (!SetData.openingFullBox)
            {
                if (SetData.openingJPPacks)
                    PullStandardJPPack();
                else
                    PullStandardPack();
            }
                
            else
            {
                if(SetData.openingJPPacks)
                    PullStandardJPPackFromBox();
                else
                    PullStandardPackFromBox();
            }
            cardsRevealed = 0;
            
        }
        
    }
    public void SetupPackAnimation()
    {
        StartCoroutine(PackSwapAnimation());
    }
    public IEnumerator PackSwapAnimation()
    {
        cardPullingMainObject.GetComponent<Animator>().Play("MenuFadeOut", 0, 0);
        yield return new WaitForSeconds(0.3f);
        SetupNextPack();
        yield return new WaitForSeconds(0.2f);
        cardPullingMainObject.GetComponent<Animator>().Play("MenuFadeIn", 0, 0);
        yield return new WaitForSeconds(0.2f);
        startedOpeningAnimation = false;
    }
    public void IncreaseCardsPulled()
    {
        cardsRevealed += 1;
        if (SetData.openingJPPacks)
        {
            if (cardsRevealed == 6)
            {
                if (currentPack + 1 == totalPacksBeingOpened)
                    FinishedPullingButton.SetActive(true);
                else
                    PullNextPackButton.SetActive(true);
            }
        }
        else
        {
            if (cardsRevealed == 12)
            {
                if (currentPack + 1 == totalPacksBeingOpened)
                    FinishedPullingButton.SetActive(true);
                else
                    PullNextPackButton.SetActive(true);
            }
        }
        
    }
    public void RestockBasicCards()
    {
        CommonCards.Clear();
        UncommonCards.Clear();
        RareCards.Clear();
        for (int i = 0; i < SetData.SetPullingFrom.ListOfCardsInSet.Count; i++)
        {
            switch (SetData.SetPullingFrom.ListOfCardsInSet[i].rarity)
            {
                case CardVariable.CardRarity.Common:
                    CommonCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.CardRarity.Uncommon:
                    UncommonCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.CardRarity.Rare:
                    RareCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                default:
                    break;
            }


        }
    }
    public void SeperateSetIntoRarities()
    {
        for (int i = 0; i < SetData.SetPullingFrom.ListOfCardsInSet.Count; i++)
        {
            switch (SetData.SetPullingFrom.ListOfCardsInSet[i].rarity)
            {
                case CardVariable.CardRarity.Common:
                    CommonCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.CardRarity.Uncommon:
                    UncommonCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.CardRarity.Rare:
                    RareCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.CardRarity.SuperRare:
                    SuperRareCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.CardRarity.SecretRare:
                    SecretRareCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                case CardVariable.CardRarity.Promo:
                    PromoCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
                    break;
                default:
                    break;
            }
            if (SetData.SetPullingFrom.ListOfCardsInSet[i].hasAnAltArt)
            {
                AlternateArtCards.Add(SetData.SetPullingFrom.ListOfCardsInSet[i]);
            }


        }
    }
    public void PullStandardPack()
    {
        int ran = 0;
        for (int i = 0; i < 7; i++)
        {
            ran = Random.Range(0, CommonCards.Count);
            StandardPackCards[i].setCardForObject = CommonCards[ran];
            CardsPulledThisOpening.Add(CommonCards[ran]);
            CommonCards.RemoveAt(ran);
        }
        for (int i = 7; i < 10; i++)
        {
            ran = Random.Range(0, UncommonCards.Count);
            StandardPackCards[i].setCardForObject = UncommonCards[ran];
            CardsPulledThisOpening.Add(UncommonCards[ran]);
            UncommonCards.RemoveAt(ran);
        }
        ran = Random.Range(0, RareCards.Count);
        StandardPackCards[10].setCardForObject = RareCards[ran];
        CardsPulledThisOpening.Add(RareCards[ran]);
        RareCards.RemoveAt(ran);
        int randomChance = Random.Range(0, 100);
        if (randomChance > 92)
        {
            /*ran = Random.Range(0, SecretRareCards.Count);
            StandardPackCards[11].setCardForObject = SecretRareCards[ran];
            CardsPulledThisOpening.Add(SecretRareCards[ran]);
            StandardPackCards[11].URAnim = true;
            if(SecretRareCards[ran].hasAnAltArt)
                StandardPackCards[11].altArtCard = true;
            else
                StandardPackCards[11].altArtCard = false;
            */
            int ran2 = Random.Range(0, 100);
            if (ran2 < 70)
            {
                ran = Random.Range(0, SecretRareCards.Count);
                StandardPackCards[11].setCardForObject = SecretRareCards[ran];
                CardsPulledThisOpening.Add(SecretRareCards[ran]);
                StandardPackCards[11].URAnim = true;
                StandardPackCards[11].altArtCard = false;
            }
            else
            {
                ran = Random.Range(0, AlternateArtCards.Count);
                StandardPackCards[11].setCardForObject = AlternateArtCards[ran];
                AlternateArtsPulledThisOpening.Add(AlternateArtCards[ran]);
                StandardPackCards[11].URAnim = true;
                StandardPackCards[11].altArtCard = true;
            }

        }
        else if (randomChance > 65)
        {
            ran = Random.Range(0, SuperRareCards.Count);
            StandardPackCards[11].setCardForObject = SuperRareCards[ran];
            CardsPulledThisOpening.Add(SuperRareCards[ran]);
            StandardPackCards[11].SRAnim = true;
            StandardPackCards[11].altArtCard = false;
        }
        else
        {
            if(PromoCards.Count > 0)
            {
                ran = Random.Range(0, 100);
                if(ran <= 50)
                {
                    ran = Random.Range(0, PromoCards.Count);
                    StandardPackCards[11].setCardForObject = PromoCards[ran];
                    CardsPulledThisOpening.Add(PromoCards[ran]);
                    StandardPackCards[11].altArtCard = false;
                }
                else
                {
                    ran = Random.Range(0, RareCards.Count);
                    StandardPackCards[11].setCardForObject = RareCards[ran];
                    CardsPulledThisOpening.Add(RareCards[ran]);
                    StandardPackCards[11].altArtCard = false;
                }
            }
            else
            {
                ran = Random.Range(0, RareCards.Count);
                StandardPackCards[11].setCardForObject = RareCards[ran];
                CardsPulledThisOpening.Add(RareCards[ran]);
                StandardPackCards[11].altArtCard = false;
            }
            
        }
        RestockBasicCards();
    }
    public void PullStandardJPPack()
    {
        int ran = 0;
        for (int i = 0; i < 3; i++)
        {
            ran = Random.Range(0, CommonCards.Count);
            StandardJPPackCards[i].setCardForObject = CommonCards[ran];
            CardsPulledThisOpening.Add(CommonCards[ran]);
            CommonCards.RemoveAt(ran);
        }
        for (int i = 3; i < 5; i++)
        {
            ran = Random.Range(0, UncommonCards.Count);
            StandardJPPackCards[i].setCardForObject = UncommonCards[ran];
            CardsPulledThisOpening.Add(UncommonCards[ran]);
            UncommonCards.RemoveAt(ran);
        }
        int randomChance = Random.Range(0, 100);
        if (randomChance > 92)
        {
            ran = Random.Range(0, 100);
            if (ran < 70)
            {
                ran = Random.Range(0, SecretRareCards.Count);
                StandardJPPackCards[5].setCardForObject = SecretRareCards[ran];
                CardsPulledThisOpening.Add(SecretRareCards[ran]);
                StandardJPPackCards[5].URAnim = true;
                StandardJPPackCards[5].altArtCard = false;
            }
            else
            {
                ran = Random.Range(0, AlternateArtCards.Count);
                StandardJPPackCards[5].setCardForObject = AlternateArtCards[ran];
                AlternateArtsPulledThisOpening.Add(AlternateArtCards[ran]);
                StandardJPPackCards[5].URAnim = true;
                StandardJPPackCards[5].altArtCard = true;
            }
        }

        else if (randomChance > 65)
        {
            ran = Random.Range(0, SuperRareCards.Count);
            StandardJPPackCards[5].setCardForObject = SuperRareCards[ran];
            CardsPulledThisOpening.Add(SuperRareCards[ran]);
            StandardJPPackCards[5].SRAnim = true;
            StandardJPPackCards[5].altArtCard = false;
        }
        else
        {
            if (PromoCards.Count > 0)
            {
                ran = Random.Range(0, 100);
                if (ran <= 50)
                {
                    ran = Random.Range(0, PromoCards.Count);
                    StandardJPPackCards[5].setCardForObject = PromoCards[Random.Range(0, PromoCards.Count)];
                    CardsPulledThisOpening.Add(PromoCards[ran]);
                    StandardJPPackCards[5].altArtCard = false;
                }
                else
                {
                    ran = Random.Range(0, RareCards.Count);
                    StandardJPPackCards[5].setCardForObject = RareCards[Random.Range(0, RareCards.Count)];
                    CardsPulledThisOpening.Add(RareCards[ran]);
                    StandardJPPackCards[5].altArtCard = false;
                }
            }
            else
            {
                ran = Random.Range(0, RareCards.Count);
                StandardJPPackCards[5].setCardForObject = RareCards[Random.Range(0, RareCards.Count)];
                CardsPulledThisOpening.Add(RareCards[ran]);
                StandardJPPackCards[5].altArtCard = false;
            }
               
        }
           

        RestockBasicCards();
    }
    public void PullStandardPackFromBox()
    {
        StandardPackCards[0].setCardForObject = packsInBox[currentPack].CardsInThePack[0];
        StandardPackCards[1].setCardForObject = packsInBox[currentPack].CardsInThePack[1];
        StandardPackCards[2].setCardForObject = packsInBox[currentPack].CardsInThePack[2];
        StandardPackCards[3].setCardForObject = packsInBox[currentPack].CardsInThePack[3];
        StandardPackCards[4].setCardForObject = packsInBox[currentPack].CardsInThePack[4];
        StandardPackCards[5].setCardForObject = packsInBox[currentPack].CardsInThePack[5];

        StandardPackCards[6].setCardForObject = packsInBox[currentPack].CardsInThePack[6];
        StandardPackCards[7].setCardForObject = packsInBox[currentPack].CardsInThePack[7];
        StandardPackCards[8].setCardForObject = packsInBox[currentPack].CardsInThePack[8];
        StandardPackCards[9].setCardForObject = packsInBox[currentPack].CardsInThePack[9];

        StandardPackCards[10].setCardForObject = packsInBox[currentPack].CardsInThePack[10];
        if(packsInBox[currentPack].Rarity == 2)
            StandardPackCards[11].URAnim = true;
        else if(packsInBox[currentPack].Rarity == 1)
            StandardPackCards[11].SRAnim = true;
        if (packsInBox[currentPack].hasAltArt)
            StandardPackCards[11].altArtCard = true;
        else
            StandardPackCards[11].altArtCard = false;
        StandardPackCards[11].setCardForObject = packsInBox[currentPack].CardsInThePack[11];
    }
    public void PullStandardJPPackFromBox()
    {
        StandardJPPackCards[0].setCardForObject = packsInBox[currentPack].CardsInThePack[0];
        StandardJPPackCards[1].setCardForObject = packsInBox[currentPack].CardsInThePack[1];
        StandardJPPackCards[2].setCardForObject = packsInBox[currentPack].CardsInThePack[2];
        StandardJPPackCards[3].setCardForObject = packsInBox[currentPack].CardsInThePack[3];
        StandardJPPackCards[4].setCardForObject = packsInBox[currentPack].CardsInThePack[4];
        if (packsInBox[currentPack].Rarity == 2)
            StandardJPPackCards[5].URAnim = true;
        else if (packsInBox[currentPack].Rarity == 1)
            StandardJPPackCards[5].SRAnim = true;
        if (packsInBox[currentPack].hasAltArt)
            StandardJPPackCards[5].altArtCard = true;
        else
            StandardJPPackCards[5].altArtCard = false;
        StandardJPPackCards[5].setCardForObject = packsInBox[currentPack].CardsInThePack[5];
    }
    public void SetupCardInBox(GeneratedPack packToPopulate, int rarityToAdd)
    {
        int ran = 0;
        for (int i = 0; i < 7; i++)
        {
            ran = Random.Range(0, CommonCards.Count);
            packToPopulate.CardsInThePack[i] = CommonCards[ran];
            CardsPulledThisOpening.Add(CommonCards[ran]);
            CommonCards.RemoveAt(ran);
        }
        for (int i = 7; i < 10; i++)
        {
            ran = Random.Range(0, UncommonCards.Count);
            packToPopulate.CardsInThePack[i] = UncommonCards[ran];
            CardsPulledThisOpening.Add(UncommonCards[ran]);
            UncommonCards.RemoveAt(ran);
        }
        if (PromoCards.Count > 0)
        {
            ran = Random.Range(0, 100);
            if (ran <= 50)
            {
                ran = Random.Range(0, PromoCards.Count);
                packToPopulate.CardsInThePack[10] = PromoCards[ran];
                CardsPulledThisOpening.Add(PromoCards[ran]);
            }
            else
            {
                ran = Random.Range(0, RareCards.Count);
                packToPopulate.CardsInThePack[10] = RareCards[ran];
                CardsPulledThisOpening.Add(RareCards[ran]);
                RareCards.RemoveAt(ran);
            }
        }
        else
        {
            ran = Random.Range(0, RareCards.Count);
            packToPopulate.CardsInThePack[10] = RareCards[ran];
            CardsPulledThisOpening.Add(RareCards[ran]);
            RareCards.RemoveAt(ran);
        }
            

        // Add the hit to the pack
        if (rarityToAdd == 0)
        {
            if (!hasPulledSecretRare)
            {
                ran = Random.Range(0, 100);
                if (ran < 50)
                {
                    ran = Random.Range(0, SecretRareCards.Count);
                    packToPopulate.CardsInThePack[11] = SecretRareCards[ran];
                    packToPopulate.Rarity = 2;
                    packToPopulate.hasAltArt = false;
                    CardsPulledThisOpening.Add(SecretRareCards[ran]);
                    SecretRareCards.RemoveAt(ran);
                    hasPulledSecretRare = true;
                }
                else
                {
                    ran = Random.Range(0, AlternateArtCards.Count);
                    packToPopulate.CardsInThePack[11] = AlternateArtCards[ran];
                    packToPopulate.Rarity = 2;
                    packToPopulate.hasAltArt = true;
                    AlternateArtsPulledThisOpening.Add(AlternateArtCards[ran]);
                    AlternateArtCards.RemoveAt(ran);
                }
            }
            else
            {
                ran = Random.Range(0, AlternateArtCards.Count);
                packToPopulate.CardsInThePack[11] = AlternateArtCards[ran];
                packToPopulate.Rarity = 2;
                packToPopulate.hasAltArt = true;
                AlternateArtsPulledThisOpening.Add(AlternateArtCards[ran]);
                AlternateArtCards.RemoveAt(ran);
            }
        }
        else if (rarityToAdd == 1)
        {
            ran = Random.Range(0, SuperRareCards.Count);
            packToPopulate.CardsInThePack[11] = SuperRareCards[ran];
            packToPopulate.Rarity = 1;
            CardsPulledThisOpening.Add(SuperRareCards[ran]);
            SuperRareCards.RemoveAt(ran);
        }
        else
        {
            ran = Random.Range(0, RareCards.Count);
            packToPopulate.CardsInThePack[11] = RareCards[ran];
            CardsPulledThisOpening.Add(RareCards[ran]);
            RareCards.RemoveAt(ran);
        }
        RestockBasicCards();
        //ran = Random.Range(0, CommonCards.Count);
        //packToPopulate.CardsInThePack[1] = CommonCards[ran];
        //ran = Random.Range(0, CommonCards.Count);
        //packToPopulate.CardsInThePack[2] = CommonCards[ran];
        //ran = Random.Range(0, CommonCards.Count);
        //packToPopulate.CardsInThePack[3] = CommonCards[ran];
        //ran = Random.Range(0, CommonCards.Count);
        //packToPopulate.CardsInThePack[4] = CommonCards[ran];
        //ran = Random.Range(0, CommonCards.Count);
        //packToPopulate.CardsInThePack[5] = CommonCards[ran];

        //packToPopulate.CardsInThePack[6] = UncommonCards[Random.Range(0, UncommonCards.Count)];
        //packToPopulate.CardsInThePack[7] = UncommonCards[Random.Range(0, UncommonCards.Count)];
        //packToPopulate.CardsInThePack[8] = UncommonCards[Random.Range(0, UncommonCards.Count)];
        //packToPopulate.CardsInThePack[9] = UncommonCards[Random.Range(0, UncommonCards.Count)];
    }
    public void SetupJPCardInBox(GeneratedPack packToPopulate, int rarityToAdd)
    {
        int ran = 0;
        for (int i = 0; i < 3; i++)
        {
            ran = Random.Range(0, CommonCards.Count);
            packToPopulate.CardsInThePack[i] = CommonCards[ran];
            CardsPulledThisOpening.Add(CommonCards[ran]);
            CommonCards.RemoveAt(ran);
        }
        for (int i = 3; i < 5; i++)
        {
            ran = Random.Range(0, UncommonCards.Count);
            packToPopulate.CardsInThePack[i] = UncommonCards[ran];
            CardsPulledThisOpening.Add(UncommonCards[ran]);
            UncommonCards.RemoveAt(ran);
        }
        // Add the hit to the pack
        if (rarityToAdd == 0)
        {
            if (!hasPulledSecretRare)
            {
                ran = Random.Range(0, 100);
                if (ran < 50)
                {
                    ran = Random.Range(0, SecretRareCards.Count);
                    packToPopulate.CardsInThePack[5] = SecretRareCards[ran];
                    packToPopulate.Rarity = 2;
                    packToPopulate.hasAltArt = false;
                    CardsPulledThisOpening.Add(SecretRareCards[ran]);
                    SecretRareCards.RemoveAt(ran);
                    hasPulledSecretRare = true;
                }
                else
                {
                    ran = Random.Range(0, AlternateArtCards.Count);
                    packToPopulate.CardsInThePack[5] = AlternateArtCards[ran];
                    packToPopulate.Rarity = 2;
                    packToPopulate.hasAltArt = true;
                    AlternateArtsPulledThisOpening.Add(AlternateArtCards[ran]);
                    AlternateArtCards.RemoveAt(ran);
                }
            }
            else
            {
                ran = Random.Range(0, AlternateArtCards.Count);
                packToPopulate.CardsInThePack[5] = AlternateArtCards[ran];
                packToPopulate.Rarity = 2;
                packToPopulate.hasAltArt = true;
                AlternateArtsPulledThisOpening.Add(AlternateArtCards[ran]);
                AlternateArtCards.RemoveAt(ran);
            }
        }
        else if (rarityToAdd == 1)
        {
            ran = Random.Range(0, SuperRareCards.Count);
            packToPopulate.CardsInThePack[5] = SuperRareCards[ran];
            packToPopulate.Rarity = 1;
            CardsPulledThisOpening.Add(SuperRareCards[ran]);
            SuperRareCards.RemoveAt(ran);
        }
        else
        {
            if (PromoCards.Count > 0)
            {
                ran = Random.Range(0, 100);
                if (ran <= 50)
                {
                    ran = Random.Range(0, PromoCards.Count);
                    packToPopulate.CardsInThePack[5] = PromoCards[ran];
                    CardsPulledThisOpening.Add(PromoCards[ran]);
                }
                else
                {
                    ran = Random.Range(0, RareCards.Count);
                    packToPopulate.CardsInThePack[5] = RareCards[ran];
                    CardsPulledThisOpening.Add(RareCards[ran]);
                    RareCards.RemoveAt(ran);
                }
            }
            else
            {
                ran = Random.Range(0, RareCards.Count);
                packToPopulate.CardsInThePack[5] = RareCards[ran];
                CardsPulledThisOpening.Add(RareCards[ran]);
                RareCards.RemoveAt(ran);
            }
            
        }
        RestockBasicCards();
    }
    public void SetupStandardBox()
    {
        // Adding every card into the box.
        for (int i = 0; i < packsInBox.Count; i++)
        {
            if(i >= 0 && i < SetData.amountOfSecsInBox)
                SetupCardInBox(packsInBox[i], 0);
            else if(i >= SetData.amountOfSecsInBox && i < (SetData.amountOfSRsInBox + SetData.amountOfSecsInBox))
                SetupCardInBox(packsInBox[i], 1);
            else
                SetupCardInBox(packsInBox[i], 2);
        }
        IListExtensions.Shuffle(packsInBox);
    }
    public void SetupJPBox()
    {
        // Adding every card into the box.
        for (int i = 0; i < packsInBox.Count; i++)
        {
            if (i >= 0 && i < SetData.amountOfSecsInBox)
                SetupJPCardInBox(packsInBox[i], 0);
            else if (i >= SetData.amountOfSecsInBox && i < (SetData.amountOfSRsInBox + SetData.amountOfSecsInBox))
                SetupJPCardInBox(packsInBox[i], 1);
            else
                SetupJPCardInBox(packsInBox[i], 2);
        }
        IListExtensions.Shuffle(packsInBox);
    }

    public void GenerateTheLastPacks()
    {
        if(!SetData.openingFullBox)
        {
            for (int i = currentPack + 1; i < totalPacksBeingOpened; i++)
            {
                Debug.Log(i);
                if (SetData.openingJPPacks)
                    PullStandardJPPack();
                else
                    PullStandardPack();
            }
        }
    }

    public void SetupCardsPulled()
    {
        

        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.name).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.color1).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderBy(card => card.cardCatagory).ToList();
        CardsPulledThisOpening = CardsPulledThisOpening.OrderByDescending(card => card.rarity).ToList();

        for (int i = 0; i < AlternateArtsPulledThisOpening.Count; i++)
        {
            CardsPulledThisOpening.Insert(0, AlternateArtsPulledThisOpening[i]);
            amountOfAltsPulled += 1;
        }

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
            if (i < amountOfAltsPulled)
                CardsPulledObjects[i].containsAltArt = true;
            CardsPulledObjects[i].SetCardData();
        }

    }

}
public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
