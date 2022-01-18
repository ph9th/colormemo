using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    GameObject button;
    public Animator animRed;
    public Animator animYellow;
    public Animator animBlue;
    GameObject colorManager;
    SetColor taskColorScript;
    Color32 taskColor;
    string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "VSMLevel" && sceneName != "VCMLevel" && sceneName != "ThemeSelection" && sceneName != "MenuScreen" && sceneName != "ObjectStolen")
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
            //5 = HumanWorld or 16 = Forest
            SceneChange.themeID = 5;
        } else if (EventSystem.current.currentSelectedGameObject.name == "Forest")
        {
            SceneChange.themeID = 16;
        }
            SceneManager.LoadScene("ObjectStolen");
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
        string color; 

        if(sceneName == "ObjectFound")
        {
            color = taskColorScript.ColorToString(StoredColors.stolenObj);
        }
        else
        {
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

}
