using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class RewardsScript : MonoBehaviour
{
    GameObject newReward;
    GameObject bg;

    void Awake()
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

    // Start is called before the first frame update
    IEnumerator Start()
    {
        newReward = GameObject.Find((StolenObjectScript.stolenObjId - 1).ToString());
        Debug.Log("stolenobjid reward: " + (StolenObjectScript.stolenObjId - 1));
        newReward.GetComponent<SpriteRenderer>().enabled = true;
        newReward.GetComponent<SpriteRenderer>().color = StoredColors.stolenObj;

        FindObjectOfType<AudioManager>().PlayNoOverlay("AllCorrect");

        ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);

        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("ThemeSelection"); 

    }


}
