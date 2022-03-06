using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneChange : MonoBehaviour
{
    public static int maxLevel = 2;
    public static int levelCount = 1;
    private string sceneName;
    public static bool error = false; //For VSM and VFCM Levels
    public static int buildID;
    public static bool[] levelBool = new bool[40]; //Array storing bool values for each level (build index); index set to true once played to avoid repeating levels
    public static int themeID = 0;
    private int startLvl;
    private int endLvl;

    public static List<int> curPlayedLevels = new List<int>(); //Stores buildIDs of all played levels during one iteration 

    private void Awake()
    {

        DirectoryInfo d = new DirectoryInfo(Application.dataPath); //Assuming Test is your Folder

        FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
        string str = "";

        foreach (FileInfo file in Files)
        {
            str = str + ", " + file.Name;
            Debug.Log(str);
        }



        if (FindObjectOfType<AudioManager>() == null)
        {
            Instantiate(Resources.Load("Prefabs/AudioManager", typeof(AudioManager)));
        }
        FindObjectOfType<AudioManager>().PauseAll();




    }
    public IEnumerator Start()
    {
        Debug.Log("Camera: " + FindObjectOfType<Camera>().name);
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
            curPlayedLevels.Clear();
            ButtonScript.hint = false;

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
            GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Fly");

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
        if(SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            yield return new WaitForSeconds(5);
        }
        else
        {
            yield return new WaitForSeconds(3);
        }
        

        //Load new level until maxLevel is reached
        if (levelCount <= maxLevel)
        {
            //if all levels have been played, reset all values in array to false
            if (CheckLevelBools(levelBool, startLvl, endLvl))
            {
                Debug.Log("Reset");
                levelBool = ResetLevelBools(levelBool, startLvl, endLvl);
                buildID = Random.Range(startLvl, endLvl);
                if (CheckJustPlayed(curPlayedLevels, buildID))
                {
                    GenerateNewId();
                }
            }
            else
            {
                //Generate new buildID until a level is found that hasn't been played yet
                buildID = GenerateNewId();
           

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
        curPlayedLevels.Add(buildIndex);
    }

    public static void SetOrder ()
    {
        VSMScript.levelOrder.Add(SceneManager.GetActiveScene().name);

    }

    private int GenerateNewId()
    {
        int id = Random.Range(startLvl, endLvl);

        int i = 0;
        //Generate new ID until level found that has not been played yet
        while ((CheckPlayed(levelBool, id) || CheckJustPlayed(curPlayedLevels, id)) && i < 10)
        {

            id = Random.Range(startLvl, endLvl);
            i++;
        }
  
        //if no level was found, use the next buildId that is set to false
        if (i == 10)
        {
            for (int j = startLvl; j <= endLvl; j++)
            {
                if (levelBool[j] == false && CheckJustPlayed(curPlayedLevels, j) == false)
                {
                    id = j;

                }
            }
        }

        return id;
    }

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
    /// Check if next level has already been played in this iteration
    /// </summary>
    private static bool CheckJustPlayed(List<int> curPlayed, int levelId)
    {
        if(curPlayed.Exists(x => x == levelId))
        {
          
            return true;
        }
     
        return false;
    }
    void InitialiseGame ()
    {
        DataManagerScript.completedIterations = 0;
        levelBool = ResetLevelBools(levelBool, 0, 39);


    }


}