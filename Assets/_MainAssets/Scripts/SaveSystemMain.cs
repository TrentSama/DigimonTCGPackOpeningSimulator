using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystemMain
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private static readonly string DECK_SAVE_FOLDER = Application.dataPath + "/DeckSaves/";
    public static void Initialize()
    {
        if (!Directory.Exists(SAVE_FOLDER)) // Seeing if the save file exists, if not, create it.
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
        if (!Directory.Exists(DECK_SAVE_FOLDER)) // Seeing if the deck save file exists, if not, create it.
        {
            Directory.CreateDirectory(DECK_SAVE_FOLDER);
        }
    }

    public static void Save(string saveString, int saveSlot)
    {
        DirectoryInfo d = new DirectoryInfo(SAVE_FOLDER);
        string saveFileName;
        if (saveSlot == 0)
        {
            if (d.GetFiles("*.json").Length > 0)
            {
                saveFileName = d.GetFiles("*.json")[0].Name;
                Debug.Log(saveFileName);
                File.WriteAllText(SAVE_FOLDER + saveFileName, saveString);
            }
            else
            {
                File.WriteAllText(SAVE_FOLDER + "/save.json", saveString);
            }
        }
        else
        {
            File.WriteAllText(SAVE_FOLDER + "/save" + (saveSlot).ToString() + ".json", saveString);
            Debug.Log("/save" + (saveSlot).ToString() + ".json");
        }

    }
    public static void SaveDeck(string saveString)
    {
        DirectoryInfo d = new DirectoryInfo(DECK_SAVE_FOLDER);
        string saveFileName;
        if (d.GetFiles("*.json").Length > 0)
        {
            saveFileName = d.GetFiles("*.json")[0].Name;
            Debug.Log(saveFileName);
            File.WriteAllText(DECK_SAVE_FOLDER + saveFileName, saveString);
        }
        else
        {
            File.WriteAllText(DECK_SAVE_FOLDER + "/DeckSaves.json", saveString);
        }
    }
    public static string Load(int saveSlot)
    {
        DirectoryInfo d = new DirectoryInfo(SAVE_FOLDER);
        if (saveSlot == 0)
        {
            if (d.GetFiles("*.json").Length > 0)
            {
                string saveString = File.ReadAllText(d.GetFiles("*.json")[0].FullName);
                return saveString;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }

    }
    public static string LoadDecks()
    {
        DirectoryInfo d = new DirectoryInfo(DECK_SAVE_FOLDER);
        if (d.GetFiles("*.json").Length > 0)
        {
            string saveString = File.ReadAllText(d.GetFiles("*.json")[0].FullName);
            return saveString;
        }
        else
        {
            return null;
        }
        /*if (File.Exists(SAVE_FOLDER + "/DeckSaves.json"))
            {
                string saveString = File.ReadAllText(SAVE_FOLDER + "/DeckSaves.json");
                return saveString;
            }
            else
            {
                return null;
            }*/
    }
    public static bool CheckIfSaveFilesExist()
    {
        if (File.Exists(SAVE_FOLDER + "/save.json") || File.Exists(SAVE_FOLDER + "/save2.json") || File.Exists(SAVE_FOLDER + "/save3.json"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public static string ReturnSaveDataPath()
    {
        return SAVE_FOLDER;
    }
}
