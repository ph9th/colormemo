using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static int maxLevel = 1;
    public static int levelCount = 1;
    private string sceneName;
    public static bool error = false;
    public static int buildID;
    public static bool[] levelBool = new bool[40]; //Array storing bool values for each level (build index); index set to true once played to avoid repeating levels
    public static int themeID = 0;

    public IEnumerator Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "StartScreen")
        {
            InitialiseGame();
        }

        if (sceneName != "ObjectStolen" && sceneName != "ObjectFound" && sceneName != "VSMLevel" && sceneName != "StartScreen" && sceneName != "ThemeSelection" && sceneName != "MenuScreen" && sceneName != "IntroductionLevel")
        {
            
        }

        //reset values after every completed iteration
        if (sceneName == "ThemeSelection")
        {
            error = false;
            levelCount = 1;
            VSMScript.characters.Clear();
            VSMScript.levelOrder.Clear();

        }

        //ObjectStolen level only has animation, no task
        if (sceneName == "ObjectStolen")
        {
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene(buildID);
        }

        
  
    }

    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(5);

        if (levelCount <= maxLevel)
        {
            // load the next random level
            if(themeID == 0)
            {
                buildID = Random.Range(Constants.humanWorldStart, Constants.humanWorldEnd);

                //if all levels have been played, reset all values in array to false
                if (CheckLevelBools(levelBool, Constants.humanWorldStart, Constants.humanWorldEnd))
                {
                    Debug.Log("all levels played");
                    levelBool = ResetLevelBools(levelBool, Constants.humanWorldStart, Constants.humanWorldEnd);
                }

                //Generate new buildID until a level is found that hasn't been played yet
                while (levelBool[buildID] == true)
                {
                    buildID = Random.Range(Constants.humanWorldStart, Constants.humanWorldEnd);
                }

                SceneManager.LoadScene(buildID);
                SetLevelBoolsTrue(buildID);
            }
            else if (themeID == 1)
            {
                if (CheckLevelBools(levelBool, Constants.forestWorldStart, Constants.forestWorldEnd))
                {
                    levelBool = ResetLevelBools(levelBool, Constants.forestWorldStart, Constants.forestWorldEnd);
                }

                buildID = Random.Range(Constants.forestWorldStart, Constants.forestWorldEnd);

                while (levelBool[buildID] == true)
                {
                    buildID = Random.Range(Constants.forestWorldStart, Constants.forestWorldEnd);
                }
                SceneManager.LoadScene(buildID);
                SetLevelBoolsTrue(buildID);
            }
        }
        else
        {
            SceneManager.LoadScene("VSMLevel");
        }
    }

    private void Update()
    {
        
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