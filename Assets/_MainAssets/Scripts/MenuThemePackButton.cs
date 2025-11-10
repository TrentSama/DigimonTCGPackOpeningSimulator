using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class MenuThemePackButton : MonoBehaviour
{
    private MainMenuScript mainMenu;
    public bool GetMainMenuOnAwake = false;

    public TextMeshProUGUI buttonThemePackName;
    public Image[] buttonThemePackImages;
    public CardSet themePackFile;
    public bool setImagesOnAwake = true;
    private void Awake()
    {
        if (setImagesOnAwake)
        {
            SetPackData();
        }
        if (GetMainMenuOnAwake)
            mainMenu = FindObjectOfType<MainMenuScript>();
    }
    public void SetPackData()
    {
        if (themePackFile.KeyCards.Count > 0)
        {
            /*buttonThemePackImages[0].sprite = await ImageGetData.GetCardImageFromFile(themePackFile.KeyCards[0].name);
            buttonThemePackImages[1].sprite = await ImageGetData.GetCardImageFromFile(themePackFile.KeyCards[1].name);
            buttonThemePackImages[2].sprite = await ImageGetData.GetCardImageFromFile(themePackFile.KeyCards[2].name);*/
            buttonThemePackImages[0].sprite = themePackFile.KeyCards[0].cardImage;
            buttonThemePackImages[1].sprite = themePackFile.KeyCards[1].cardImage;
            buttonThemePackImages[2].sprite = themePackFile.KeyCards[2].cardImage;
        }
        buttonThemePackName.text = themePackFile.SetName;
    }

    public void SetCorrectPackInMenu()
    {
        mainMenu.SetPackToOpenFrom(themePackFile);
    }
}
