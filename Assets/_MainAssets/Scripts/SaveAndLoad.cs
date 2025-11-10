using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour
{
    [System.Serializable]
    public class SavedDeckClass
    {
        public List<string> ListForDeck = new List<string>();
        public List<string> EggListForDeck = new List<string>();
        public string NameForDeck;
        public string KeyCardForDeck;
    }
     [System.Serializable]
    public class SaveDataClass
    {
        // Secret packs
        public List<bool> IfSecretPackIsUnlocked = new List<bool>();
        // Secret packs
        public List<bool> IfSecretPackIsCompleted = new List<bool>();
        // List of cards
        public List<int> AmountOfEachCardOwned = new List<int>();

        //public List<SavedDeckClass> Decks = new List<SavedDeckClass>();
    }
    [System.Serializable]
    public class DeckSaveDataClass
    {
        public List<SavedDeckClass> Decks = new List<SavedDeckClass>();
    }

    public List<CardVariable> AllCardFiles = new List<CardVariable>();
    public List<CardSet> CardSets = new List<CardSet>();
    public List<DeckFile> SavedDecks = new List<DeckFile>();

    //public SaveFileDataHolder saveDataContainer;
    [SerializeField]
    protected SaveDataClass SaveObject = new SaveDataClass();
    [SerializeField]
    protected DeckSaveDataClass DeckSaveObject = new DeckSaveDataClass();

    public CardVariable defaultDeckPicture;

    public bool LoadOnAwake = false;
    public bool SaveOnAwake = false;

    public int targetFPS = 60;

    //private string jsonSave;

    public virtual void Awake()
    {
        Application.targetFrameRate = targetFPS;
        QualitySettings.vSyncCount = 0;
        SaveSystemMain.Initialize();
        if (SaveOnAwake)
            SaveGameToSlot();
        if (LoadOnAwake)
            LoadGameSlot(0);
    }

    private void Initialize()
    {

        for (int i = 0; i < AllCardFiles.Count; i++)
        {
            SaveObject.AmountOfEachCardOwned[i] = AllCardFiles[i].amountOwned;
        }
        for (int i = 0; i < CardSets.Count; i++)
        {
            SaveObject.IfSecretPackIsUnlocked[i] = CardSets[i].hasBeenUnlocked;
            SaveObject.IfSecretPackIsCompleted[i] = CardSets[i].hasBeenCompleted;
        }
        for (int i = 0; i < SavedDecks.Count; i++)
        {
            if(SavedDecks[i].CardsInDeck.Count > 0)
            {
                for (int j = 0; j < SavedDecks[i].CardsInDeck.Count; j++)
                {
                    DeckSaveObject.Decks[i].ListForDeck.Add(SavedDecks[i].CardsInDeck[j].name);
                }
            }
            if (SavedDecks[i].EggCardsInDeck.Count > 0)
            {
                for (int j = 0; j < SavedDecks[i].EggCardsInDeck.Count; j++)
                {
                    DeckSaveObject.Decks[i].EggListForDeck.Add(SavedDecks[i].EggCardsInDeck[j].name);
                }
            } 
            if(SavedDecks[i].CurrentDeckPicture != null)
                DeckSaveObject.Decks[i].KeyCardForDeck = SavedDecks[i].CurrentDeckPicture.name;
            DeckSaveObject.Decks[i].NameForDeck = SavedDecks[i].DeckName;
        }

    }
    private void InitializeForReset()
    {

        for (int i = 0; i < AllCardFiles.Count; i++)
        {
            SaveObject.AmountOfEachCardOwned[i] = 0;
        }
        for (int i = 0; i < CardSets.Count; i++)
        {
            SaveObject.IfSecretPackIsUnlocked[i] = false;
            SaveObject.IfSecretPackIsCompleted[i] = false;
        }
        for (int i = 0; i < DeckSaveObject.Decks.Count; i++)
        {
            DeckSaveObject.Decks[i].EggListForDeck.Clear();
            DeckSaveObject.Decks[i].ListForDeck.Clear();
            DeckSaveObject.Decks[i].KeyCardForDeck = "";
            DeckSaveObject.Decks[i].NameForDeck = "";
        }

    }
    public void SaveGameToSlot()
    {
        Initialize();
        string jsonSave = JsonUtility.ToJson(SaveObject);
        SaveSystemMain.Save(jsonSave, 0);
        string jsonSaveDeck = JsonUtility.ToJson(DeckSaveObject);
        SaveSystemMain.SaveDeck(jsonSaveDeck);
        Debug.Log("Saved!");
    }
    public void ResetSaveData()
    {
        InitializeForReset();
        string jsonSave = JsonUtility.ToJson(SaveObject);
        SaveSystemMain.Save(jsonSave, 0);
        string jsonSaveDeck = JsonUtility.ToJson(DeckSaveObject);
        SaveSystemMain.SaveDeck(jsonSaveDeck);

        Debug.Log("Saved!");
        LoadGameSlot(0);
    }
    public void LoadGameSlot(int saveSlot)
    {
        string saveString = SaveSystemMain.Load(saveSlot);
        string deckSaveString = SaveSystemMain.LoadDecks();

        if (saveString != null)
        {
            SaveObject = JsonUtility.FromJson<SaveDataClass>(saveString);
            for (int i = 0; i < AllCardFiles.Count; i++)
            {
                AllCardFiles[i].amountOwned = SaveObject.AmountOfEachCardOwned[i];
            }
            for (int i = 0; i < CardSets.Count; i++)
            {
                CardSets[i].hasBeenUnlocked = SaveObject.IfSecretPackIsUnlocked[i];
                CardSets[i].hasBeenCompleted = SaveObject.IfSecretPackIsCompleted[i];
            }
        }
        else
        {
            Debug.Log("No save data found!");
        }
        if (deckSaveString != null)
        {
            DeckSaveObject = JsonUtility.FromJson<DeckSaveDataClass>(deckSaveString);

            for (int i = 0; i < SavedDecks.Count; i++)
            {
                SavedDecks[i].CardsInDeck.Clear();
                SavedDecks[i].EggCardsInDeck.Clear();
                for (int j = 0; j < DeckSaveObject.Decks[i].ListForDeck.Count; j++)
                {
                    for (int k = 0; k < AllCardFiles.Count; k++)
                    {
                        if (AllCardFiles[k].name == DeckSaveObject.Decks[i].ListForDeck[j])
                        {
                            SavedDecks[i].CardsInDeck.Add(AllCardFiles[k]);
                            break;
                        }
                    }
                }
                for (int j = 0; j < DeckSaveObject.Decks[i].EggListForDeck.Count; j++)
                {
                    for (int k = 0; k < AllCardFiles.Count; k++)
                    {
                        if (AllCardFiles[k].name == DeckSaveObject.Decks[i].EggListForDeck[j])
                        {
                            SavedDecks[i].EggCardsInDeck.Add(AllCardFiles[k]);
                            break;
                        }
                    }
                }
                SavedDecks[i].DeckName = DeckSaveObject.Decks[i].NameForDeck;

                if(DeckSaveObject.Decks[i].KeyCardForDeck != null)
                {
                    for (int k = 0; k < AllCardFiles.Count; k++)
                    {
                        if (AllCardFiles[k].name == DeckSaveObject.Decks[i].KeyCardForDeck)
                        {
                            SavedDecks[i].CurrentDeckPicture = AllCardFiles[k];
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.Log("No decks found!");
        }
    }
}
