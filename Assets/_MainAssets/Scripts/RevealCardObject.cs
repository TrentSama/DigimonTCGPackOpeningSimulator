using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.IO;
using System.Threading.Tasks;

public class RevealCardObject : MonoBehaviour
{
    public Image CardImage;
    public CardVariable defaultCard;
    public CardVariable setCardForObject;
    public Image cardMask;

    public Image rarityImage;
    public Image secretPackRarityImage;
    public Sprite[] raritySprites;
    public Sprite[] secretPackRaritySprites;
    public bool revealedFromSecretPack = false;
    public bool SRAnim = false;
    public bool URAnim = false;
    public bool altArtCard = false;

    public bool hasBeenRevealed = false;
    public bool holdingDownMouse = false;

    public GameEvent increaseCardsPulled;

    public GameObject AltArtObject;

    private Animator anim;

    public AudioSource regularRevealSound;
    public AudioSource specialRevealSound;
    public AudioSource specialRevealPart2Sound;
    public AudioSource ultraRevealSound;
    public AudioSource ultraRevealPart2Sound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //CardImage.sprite = await ImageGetData.GetCardImageFromFile("CardBack");
        //cardMask.sprite = await ImageGetData.GetCardImageFromFile("CardBack");
        CardImage.sprite = defaultCard.cardImage;
        cardMask.sprite = defaultCard.cardImage;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            holdingDownMouse = true;
        if (Input.GetMouseButtonUp(0))
            holdingDownMouse = false;
    }

    public void RevealCardMethod()
    {
        CardImage.sprite = setCardForObject.cardImage;
        if (altArtCard)
            AltArtObject.SetActive(true);
        if (revealedFromSecretPack)
        {
            secretPackRarityImage.sprite = secretPackRaritySprites[(int)setCardForObject.secretPackRarity];
            secretPackRarityImage.gameObject.SetActive(true);
        }
        else
        {
            rarityImage.sprite = raritySprites[(int)setCardForObject.rarity];
            rarityImage.gameObject.SetActive(true);
        }
        if(SRAnim)
            specialRevealSound.Play();
        else if(URAnim)
            ultraRevealSound.Play();
        else
            regularRevealSound.Play();
        hasBeenRevealed = true;
    }
    public void SaveScriptableObject()
    {
        /*if(!altArtCard)
            setCardForObject.amountOwned += 1;
        else
            setCardForObject.amountOwned += 1;
#if UNITY_EDITOR
        EditorUtility.SetDirty(setCardForObject);
#endif
        */
    }
    public void NormalPullSound()
    {
        regularRevealSound.Play();
    }
    public void SpecialPullSound()
    {
        specialRevealSound.Play();
    }
    public void SpecialPullPart2Sound()
    {
        specialRevealPart2Sound.Play();
    }
    public void UltraPullSound()
    {
        ultraRevealSound.Play();
    }
    public void UltraPullPart2Sound()
    {
        ultraRevealPart2Sound.Play();
    }
    
    
   
    public void ResetCard()
    {
        rarityImage.gameObject.SetActive(false);
        secretPackRarityImage.gameObject.SetActive(false);
        AltArtObject.SetActive(false);
        CardImage.sprite = defaultCard.cardImage;
        hasBeenRevealed = false;
        setCardForObject = null;

    }

    public void RevealCardFromMouse()
    {
        if (holdingDownMouse && !hasBeenRevealed)
        {
            if (revealedFromSecretPack)
            {
                if (setCardForObject.secretPackRarity == CardVariable.SecretPackCardRarity.UltraRare || altArtCard)
                {
                    anim.Play("CardPullUR");
                }
                else if (setCardForObject.secretPackRarity == CardVariable.SecretPackCardRarity.SuperRare)
                {
                    anim.Play("CardPullSR");
                }
                else
                {
                    anim.Play("CardPullNormal");
                }
                URAnim = false;
                SRAnim = false;
            }
            else
            {
                if (setCardForObject.rarity == CardVariable.CardRarity.SecretRare || altArtCard)
                {
                    anim.Play("CardPullUR");
                }
                else if (setCardForObject.rarity == CardVariable.CardRarity.SuperRare)
                {
                    anim.Play("CardPullSR");
                }
                else
                {
                    anim.Play("CardPullNormal");
                }
                URAnim = false;
                SRAnim = false;
            }
            
            hasBeenRevealed = true;
        }
    }

    public void IncreaseCardsPulledMethod()
    {
        increaseCardsPulled.Raise();
    }

    public void SetHoldingMouse(bool mouseStatus)
    {
        if(mouseStatus == true)
        {
            holdingDownMouse = true;
        }
        else
        {
            holdingDownMouse = false;
        }
    }

    
    
}

