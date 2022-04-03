using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    Animator animRed;
    Animator animYellow;
    Animator animBlue;
    SetColor taskColorScript;
    Color32 TaskColor;
    string sceneName;

    public static bool VsmHint { get; set; }
    public static bool Hint { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        int curBuildID = SceneManager.GetActiveScene().buildIndex;

        if ( Constants.vfcmLevel <= curBuildID && curBuildID <= Constants.arcticWorldEnd)
        {
            animRed = GameObject.Find("Red").GetComponent<Animator>();
            animYellow = GameObject.Find("Yellow").GetComponent<Animator>();
            animBlue = GameObject.Find("Blue").GetComponent<Animator>();

            GameObject colorManager = GameObject.Find("ColorManager");
            taskColorScript = colorManager.GetComponent<SetColor>();
            TaskColor = colorManager.GetComponent<SetColor>().TaskColor;

            // Hide hint button if it has been used
            if (Hint)
            {
                this.GetComponent<Image>().enabled = false;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>Sets the theme based on which theme button was clicked.</summary>
    public void SetTheme()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        if (buttonName == "HumanWorld")
        {
            SceneChange.ThemeID = 0; //HumanWorld ThemeID = 0
            SceneManager.LoadScene("ObjectStolen");
        }
        else if (buttonName == "Forest")
        {
            SceneChange.ThemeID = 1; //Forest ThemeID = 1
            SceneManager.LoadScene("ObjectStolen");
        }
        else if (buttonName == "Water")
        {
            SceneChange.ThemeID = 2; //Water ThemeID = 2
            SceneManager.LoadScene("ObjectStolen");
        }
        else if (buttonName == "Space")
        {
            SceneChange.ThemeID = 3; //Space ThemeID = 3
            SceneManager.LoadScene("ObjectStolen");
        }
        else if (buttonName == "Arctic")
        {
            SceneChange.ThemeID = 4; //Arctic ThemeID = 4
            SceneManager.LoadScene("ObjectStolen");
        }
    }

    //Loading and Switching Scenes
    public void LoadStart()
    {
        SceneManager.LoadScene("ThemeSelection");
    }

    /// <summary>Loads Player Selection Screen.</summary>
    public void GoToPlayers()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("PlayerSelection");
    }

    /// <summary>Loads Menu Screen.</summary>
    public void GoToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("MenuScreen");
    }
    /// <summary>Loads Reward Room Screen.</summary>
    public void GoToAchievements()
    {

        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("RewardsRoom");
    }

    /// <summary>Loads Coloring Picture Frame Room Screen.</summary>
    public void GoToFrameHall()
    {

        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("Achievements");
    }

    /// <summary>Loads a Coloring picture screen based on which coloring picture was clicked.</summary>
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

    //Level Help
    /// <summary>Shows help for coloring task.</summary>
    public void ColorHelp()
    {
        Hint = true;
        FindObjectOfType<AudioManager>().Play("Click");
        string color; 

        if(sceneName == "ObjectFound")
        {
            color = taskColorScript.ColorToString(StoredColors.stolenObj);
        }
        else
        {
            color = taskColorScript.ColorToString(TaskColor);
        }
        //Replace default sprites with bears holding wands
        if (color == "red")
        {
            animRed.SetTrigger("Active");
            FindObjectOfType<AudioManager>().PlayNoOverlay("Red");
           
        }
        else if (color == "yellow")
        {
            animYellow.SetTrigger("Active");
            FindObjectOfType<AudioManager>().PlayNoOverlay("Yellow");
        }
        else if (color == "blue")
        {
            animBlue.SetTrigger("Active");

            FindObjectOfType<AudioManager>().PlayNoOverlay("Blue");
        }
        else if (color == "green")
        {
            animBlue.SetTrigger("Active");
            animYellow.SetTrigger("Active");
            FindObjectOfType<AudioManager>().PlayNoOverlay("Green");

        }
        else if (color == "orange")
        {
            animRed.SetTrigger("Active");
            animYellow.SetTrigger("Active");
            FindObjectOfType<AudioManager>().PlayNoOverlay("Orange");
        }
        else if (color == "purple")
        {
            animRed.SetTrigger("Active");
            animBlue.SetTrigger("Active");
            FindObjectOfType<AudioManager>().PlayNoOverlay("Purple");
        }

        if (Hint)
        {
            this.GetComponent<Image>().enabled = false;
        }
    }

    /// <summary>VSM help; displays an animated hand above the correct gameObject's position .</summary>
    public void VSMHelp()
    {
        VsmHint = true;
        GameObject hand = GameObject.Find("Hand");
        GameObject finger = GameObject.Find("Finger");
     
        string objName = VSMScript.levelOrder[VSMPlayScript.orderCounter].Substring(0, VSMScript.levelOrder[VSMPlayScript.orderCounter].Length - 5);
        float xPos = GameObject.Find(objName + "(Clone)").transform.position.x;

        Animator anim = finger.GetComponent<Animator>();

        GameObject.Find("Finger").GetComponent<SpriteRenderer>().enabled = true;
        hand.transform.position = new Vector3(xPos, 750, 0);
        anim.SetTrigger("Help");

        if(VsmHint)
        {
            this.GetComponent<Image>().enabled = false;
        } 
    }
    /// <summary>Takes Screenshot of current scene.</summary>
    public void Screenshot()
    {
        FindObjectOfType<AudioManager>().Play("Screenshot");
        ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);
        StartCoroutine(ScreenshotDelay());
    }

    /// <summary>Takes Screenshot of current scene with a delay of one second.</summary>
    private IEnumerator ScreenshotDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ThemeSelection");
    }

}
