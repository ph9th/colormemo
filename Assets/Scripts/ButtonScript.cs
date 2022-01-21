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
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "VSMLevel" && sceneName != "VCMLevel" && sceneName != "ThemeSelection" && sceneName != "MenuScreen" && sceneName != "ObjectStolen" && sceneName != "StartScreen")
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
        if(EventSystem.current.currentSelectedGameObject.name == "HumanWorld")
        {
            SceneChange.themeID = 0; //HumanWorld themeID = 0
            //select random first level
            SceneChange.buildID = Random.Range(Constants.humanWorldStart, Constants.humanWorldEnd);
            SceneManager.LoadScene("ObjectStolen");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Forest")
        {
            SceneChange.themeID = 1; //Forest themeID = 1
            //select random first level
            SceneChange.buildID = Random.Range(Constants.forestWorldStart, Constants.forestWorldEnd);
            SceneManager.LoadScene("ObjectStolen");
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "ThemeButton")
        {
            SceneManager.LoadScene("ThemeSelection");
        }
        else
        {
            SceneManager.LoadScene("ObjectStolen");
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
            color = taskColorScript.ColorToString(taskColor);
        }

        Debug.Log("taskcolor: " + color);
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

}
