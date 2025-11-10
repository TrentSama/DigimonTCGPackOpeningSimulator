using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuDeckButton : MonoBehaviour
{
    public TextMeshProUGUI buttonDeckName;
    public Image buttonDeckImage;
    public DeckFile deckForButton;
    private void OnEnable()
    {
        if (deckForButton.CurrentDeckPicture != null)
            buttonDeckImage.sprite = deckForButton.CurrentDeckPicture.cardImage;
        buttonDeckName.text = deckForButton.DeckName;
    }
}
