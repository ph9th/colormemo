using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ColorTaskObject : MonoBehaviour
{
    
    SpriteRenderer obj;
    GameObject colorManager;
    Color32 taskColor;

    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
        taskColor = colorManager.GetComponent<SetColor>().taskColor;
        ColorObj(obj);

        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            StoredColors.stolenObj = taskColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// Color the object in the set task color.
    /// </summary>
    /// <param name="obj"> object to be colored</param>
    /// <returns></returns>
    void ColorObj(SpriteRenderer obj)
    {
        obj.color = taskColor;
    }

  
}
