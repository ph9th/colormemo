using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FramesScript : MonoBehaviour
{
    GameObject picFrame;
    private Sprite texture;

    private void Awake()
    {
        //Display screenshot taken of each colored coloring page level

        
    }

    private void Start()
    {
        //AssetDatabase.Refresh();

        picFrame = this.gameObject;
        string frameID = picFrame.name;
        string screenshotPath = Application.persistentDataPath + "/Screenshots/";

        //check if folder exists
        if (!Directory.Exists(screenshotPath))
        {
            //create folder
            Directory.CreateDirectory(screenshotPath);
        }

        


        if (File.Exists(screenshotPath + PlayerManager.players[0].name + "/" + "ColoringPage" + frameID + ".jpg"))
        {
            Debug.Log("Screenshot exists at: " + screenshotPath + PlayerManager.players[0].name + "/" + "ColoringPage" + frameID + ".jpg");
            Texture2D SpriteTexture = LoadImage(screenshotPath + PlayerManager.players[0].name + "/" + "ColoringPage" + frameID + ".jpg");
            Sprite coloringPic = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0.5f, 0.5f));

            texture = coloringPic;
            //Debug.Log("texture: " + texture);
        } else
        {
            //Debug.Log("No screenshot exists.");
            texture = Resources.Load<Sprite>("ColoringPages/empty");
        }

        picFrame.GetComponent<Image>().sprite = texture;


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
