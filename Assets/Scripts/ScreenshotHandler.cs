using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

/// <summary>Contains methods to handle Screenshot capturing.</summary>
public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera myCamera;
    private bool takeScreenshot;
    private string sceneName;

    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            TakeScreenshot_Static(Screen.width, Screen.height);

        }
    }

    private void OnPostRender()
    {
        if (takeScreenshot)
        {
            takeScreenshot = false;
            RenderTexture renderTexture = myCamera.targetTexture;
            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(1, 1, renderTexture.width-1, renderTexture.height-1);
            
            renderResult.ReadPixels(rect, 1, 1);
            byte[] byteArray = renderResult.EncodeToJPG();

            for (int i = 0; i< 3; i++)
            {
                string path = Application.persistentDataPath + "/Screenshots/" + PlayerManager.Players[i].name;
                //check if folder exists
                if (!Directory.Exists(path))
                {
                    //create folder
                    Directory.CreateDirectory(path);
                }

                //Check where to save screenshot to - Coloring Page or Reward
                if(TrimString(sceneName, 2) == "ColoringPage")
                {
                    File.WriteAllBytes(path + "/" + sceneName + ".jpg", byteArray);
                }
                else
                {
                    File.WriteAllBytes(path + "/" + "Rewards.jpg", byteArray);
                }
            }
            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
        }
    }

    public string TrimString(string stringToTrim, int x)
    {
        return stringToTrim.Substring(0, stringToTrim.Length - x);
    }

    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshot = true;
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }
}