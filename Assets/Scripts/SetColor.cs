using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        if( repeatColor.Equals(new Color32(0, 0, 0, 255)) )
        {

            taskColor = randomColor();
        }
        else
        {

            taskColor = repeatColor;
            repeatColor = new Color32(0, 0, 0, 255);
        }
        
    }
    Color randomColor()
    {
        return colors[Random.Range(0, 6)];

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
