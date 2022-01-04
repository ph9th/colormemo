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
    GetColor penColorScript;
    SetColor taskColorScript;
    SceneChange SceneChanger;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
        SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();

        DataManagerScript.tryCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        colorThisObject(obj);

        timer += Time.deltaTime;
   
        
    }

    //color object if there is touch input
    void colorThisObject (SpriteRenderer obj)
    {
        penColorScript = colorManager.GetComponent<GetColor>();
        taskColorScript = colorManager.GetComponent<SetColor>();


        if (Input.touchCount > 0)
        {
            obj.color = penColorScript.penColor;

            //Increase try counter for every coloring try
            DataManagerScript.tryCounter++;
            DataManagerScript.curTaskColor = taskColorScript.ColorToString(taskColorScript.taskColor);

            if (SceneManager.GetActiveScene().name == "ObjectFound")
            {
                if (compareColors(penColorScript.penColor, StoredColors.stolenObj))
                {
                    Debug.Log("GOOD! Stolen Object was this color!");
                }
                else
                {
                    Debug.Log("Stolen Object was not this color!");
                }
            }
            else
            {
                //correct color
                if (compareColors(penColorScript.penColor, taskColorScript.taskColor))
                {
                    Debug.Log("GOOD!");

                    //store data in dataManager
                    DataManagerScript.levelID = SceneManager.GetActiveScene().buildIndex;
                    DataManagerScript.timeSuccess = timer;
                    
                    DataManagerScript.addRecord();
                    //load next level
                    StartCoroutine(SceneChanger.LoadLevel());

                }
                else
                {
                    Debug.Log("WRONG COLOR!");
                }
            }
        }
    }

    bool compareColors(Color penColor, Color taskColor)
    {
        if (taskColor.Equals(penColor))
        {
            Debug.Log("correct color");
            return true;
        }
        else
        {
            Debug.Log("wrong color");
            return false;
        }
    }



    //color object if there is mouse click
    void OnMouseDown()
    {
        penColorScript = colorManager.GetComponent<GetColor>();
        taskColorScript = colorManager.GetComponent<SetColor>();

        obj.color = penColorScript.penColor;

        //Increase try counter for every coloring try
        DataManagerScript.tryCounter++;
        DataManagerScript.curTaskColor = taskColorScript.ColorToString(taskColorScript.taskColor);

        if (SceneManager.GetActiveScene().name == "ObjectFound")
        {
            if (compareColors(penColorScript.penColor, StoredColors.stolenObj))
            {
                Debug.Log("GOOD! Stolen Object was this color!");
            }
            else
            {
                Debug.Log("Stolen Object was not this color!");
            }
        }
        else
        {
            //correct color
            if (compareColors(penColorScript.penColor, taskColorScript.taskColor))
            {
                Debug.Log("GOOD!");

                //store data in dataManager
                DataManagerScript.levelID = SceneManager.GetActiveScene().buildIndex;
                DataManagerScript.timeSuccess = timer;

                DataManagerScript.addRecord();
                //load next level
                StartCoroutine(SceneChanger.LoadLevel());

            }
            else
            {
                Debug.Log("WRONG COLOR!");
            }
        }
    }

}
