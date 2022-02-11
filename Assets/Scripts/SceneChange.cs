using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static int maxLevel = 2;
    public static int levelCount = 1;
    private string sceneName;
    public static bool error = false;
    public static int buildID;
    public static bool[] levelBool = new bool[40]; //Array storing bool values for each level (build index); index set to true once played to avoid repeating levels
    public static int themeID = 0;
    private int startLvl;
    private int endLvl;

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

        switch(themeID)
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
            default:
                Debug.LogWarning("Unknown theme ID");
                break;
        }


        if (sceneName == "StartScreen")
        {
            InitialiseGame();
        }


        //reset values after every completed iteration
        if (sceneName == "ThemeSelection")
        {
            if (!FindObjectOfType<AudioManager>().IsPlaying("Theme"))
            {
                FindObjectOfType<AudioManager>().Play("Theme");
            }

            FindObjectOfType<AudioManager>().Play("SelectTheme");
            error = false;
            levelCount = 1;
            VSMScript.characters.Clear();
            VSMScript.levelOrder.Clear();

        }

        //ObjectStolen level only has animation, no task
        if (sceneName == "ObjectStolen")
        {
            if (FindObjectOfType<AudioManager>().IsPlaying("Theme"))
            {
                FindObjectOfType<AudioManager>().Pause("Theme");
            }

            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("WitchSteals", 1));

            yield return new WaitForSeconds(1);
            StartCoroutine(LoadLevel());

            //SceneManager.LoadScene(buildID);
            //SetLevelBoolsTrue(buildID);
        }

        if (Constants.humanWorldStart <= SceneManager.GetActiveScene().buildIndex && SceneManager.GetActiveScene().buildIndex <= Constants.waterWorldEnd)
        {
            if(ColorObject.colorTaskAssign == 0)
            {
                FindObjectOfType<AudioManager>().Play("RedBear");
            } else if (ColorObject.colorTaskAssign == 1)
            {
                FindObjectOfType<AudioManager>().Play("YellowBear");
            }
            else if (ColorObject.colorTaskAssign == 2)
            {
                FindObjectOfType<AudioManager>().Play("BlueBear");
            }
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("YourTurn", 1));
            StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("ColorIt", 2));


        }



    }

    public IEnumerator LoadDelay(string sceneName, int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(sceneName);

    }
    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(5);

        //Debug.Log("levelCount: " + levelCount);
        //Debug.Log("maxLevel: " + maxLevel);

        if (levelCount <= maxLevel)
        {
 

            // load the next random level
             buildID = Random.Range(startLvl, endLvl);

            //if all levels have been played, reset all values in array to false
            if (CheckLevelBools(levelBool, startLvl, endLvl))
            {
                levelBool = ResetLevelBools(levelBool, startLvl, endLvl);
            }
            else
            {
                //Generate new buildID until a level is found that hasn't been played yet
                if (levelBool[buildID] == true)
                {
                    buildID = Random.Range(startLvl, endLvl);
                }
            }
            

            SceneManager.LoadScene(buildID);
            SetLevelBoolsTrue(buildID);
          
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

    private bool CheckLevelBools(bool[] array, int startIndex, int endIndex) 
    {
        
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (array[i] == false)
            {
                return false;
            }
        }
        Debug.Log("All levels played");
        return true;
    }

    void SetLevelBoolsTrue(int buildIndex)
    {
        levelBool[buildIndex] = true;
    }

    public static void SetOrder ()
    {
        VSMScript.levelOrder.Add(SceneManager.GetActiveScene().name);

    }

    void InitialiseGame ()
    {
        DataManagerScript.completedIterations = 0;
        levelBool = ResetLevelBools(levelBool, 0, 39);


    }


}