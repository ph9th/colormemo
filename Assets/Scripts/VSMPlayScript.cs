using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class VSMPlayScript : MonoBehaviour
{
     static int orderCounter = 0; //
    SceneChange SceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();
        orderCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Remove last x characters from given string
    /// </summary>
    /// <param name="stringToTrim">String to be trimmed</param>
    /// <param name="x"> Number of characters to remove</param>
    /// <returns>Trimmed string</returns>
    public string TrimString (string stringToTrim, int x)
    {
        return stringToTrim.Substring(0, stringToTrim.Length - x);
    }

    //select sprite on mouse click
    void OnMouseDown()
    {
        foreach(string i in VSMScript.levelOrder)
        {
        }

        if (TrimString(this.gameObject.name, 7 ) == TrimString(VSMScript.levelOrder[orderCounter], 5))
        {
            Debug.Log("order correct");
            
            orderCounter = orderCounter + 1;

            if (orderCounter == VSMScript.levelOrder.Count)
            {
                SceneManager.LoadScene("ObjectFound");

            }
            this.gameObject.SetActive(false);
        }

    }

}
