using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SetColor : MonoBehaviour
{
    public Color taskColor;
    Color[] colors = new Color[9];
    public static Color32 repeatColor = new Color32(0, 0, 0, 255);

    private void Awake()
    {
        colors[0] = new Color32(235, 30, 30, 255); //red
        colors[1] = new Color32(255, 247, 0, 255); //yellow
        colors[2] = new Color32(2, 78, 219, 255); //blue 
        colors[3] = new Color32(24, 196, 8, 255); //green
        colors[4] = new Color32(255, 154, 23, 255); //orange
        colors[5] = new Color32(181, 27, 242, 255); //purple

        setObjColor();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void setObjColor()
    {
        //
        if( repeatColor.Equals(new Color32(0, 0, 0, 255)) )
        {
            taskColor = randomColor();
        }
        else
        {
            //taskColor = repeatColor;
            repeatColor = new Color32(0, 0, 0, 255);
        }
    }

    Color randomColor()
    {
        //choose colors based on who the task is assigned to;
        // red can only paint red, orange, purple
        // yellow only yellow, green, orange
        // 
        Color[] colorList = new Color[3];

        //Set colors according to which player's turn it is
        int assignedTo;

        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            assignedTo = VFCMScript.vfcmTaskAssign;
        } else
        {
            assignedTo = ColorObject.colorTaskAssign;
        }

        if (assignedTo == 0)
        {
            Debug.Log("Red Turn");
            colorList[0] = colors[0];
            colorList[1] = colors[4];
            colorList[2] = colors[5];
        }
        else if (assignedTo == 1)
        {
            Debug.Log("Yellow Turn");
            colorList[0] = colors[1];
            colorList[1] = colors[3];
            colorList[2] = colors[4];
        }
        else if (assignedTo == 2)
        {
            Debug.Log("Blue Turn");
            colorList[0] = colors[2];
            colorList[1] = colors[3];
            colorList[2] = colors[5];
        }

        return colorList[Random.Range(0, 2)];


    }

    public string ColorToString(Color color)
    {
        if (color.Equals(colors[0]))
        {
            return "red";
        }
        else if (color.Equals(colors[1]))
        {
            return "yellow";
        }
        else if (color.Equals(colors[2]))
        {
            return "blue";
        }
        else if (color.Equals(colors[3]))
        {
            return "green";
        }
        else if (color.Equals(colors[4]))
        {
            return"orange";
        }
        else if (color.Equals(colors[5]))
        {
            return "purple";
        }
        else
        {
            return "other color";
        }
    }

}
