using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

/// <summary>Displays the reward collection (png file) for one player.</summary>
public class RewardsRoomScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject bg = GameObject.Find("background");
        string screenshotPath = Application.persistentDataPath + "/Screenshots/";

        if (File.Exists(screenshotPath + PlayerManager.Players[1].name + "/" + "Rewards.jpg"))
        {
            Texture2D SpriteTexture = LoadImage(screenshotPath + PlayerManager.Players[1].name + "/" + "Rewards.jpg");
            Sprite bgSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0.5f, 0.5f));

            bg.GetComponent<SpriteRenderer>().sprite = bgSprite;
        }
    }

    private static Texture2D LoadImage(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (System.IO.File.Exists(filePath))
        {
            fileData = System.IO.File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
