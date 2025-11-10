using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class ImageGetData
{
    public static async Task<Sprite> GetCardImageFromFile(string CardID)
    {
        string PathForCardArt = Application.dataPath + "/Images/" + CardID + ".jpg";
        string PathForCardArtPng = Application.dataPath + "/Images/" + CardID + ".png";
        if (File.Exists(PathForCardArt))
        {
            //Debug.Log("Sprite found!");
            //Debug.Log(PathForCardArt);
            byte[] imageBuff = await ReadFile(PathForCardArt);
            Texture2D tex = BinaryToTexture(imageBuff);
            //if(tex.width % 4 == 0 && tex.height % 4 == 0)
            //    tex.Compress(false);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            //CardImage.sprite = sprite;
            return sprite;
        }
        else if (File.Exists(PathForCardArtPng))
        {
            byte[] imageBuff = await ReadFile(PathForCardArtPng);
            Texture2D tex = BinaryToTexture(imageBuff);
            //if (tex.width % 4 == 0 && tex.height % 4 == 0)
            //    tex.Compress(false);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            return sprite;
        }
        else
        {
            //Debug.Log("No sprite found");
            //Debug.Log(PathForCardArt);
            return null;
        }
    }

    public static async Task<Sprite> GetDefaultCardBack()
    {
        string PathForCardArt = Application.dataPath + "/Images/CardBack.jpg";
        string PathForCardArtPng = Application.dataPath + "/Images/CardBack.png";
        if (File.Exists(PathForCardArt))
        {
            Debug.Log(PathForCardArt);
            byte[] imageBuff = await ReadFile(PathForCardArt);
            Texture2D tex = BinaryToTexture(imageBuff);
            //tex.Compress(false);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            
            return sprite;
        }
        else if (File.Exists(PathForCardArtPng))
        {
            Debug.Log(PathForCardArt);
            byte[] imageBuff = await ReadFile(PathForCardArt);
            Texture2D tex = BinaryToTexture(imageBuff);
            //tex.Compress(false);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);

            return sprite;
        }
        else
        {
            Debug.Log("No sprite found");
            Debug.Log(PathForCardArt);
            return null;
        }
    }
    public static Texture2D BinaryToTexture(byte[] bytes)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.LoadImage(bytes);
        if (texture.width % 4 == 0 && texture.height % 4 == 0)
            texture.Compress(false);
        return texture;
    }
    public static async Task<byte[]> ReadFile(string path)
    {
        using (FileStream fileStream = new FileStream(
            path, FileMode.Open, FileAccess.Read))
        {
            var resultBytes = new byte[fileStream.Length];
            await fileStream.ReadAsync(resultBytes, 0, (int)fileStream.Length);
            return resultBytes;
        }
    }

}
