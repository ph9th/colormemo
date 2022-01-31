using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    GameObject button;
    Animator animRed;
    Animator animYellow;
    Animator animBlue;
    GameObject colorManager;
    SetColor taskColorScript;
    Color32 taskColor;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
        if ( 6 <= SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex <= Constants.forestWorldEnd)
        {
            animRed = GameObject.Find("Red").GetComponent<Animator>();
            animYellow = GameObject.Find("Yellow").GetComponent<Animator>();
            animBlue = GameObject.Find("Blue").GetComponent<Animator>();
            colorManager = GameObject.Find("ColorManager");
            taskColorScript = colorManager.GetComponent<SetColor>();
            taskColor = colorManager.GetComponent<SetColor>().taskColor;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStart()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName == "HumanWorld")
        {
            SceneChange.themeID = 0; //HumanWorld themeID = 0
            SceneManager.LoadScene("ObjectStolen");
        }
        else if (buttonName == "Forest")
        {
            SceneChange.themeID = 1; //Forest themeID = 1
            SceneManager.LoadScene("ObjectStolen");
        }
        else if (buttonName == "Water")
        {
            SceneChange.themeID = 2; //Water themeID = 1
            SceneManager.LoadScene("ObjectStolen");
        }
        else if(buttonName == "ThemeButton")
        {
            SceneManager.LoadScene("ThemeSelection");
        }    
    }

    public void GoToIntro()
    {
        SceneManager.LoadScene("IntroductionLevel");
    }

    public void SaveData()
    {
        DataManagerScript.AddSessionData();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
    public void GoToAchievements()
    {
        SceneManager.LoadScene("Achievements");
    }

    public void GoToColoringBook()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;

        SceneManager.LoadScene("ColoringPage" + buttonName);
    }

    public void ColorHelp()
    {
        Debug.Log("help");
        string color; 

        if(sceneName == "ObjectFound")
        {
            color = taskColorScript.ColorToString(StoredColors.stolenObj);
        }
        else
        {
            Debug.Log("task color: " + taskColor);
            color = taskColorScript.ColorToString(taskColor);
            
        }

        if (color == "green")
        {
            animBlue.SetTrigger("Active");
            animYellow.SetTrigger("Active");

        }
        else if (color == "orange")
        {
            animRed.SetTrigger("Active");
            animYellow.SetTrigger("Active");
        }
        if (color == "purple")
        {
            animRed.SetTrigger("Active");
            animBlue.SetTrigger("Active");
        }
    }

    public void Screenshot()
    {
        //ScreenCapture.CaptureScreenshot(Application.dataPath + "/Resources/Screenshots/Screenshot2.png");

       ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);

        StartCoroutine(ScreenshotDelay());
        

    }

    private IEnumerator ScreenshotDelay()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Achievements");
    }

}
