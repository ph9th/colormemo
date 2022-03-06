using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using UnityEngine.UI;

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

    public static bool hint;
    public static bool vsmhint;

    // Start is called before the first frame update
    void Start()
    {
        vsmhint = false;
        sceneName = SceneManager.GetActiveScene().name;

        if ( Constants.humanWorldStart <= SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex <= Constants.waterWorldEnd || sceneName == "ObjectFound" || sceneName == "IntroductionLevel")
        {
            animRed = GameObject.Find("Red").GetComponent<Animator>();
            animYellow = GameObject.Find("Yellow").GetComponent<Animator>();
            animBlue = GameObject.Find("Blue").GetComponent<Animator>();

            colorManager = GameObject.Find("ColorManager");
            taskColorScript = colorManager.GetComponent<SetColor>();
            taskColor = colorManager.GetComponent<SetColor>().taskColor;

            if (hint == true)
            {
                this.GetComponent<Image>().enabled = false;
            }
        }

        

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetTheme()
    {
        FindObjectOfType<AudioManager>().Play("Click");
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
        else if (buttonName == "ThemeButton")
        {
            SceneManager.LoadScene("ThemeSelection");
        }
    }


    //Loading and Switching Scenes
    public void LoadStart()
    {
        FindObjectOfType<AudioManager>().Play("Click");
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
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("IntroductionLevel");
    }

    public void GoToPlayers()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("PlayerSelection");
    }

    public void GoToPlayerStats()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("PlayerStats");
    }

    public void GoToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("MenuScreen");
    }
    public void GoToAchievements()
    {

        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("RewardsRoom");
    }

    public void GoToColoringBook()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        string buttonName;

        if (sceneName == "RewardSelection")
        {
            buttonName = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name.Substring(9, 1);
        }
        else
        {
            buttonName = EventSystem.current.currentSelectedGameObject.name;
        }
        SceneManager.LoadScene("ColoringPage" + buttonName);
    }

    //Player Management
    public void SaveData()
    {
        DataManagerScript.AddSessionData();
    }

    public void SavePlayer()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        SaveSystem.Save();
        SaveData();
    }

    public void AddPlayer()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        StartCoroutine( GameObject.Find("PlayerManager").GetComponent<PlayerManager>().CreatePlayer());
    }

   public void Submit()
    {
        Debug.Log("Submit new Player");
        FindObjectOfType<AudioManager>().Play("Click");
        PlayerManager.submitted = true;

    }

    public void PlayerSelectionDone()
    {
        FindObjectOfType<AudioManager>().Play("Click");

        if (PlayerSlot.playerCount == 1)
        {
            string yellow = PlayerSlot.yellowSlotName;

            SelectPlayer(0, yellow);
            SelectPlayer(1, yellow);
            SelectPlayer(2, yellow);

            SceneManager.LoadScene("MenuScreen");
            DataManagerScript.AddHeadings();

        }
        else if(PlayerSlot.playerCount == 3)
        {
            string red = PlayerSlot.redSlotName;
            string yellow = PlayerSlot.yellowSlotName;
            string blue = PlayerSlot.blueSlotName;

            if (red != null && yellow != null && blue != null)
            {
                SelectPlayer(0, red);
                SelectPlayer(1, yellow);
                SelectPlayer(2, blue);

                SceneManager.LoadScene("MenuScreen");
                DataManagerScript.AddHeadings();

            }
            else
            {
                Debug.LogWarning("Select 3 Players.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerCount Error");
        }

    }

    public void SetOnePlayer()
    {
        PlayerSlot.playerCount = 1;
        GameObject.Find("Blue").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Red").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BlueSlot").GetComponent<Image>().enabled = false;
        GameObject.Find("RedSlot").GetComponent<Image>().enabled = false;

    }

    public void SetThreePlayers()
    {
        PlayerSlot.playerCount = 3;
        GameObject.Find("Blue").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Red").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("BlueSlot").GetComponent<Image>().enabled = true;
        GameObject.Find("RedSlot").GetComponent<Image>().enabled = true;
    }

    public void SelectPlayer (int playerArrayID, string name)
    {
        PlayerManager.players[playerArrayID] = new PlayerObject(name);
        SaveSystem.LoadPlayer(name);
    }

    
    //Level Help
    public void ColorHelp()
    {
        hint = true;
        FindObjectOfType<AudioManager>().Play("Click");
        //Debug.Log("help");
        string color; 

        if(sceneName == "ObjectFound")
        {
            color = taskColorScript.ColorToString(StoredColors.stolenObj);
        }
        else
        {
            color = taskColorScript.ColorToString(taskColor);
        }

        //Replace default sprites with bears holding wands
        if (color == "red")
        {
            animRed.SetTrigger("Active");
        }
        else if (color == "yellow")
        {
            animYellow.SetTrigger("Active");
        }
        else if (color == "blue")
        {
            animBlue.SetTrigger("Active");
        }
        else if (color == "green")
        {
            animBlue.SetTrigger("Active");
            animYellow.SetTrigger("Active");
        }
        else if (color == "orange")
        {
            animRed.SetTrigger("Active");
            animYellow.SetTrigger("Active");
        }
        else if (color == "purple")
        {
            animRed.SetTrigger("Active");
            animBlue.SetTrigger("Active");
        }

        if (hint == true)
        {
            this.GetComponent<Image>().enabled = false;
        }

    }

    public void VSMHelp()
    {
        vsmhint = true;
        GameObject hand = GameObject.Find("Hand");
        GameObject finger = GameObject.Find("Finger");
     
        string objName = VSMScript.levelOrder[VSMPlayScript.orderCounter].Substring(0, VSMScript.levelOrder[VSMPlayScript.orderCounter].Length - 5);
        float xPos = GameObject.Find(objName + "(Clone)").transform.position.x;

        Animator anim = finger.GetComponent<Animator>();

        GameObject.Find("Finger").GetComponent<SpriteRenderer>().enabled = true;
        hand.transform.position = new Vector3(xPos, 750, 0);
        anim.SetTrigger("Help");

        if(vsmhint == true)
        {
            this.GetComponent<Image>().enabled = false;
        }
         
    }
}
