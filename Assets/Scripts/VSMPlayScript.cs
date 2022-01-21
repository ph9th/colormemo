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
        Debug.Log("VSM Sprite clicked");

        //Debug.Log("obj name: " + TrimString(this.gameObject.name, 7));
        //Debug.Log("levelOrder name: " + TrimString(VSMScript.levelOrder[orderCounter], 5));

        foreach(string i in VSMScript.levelOrder)
        {
            Debug.Log("levelorder item: " + i);
        }

        Debug.Log("order counter ++: " + orderCounter);
        Debug.Log("content levelorder[ordercounter]: " + TrimString(VSMScript.levelOrder[orderCounter], 5));

        if (TrimString(this.gameObject.name, 7 ) == TrimString(VSMScript.levelOrder[orderCounter], 5))
        {
            Debug.Log("order correct");
            //Debug.Log("order counter: " + orderCounter);
            //Debug.Log("vsm levelorder counter: " + VSMScript.levelOrder.Count);
            
            orderCounter = orderCounter + 1;

            //Debug.Log("order counter ++: " + orderCounter);

            if (orderCounter == VSMScript.levelOrder.Count)
            {
                SceneManager.LoadScene("ObjectFound");

            }
            this.gameObject.SetActive(false);
        }

    }

}
