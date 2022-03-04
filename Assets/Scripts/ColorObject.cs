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
    bool witchGone = false;

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
    
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)

        {

            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    FindObjectOfType<AudioManager>().Play("Magic");
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
           
            FindObjectOfType<AudioManager>().PlayNoOverlay("WellDone");
            //Debug.Log("GOOD! Stolen Object was this color!");

            //store data
            float timeSuccess = timer;
            int maxLevel = SceneChange.maxLevel;
            DataManagerScript.AddVFCData(VFCMScript.vfcmTaskAssign, tryCounter, taskColorScript.ColorToString(StoredColors.stolenObj), timeSuccess, maxLevel, ButtonScript.hint);

            PlayerManager.players[VFCMScript.vfcmTaskAssign].maxLevel = maxLevel;

            GameObject.Find("witch").GetComponent<Animator>().ResetTrigger("Fly");

            if (witchGone == false)
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("WitchBeBack");
            }
            GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Correct");

            //if no error was made, number of levels will increase in the next iteration
            if (SceneChange.error == false)
            {
                SceneChange.maxLevel++;
                StolenObjectScript.stolenObjId++;
                Debug.Log("stolenObjId++: " + StolenObjectScript.stolenObjId);

                //Update values for each player
                for(int i = 0; i<3; i++)
                {
                    if(StolenObjectScript.stolenObjId > PlayerManager.players[i].stolenObjId)
                    {
                        PlayerManager.players[i].stolenObjId = StolenObjectScript.stolenObjId;
                    }

                }

                if (!(tryCounter > 1))
                {
                    StartCoroutine(SceneChanger.LoadDelay("Rewards", 3));
                }
            }
            else
            {
                if (SceneChange.maxLevel > 2)
                {
                    SceneChange.maxLevel--;
                }
                StartCoroutine(SceneChanger.LoadDelay("ThemeSelection", 3));
            }

        }
        //decrease number of levels
        else
        {
            SceneChange.error = true;
           

            if (witchGone == false)
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("WitchBeBack");
                GameObject.Find("witch").GetComponent<Animator>().ResetTrigger("Fly");
                GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Error");
                FindObjectOfType<AudioManager>().PlayDelay("Wrong", 2);
                witchGone = true;
            }
            else
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("Wrong");
            }
            
            //Debug.Log("Stolen Object was not this color!");
        }
    }


    /// <summary>
    /// Check if the color is correct. If yes, record data in DataManager and Load next level.
    /// </summary>
    /// <returns></returns>
    void CheckColor()
    {
        //correct color
        if (CompareColors(penColor, taskColorScript.taskColor))
        {
            if (!CharacterScript.success)
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("Super");
                //Debug.Log("CORRECT COLOR!");

                SceneChange.levelCount++;
                SceneChange.SetOrder();
                CharacterScript.success = true;

                //record color data in Data manager
                int levelID = SceneManager.GetActiveScene().buildIndex;
                float timeSuccess = timer;
                DataManagerScript.AddColorData(colorTaskAssign, tryCounter, taskColorScript.ColorToString(curTaskColor), timeSuccess, ButtonScript.hint);

                PlayerManager.IncreaseColorCount(colorTaskAssign, taskColorScript.ColorToString(curTaskColor), 1, 0);

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
            

        }
        else
        {
            //SetColor.repeatColor = taskColorScript.taskColor;

            PlayerManager.IncreaseColorCount(colorTaskAssign, taskColorScript.ColorToString(curTaskColor), 1, 1);

            FindObjectOfType<AudioManager>().PlayNoOverlay("Wrong");
            //Debug.Log("WRONG COLOR!");
        }
    }

    //color object if there is mouse click
    void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("Magic");
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
