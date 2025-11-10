using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;

public class TEMPLoadEveryImageStartup : MonoBehaviour
{
    //public List<Sprite> CardImages = new List<Sprite>();
    public CardSet EveryCard;
    public CardVariable defaultCard;
    //public TextMeshProUGUI loadingPercent;
    public Slider loadingPercent;
    public float totalCards;
    public string sceneToLoad;
    private async void Awake()
    {
        DontDestroyOnLoad(gameObject);
        totalCards = EveryCard.ListOfCardsInSet.Count;
        SceneManager.LoadScene(sceneToLoad);
        for (int i = 0; i < EveryCard.ListOfCardsInSet.Count; i++)
        {
            EveryCard.ListOfCardsInSet[i].cardImage =  await ImageGetData.GetCardImageFromFile(EveryCard.ListOfCardsInSet[i].name);
            loadingPercent.value = (((i + 1) / totalCards) * 100);
        }
        defaultCard.cardImage = await ImageGetData.GetCardImageFromFile(defaultCard.name);
        Destroy(gameObject);
    }
}
