using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReadColorScript : MonoBehaviour
{
    public Color colorText;
    public Text readColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        GetColor();
        
    }

    void GetColor()
    {
        string newColor = readColor.text;

        if (newColor == string.Empty)
        {
            Debug.Log("No color");
        }
        else if ( readColor.text.CompareTo("red") == 0)
        {
            colorText = new Color(1, 0, 0, 1);
            Debug.Log("color is red");
        }
        else if (readColor.text.CompareTo("blue") == 0)
        {
            colorText = new Color(0, 0, 1, 1);
        }
        else
        {
            Debug.Log(readColor.text);
        }
            
        
    }

}
