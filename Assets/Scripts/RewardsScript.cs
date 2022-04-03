using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RewardsScript : MonoBehaviour
{
    void Awake()
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

        if (File.Exists(filePath))
        {
            fileData = System.IO.File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    /// <summary>Shows the reward collection with the most recent addition for 5 seconds, then loads Theme Selection Screen.</summary>
    IEnumerator Start()
    {
        GameObject newReward = GameObject.Find((StolenObjectScript.StolenObjId - 1).ToString());
        GameObject smoke = Resources.Load<GameObject>("FX/Prefabs/FX001_01");
        Instantiate(smoke, new Vector3(newReward.transform.position.x, newReward.transform.position.y, 0), Quaternion.identity);
        smoke.GetComponent<Animator>().Play("Poof");
        newReward.GetComponent<SpriteRenderer>().enabled = true;
        newReward.GetComponent<SpriteRenderer>().color = StoredColors.stolenObj;
        FindObjectOfType<AudioManager>().PlayNoOverlay("ToysBack");
        yield return new WaitForSeconds(5);
        
        ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);
        SceneManager.LoadScene("ThemeSelection"); 
    }

}
