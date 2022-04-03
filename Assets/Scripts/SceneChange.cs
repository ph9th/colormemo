using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    /// <summary>Gets or sets the maximal number of coloring tasks for one iteration.</summary>
    /// <value>The maximum level.</value>
    public static int MaxLevel { get; set; }
    public static int LevelCount { get; set; }

    /// <summary>Error flag for VSM and VFCM tasks.</summary>
    /// <value>
    ///   <c>true</c> if error was made; otherwise, <c>false</c>.</value>
    public static bool Error { get; set; }
    public static int BuildID { get; set; }

    /// <summary>Gets or sets the theme ID.</summary>
    /// <value>The theme ID.</value>
    public static int ThemeID { get; set; }

    /// <summary>Gets or sets the array storing bool values for all scenes (BuildIDs).</summary>
    /// <value>The bool array.</value>
    public static bool[] LevelBool { get; set; } 

    private string sceneName;
    private int curBuildID;

    private int startLvl;
    private int endLvl;


    /// <summary>Gets or sets array storing BuildIDs of all played scenes for one iteration.</summary>
    /// <value>The array storing BuildIDs.</value>
    public static List<int> CurPlayedLevels { get; set; }

    private void Awake()
    {
        if (FindObjectOfType<AudioManager>() == null)
        {
            Instantiate(Resources.Load("Prefabs/AudioManager", typeof(AudioManager)));
        }
        FindObjectOfType<AudioManager>().PauseAll();
    }

    public IEnumerator Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        curBuildID = SceneManager.GetActiveScene().buildIndex;

        switch (ThemeID)
        {
            case 0:
                startLvl = Constants.humanWorldStart;
                endLvl = Constants.humanWorldEnd;
                break;
            case 1:
                startLvl = Constants.forestWorldStart;
                endLvl = Constants.forestWorldEnd;
                break;
            case 2:
                startLvl = Constants.waterWorldStart;
                endLvl = Constants.waterWorldEnd;
                break;
            case 3:
                startLvl = Constants.spaceWorldStart;
                endLvl = Constants.spaceWorldEnd;
                break;
            case 4:
                startLvl = Constants.arcticWorldStart;
                endLvl = Constants.arcticWorldEnd;
                break;
            default:
                Debug.LogWarning("Unknown theme ID");
                break;
        }

        if (sceneName == "StartScreen")
        {
            InitialiseGame();
        }

        //reset values after every completed iteration
        else if (sceneName == "ThemeSelection")
        {
            if (!FindObjectOfType<AudioManager>().IsPlaying("Theme"))
            {
                FindObjectOfType<AudioManager>().Play("Theme");
            }

            FindObjectOfType<AudioManager>().Play("SelectTheme");
            InitialiseIteration();
        }

        //ObjectStolen level only has animation, no task
        else if (sceneName == "ObjectStolen")
        {
            if (FindObjectOfType<AudioManager>().IsPlaying("Theme"))
            {
                FindObjectOfType<AudioManager>().Pause("Theme");
            }
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("WitchSteals", 1));

            yield return new WaitForSeconds(6);
            SceneManager.LoadScene("TransitionScene");
        }

        else if (Constants.humanWorldStart <= curBuildID && curBuildID <= Constants.arcticWorldEnd)
        {
            SetLevelBoolsTrue(curBuildID);
            GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Fly");
            ShowTaskAssignment();
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("YourTurn", 1));
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("ColorIt", 2));
        }
    }

    /// <summary>Loads a scene with a delay.</summary>
    /// <param name="sceneName">Name of the scene.</param>
    /// <param name="seconds">The delay in seconds.</param>
    public IEnumerator LoadDelay(string sceneName, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);

    }
    /// <summary>Loads the next scene.</summary>
    public IEnumerator LoadLevel()
    {
        if(sceneName == "ObjectStolen")
        {
            yield return new WaitForSeconds(5);
        }
        /*else if (sceneName == "TransitionScene")
        {
            yield return new WaitForSeconds(2);
        }*/
        else
        {
            yield return new WaitForSeconds(3);
        }

        //Load new level until MaxLevel is reached
        if (LevelCount <= MaxLevel)
        {
            //if all levels have been played, reset all values in array to false
            if (CheckLevelBools(LevelBool, startLvl, endLvl))
            {
                LevelBool = ResetLevelBools(LevelBool, startLvl, endLvl);
                BuildID = Random.Range(startLvl, endLvl);
                if (CheckJustPlayed(CurPlayedLevels, BuildID))
                {
                    GenerateNewId();
                }
            }
            else
            {
                //Generate new BuildID until a level is found that hasn't been played yet
                BuildID = GenerateNewId();
            }    
            SceneManager.LoadScene(BuildID);
        }
        else
        {
            SceneManager.LoadScene("VSMLevel");
        }
    }

    /// <summary>
    /// Reset all array indices to false
    /// </summary>
    /// <returns></returns>
    private bool[] ResetLevelBools(bool[] array, int startIndex, int endIndex)
    {
        for (int i = startIndex; i<= endIndex; i++)
        {
            array[i] = false;
        }
        return array;
    }


    /// <summary>Checks if all scenes have been played.</summary>
    /// <param name="array">The array.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">The end index.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    private bool CheckLevelBools(bool[] array, int startIndex, int endIndex) 
    {
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (!array[i])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>Marks a scene as played.</summary>
    /// <param name="The buildIndex of the scene.">Index of the build.</param>
    void SetLevelBoolsTrue(int buildIndex)
    {
        LevelBool[buildIndex] = true;
        CurPlayedLevels.Add(buildIndex);
    }

    /// <summary>Stores the order of the coloring tasks for the VSM task.</summary>
    public static void SetOrder ()
    {
        VSMScript.levelOrder.Add(SceneManager.GetActiveScene().name);
    }

    /// <summary>Generates a new BuildID for new scene.</summary>
    /// <returns>A BuildID.</returns>
    private int GenerateNewId()
    {
        int id = Random.Range(startLvl, endLvl);
        int i = 0;
        //Generate new ID until level found that has not been played yet
        while ((CheckPlayed(LevelBool, id) || CheckJustPlayed(CurPlayedLevels, id)) && i < 10)
        {
            id = Random.Range(startLvl, endLvl);
            i++;
        }
  
        //if no level was found, use the next BuildID that is set to false
        if (i == 10)
        {
            for (int j = startLvl; j <= endLvl; j++)
            {
                if (!LevelBool[j] && !CheckJustPlayed(CurPlayedLevels, j))
                {
                    id = j;
                }
            }
        }
        return id;
    }

    /// <summary>Checks if a scene has been played.</summary>
    /// <param name="array">The array.</param>
    /// <param name="index">The index.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    private bool CheckPlayed(bool[] array, int index)
    {
        if (array[index] == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// Check if a level has already been played in the current iteration.
    /// </summary>
    private static bool CheckJustPlayed(List<int> curPlayed, int levelId)
    {
        if(curPlayed.Exists(x => x == levelId))
        {
            return true;
        }
        return false;
    }

    /// <summary>Resets values for new game start.</summary>
    public void InitialiseGame ()
    {
        MaxLevel = 2;
        LevelCount = 1;
        Error = false;
        LevelBool = new bool[Constants.sceneCount];
        LevelBool = ResetLevelBools(LevelBool, 0, Constants.sceneCount - 1);
        ThemeID = 0;
        CurPlayedLevels = new List<int>();
        DataManagerScript.completedIterations = 0;
        ColorObject.ColorTaskAssign = 0;
        VFCMScript.vfcmTaskAssign = 0;
        VSMScript.vsmTaskAssign = 2;
        VSMScript.levelOrder = new List<string>();
        VSMScript.characters = new List<GameObject>();
    }

    /// <summary>Resets all values for the next iteration.</summary>
    void InitialiseIteration()
    {
        
        Error = false;
        LevelCount = 1;
        VSMScript.characters.Clear();
        VSMScript.levelOrder.Clear();
        CurPlayedLevels.Clear();
        ButtonScript.Hint = false;
        ButtonScript.VsmHint = false;
    }

    /// <summary>Visually display which player's turn it is.</summary>
    void ShowTaskAssignment()
    {
        
        Animator animRed = GameObject.Find("Red").GetComponent<Animator>();
        Animator animYellow = GameObject.Find("Yellow").GetComponent<Animator>();
        Animator animBlue = GameObject.Find("Blue").GetComponent<Animator>();

        //Visually show the task assignment.
        if (ColorObject.ColorTaskAssign == 0)
        {
            FindObjectOfType<AudioManager>().Play("RedBear");
            animRed.SetTrigger("Active");
        }
        else if (ColorObject.ColorTaskAssign == 1)
        {
            FindObjectOfType<AudioManager>().Play("YellowBear");
            animYellow.SetTrigger("Active");
        }
        else if (ColorObject.ColorTaskAssign == 2)
        {
            FindObjectOfType<AudioManager>().Play("BlueBear");
            animBlue.SetTrigger("Active");
        }
    }

}