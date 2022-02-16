using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class VSMPlayScript : MonoBehaviour
{
    public static int orderCounter = 0; //
    SceneChange SceneChanger;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();
        orderCounter = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
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
        FindObjectOfType<AudioManager>().Play("Magic");
        if (TrimString(this.gameObject.name, 7 ) == TrimString(VSMScript.levelOrder[orderCounter], 5))
        {
            //Debug.Log("order correct");
            
            GameObject.Find("Finger").GetComponent<SpriteRenderer>().enabled = false;
            FindObjectOfType<AudioManager>().PlayNoOverlay("RightAnswer");
            orderCounter = orderCounter + 1;
            this.gameObject.SetActive(false);

            

            if (orderCounter == VSMScript.levelOrder.Count)
            {
                DataManagerScript.AddVSMData(VSMScript.errorCounter, timer, SceneChange.maxLevel, ButtonScript.hint);
                SceneManager.LoadScene("ObjectFound");
            } else
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("WhoNext");
            }
            
            
        } else
        {
            VSMScript.errorCounter++;
            SceneChange.error = true;
            FindObjectOfType<AudioManager>().PlayNoOverlay("SomeoneElse");
        }

    }

}
