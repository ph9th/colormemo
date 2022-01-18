using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static int maxLevel = 1;
    static int levelCount = 1;
    private string sceneName;
    public static bool error = false;
    public static int themeID;

    public IEnumerator Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        if (SceneManager.GetActiveScene().name == "StartScreen")
        {
            InitaliseGame();
        }

        if (sceneName != "ObjectStolen" && sceneName != "ObjectFound" && sceneName != "VSMLevel" && sceneName != "StartScreen" && sceneName != "ThemeSelection")
        {
            levelCount++;
            SetOrder();
        }

        //reset levelCount after every completed level
        if (SceneManager.GetActiveScene().name == "ThemeSelection")
        {
            error = false;
            levelCount = 1;
        }

        //ObjectStolen level only has animation, no task
        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            VSMScript.characters.Clear();
            InitaliseBools();
            yield return new WaitForSeconds(6);

            SceneManager.LoadScene(themeID);
        }
  
    }

    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(5);

        if (levelCount <= maxLevel)
        {
            // load the next level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("VSMLevel");
        }
    }

    private void Update()
    {
        
    }

    void InitaliseBools()
    {
        VSMScript.sandbox = false;
        VSMScript.amusement = false;
        VSMScript.garden = false;
        VSMScript.road = false;
        VSMScript.park = false;
        VSMScript.house = false;
    }



    void SetOrder ()
    {
        VSMScript.levelOrder.Add(SceneManager.GetActiveScene().name);

    }

    void InitaliseGame ()
    {
        DataManagerScript.completedIterations = 0;

    }


}