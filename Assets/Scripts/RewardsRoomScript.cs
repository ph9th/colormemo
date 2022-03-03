using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class RewardsRoomScript : MonoBehaviour
{
    GameObject bg;
    // Start is called before the first frame update
    void Start()
    {
        bg = GameObject.Find("background");
        string screenshotPath = Application.persistentDataPath + "/Screenshots/";

        if (File.Exists(screenshotPath + PlayerManager.players[1].name + "/" + "Rewards.jpg"))
        {
            Texture2D SpriteTexture = LoadImage(screenshotPath + PlayerManager.players[1].name + "/" + "Rewards.jpg");
            Sprite bgSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0.5f, 0.5f));

            //Debug.Log("texture: " + texture);

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
