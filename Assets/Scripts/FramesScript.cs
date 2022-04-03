using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FramesScript : MonoBehaviour
{
    private Sprite texture;

    private void Start()
    {
        //AssetDatabase.Refresh();

        GameObject picFrame = this.gameObject;
        string frameID = picFrame.name;
        string screenshotPath = Application.persistentDataPath + "/Screenshots/";

        //check if folder exists
        if (!Directory.Exists(screenshotPath))
        {
            //create folder
            Directory.CreateDirectory(screenshotPath);
        }

        //if player has colored the coloring picture, use the saved screenshot as the sprite
        if (File.Exists(screenshotPath + PlayerManager.Players[0].name + "/" + "ColoringPage" + frameID + ".jpg"))
        {
            Texture2D SpriteTexture = LoadImage(screenshotPath + PlayerManager.Players[0].name + "/" + "ColoringPage" + frameID + ".jpg");
            Sprite coloringPic = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0.5f, 0.5f));

            texture = coloringPic;
        } else
        {
            texture = Resources.Load<Sprite>("ColoringPages/thumbnail" + frameID);
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
            tex.LoadImage(fileData); // this will auto-resize the texture dimensions.
        }
        return tex;
    }



}
