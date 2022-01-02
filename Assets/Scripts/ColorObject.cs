using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ColorObject : MonoBehaviour
{
    SpriteRenderer obj;
    GameObject colorManager;
    GetColor penColorScript;
    SetColor taskColorScript;
    SceneChange SceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
        SceneChanger = GameObject.Find("SceneChanger").GetComponent<SceneChange>();

    }

    // Update is called once per frame
    void Update()
    {
        colorThisObject(obj);
    }

    void colorThisObject (SpriteRenderer obj)
    {
        penColorScript = colorManager.GetComponent<GetColor>();
        taskColorScript = colorManager.GetComponent<SetColor>();


        if (Input.touchCount > 0)
        {
            Debug.Log("tapped");
            obj.color = penColorScript.penColor;

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
                if (compareColors(penColorScript.penColor, taskColorScript.taskColor))
                {
                    Debug.Log("GOOD!");

                }
                else
                {
                    Debug.Log("WRONG COLOR!");
                }
            }
        }
    }

    bool compareColors (Color penColor, Color taskColor)
    {
        if (taskColor.Equals(penColor)) {
            Debug.Log("correct color");
            return true;
        } else
        {
            Debug.Log("wrong color");
            return false;
        }
    }

    void OnMouseDown()
    {
        //Debug.Log("Sprite Clicked");
        obj.color = penColorScript.penColor;

        if (compareColors(penColorScript.penColor, taskColorScript.taskColor))
        {
            Debug.Log("GOOD!");
            StartCoroutine(SceneChanger.LoadLevel());
        }
        else
        {
            Debug.Log("stored color: stolenObj");
            Debug.Log(StoredColors.stolenObj);

            Debug.Log("WRONG COLOR!");
        }
    }
}
