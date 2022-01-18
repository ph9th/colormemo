using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class VSMPlayScript : MonoBehaviour
{
    public static int orderCounter = 0;
    SceneChange SceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Converts the clones' names into the level names to set bools in VSM Level.
    /// </summary>
    /// <param name="prefab">prefab</param>
    /// <returns> levelName </returns>
    string convertToLevelName (string prefab)
    {
        string levelName = "";
        if (prefab == "sandbox(Clone)") { levelName = "SandboxLevel"; }
        if (prefab == "garden(Clone)") { levelName = "GardenLevel"; }
        if (prefab == "park(Clone)") { levelName = "ParkLevel"; }
        if (prefab == "road(Clone)") { levelName = "RoadLevel"; }
        if (prefab == "amusement(Clone)") { levelName = "AmusementParkLevel"; }
        if (prefab == "house(Clone)") { levelName = "HouseLevel"; }

        return levelName;
    }

    //select sprite on mouse click
    void OnMouseDown()
    {
        Debug.Log("VSM Sprite clicked");
        if (convertToLevelName(this.gameObject.name) == VSMScript.levelOrder[orderCounter])
        {
            orderCounter++;
            if (orderCounter == VSMScript.levelOrder.Count)
            {
                SceneManager.LoadScene("ObjectFound");

            }
            this.gameObject.SetActive(false);
        }

   
    }

}
