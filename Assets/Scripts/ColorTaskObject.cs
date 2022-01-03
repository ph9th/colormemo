using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ColorTaskObject : MonoBehaviour
{
    SpriteRenderer obj;
    GameObject colorManager;
    SetColor taskColorScript;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
        taskColorScript = colorManager.GetComponent<SetColor>();
        colorTaskObject(obj);

        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            StoredColors.stolenObj = taskColorScript.taskColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void colorTaskObject(SpriteRenderer obj)
    {
        //Debug.Log("taskcolor" + taskColorScript.taskColor);
        obj.color = taskColorScript.taskColor;
    }

  
}
