using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class ColorObject : MonoBehaviour
{
    SpriteRenderer obj;
    GameObject colorManager;
    Color32 penColor;
    SetColor taskColorScript;
    Color32 curTaskColor;
    SceneChange SceneChanger;
    float timer;
    Color32 lastTryColor = new Color(0,0,0,255);
    public static int colorTaskAssign = 0;
    int tryCounter;



    void Start()
    {
        //Debug.Log("assigned to: " + colorTaskAssign);
        // 
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
        SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();
        
        taskColorScript = colorManager.GetComponent<SetColor>();

        curTaskColor = taskColorScript.taskColor;
        tryCounter = 0;

        
    }

    void Update()
    {
        ColorThisObject(obj);

        timer += Time.deltaTime;

    }


    /// <summary>
    /// color object if there is touch input
    /// </summary>
    /// <param name="obj"></param>
    void ColorThisObject(SpriteRenderer obj)
    {
        penColor = colorManager.GetComponent<GetColor>().penColor;
    
        if (Input.touchCount > 0)

        {
            obj.color = penColor; //color object with pen color

            //Increase try counter for every new coloring try
            if (!CompareColors(penColor, lastTryColor))
            {
                tryCounter++;
            }
            lastTryColor = obj.color; //store used color to check if equal to the color of next touch input
            
            if (SceneManager.GetActiveScene().name == "ObjectFound")
            {
                CheckObjFoundColor();
            }
            else
            {
                CheckColor();
            }
        }
    }


    /// <summary>
    /// Compare 2 colors.
    /// </summary>
    /// <param name="color1"></param>
    /// <param name="color2"></param>
    /// <returns>true if the same colors, false if not the same colors</returns>

    bool CompareColors(Color color1, Color color2)
    {
        if (color1.Equals(color2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Check only for Level 'Object Found' if the color is correct.
    /// </summary>
    /// <returns></returns>
    void CheckObjFoundColor()
    {
        if (CompareColors(penColor, StoredColors.stolenObj))
        {
            Debug.Log("GOOD! Stolen Object was this color!");

            //store data
            float timeSuccess = timer;
            int maxLevel = SceneChange.maxLevel;
            DataManagerScript.AddVFCData(tryCounter, taskColorScript.ColorToString(StoredColors.stolenObj), timeSuccess, maxLevel);

            //if no error was made, number of levels will increase in the next iteration
            if (SceneChange.error == false)
            {
                SceneChange.maxLevel++;
            }

            SceneManager.LoadScene("ThemeSelection");
        }
        else
        {
            SceneChange.error = true;
            Debug.Log("Stolen Object was not this color!");
        }
    }


    /// <summary>
    /// Check if the color is correct. If yes, record data in DataManager.
    /// </summary>
    /// <returns></returns>
    void CheckColor()
    {
        //correct color
        if (CompareColors(penColor, taskColorScript.taskColor))
        {
            Debug.Log("CORRECT COLOR!");

            //record color data in Data manager
            int levelID = SceneManager.GetActiveScene().buildIndex;
            float timeSuccess = timer;
            DataManagerScript.AddColorData(levelID, tryCounter, taskColorScript.ColorToString(curTaskColor), timeSuccess);

            //assign next level to next avatar
            if (colorTaskAssign < 2)
            {
                colorTaskAssign++;
            }
            else
            {
                colorTaskAssign = 0;
            }

            //load next level
            StartCoroutine(SceneChanger.LoadLevel());

        }
        else
        {
            SetColor.repeatColor = taskColorScript.taskColor;
            Debug.Log("WRONG COLOR!");
        }
    }

    //color object if there is mouse click
    void OnMouseDown()
    {
        obj.color = penColor; //color object with pen color
        if (!CompareColors(penColor, lastTryColor))
        {
            tryCounter++;
        }
        lastTryColor = obj.color; //store used color to check if equal to the color of next touch input
                                  //Increase try counter for every new coloring try


        if (SceneManager.GetActiveScene().name == "ObjectFound")
        {
            CheckObjFoundColor();
        }
        else
        {
            CheckColor();
        }

    }
}
