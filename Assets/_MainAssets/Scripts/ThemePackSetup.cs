using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemePackSetup : MonoBehaviour
{
    public List<CardSet> ListOfAllSecretPacks = new();
    public List<MenuThemePackButton> SecretPackButtons = new();

    private void Awake()
    {
        for (int i = 0; i < ListOfAllSecretPacks.Count; i++)
        {
            SecretPackButtons[i].themePackFile = ListOfAllSecretPacks[i];
            if (ListOfAllSecretPacks[i].hasBeenUnlocked)
            {
                SecretPackButtons[i].gameObject.SetActive(true);
            }
        }
    }

    public void UnlockAllSecretPacks()
    {
        for (int i = 0; i < ListOfAllSecretPacks.Count; i++)
        {
            SecretPackButtons[i].gameObject.SetActive(true);
            ListOfAllSecretPacks[i].hasBeenUnlocked = true;
        }
    }
    public void LockAllSecretPacks()
    {
        for (int i = 0; i < ListOfAllSecretPacks.Count; i++)
        {
            SecretPackButtons[i].gameObject.SetActive(false);
            ListOfAllSecretPacks[i].hasBeenUnlocked = false;
        }
    }
}
