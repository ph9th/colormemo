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
        Debug.Log("sprite clicked");
        Debug.Log("levelorder: " + VSMScript.levelOrder[1]);
        Debug.Log("gameobj name: " + this.gameObject.name);
        Debug.Log("converted prefab name: " + convertToLevelName(this.gameObject.name)); 
        if (convertToLevelName(this.gameObject.name) == VSMScript.levelOrder[orderCounter])
        {
            orderCounter++;
            if (orderCounter == VSMScript.levelOrder.Count)
            {
                Debug.Log("done vsm");
                SceneManager.LoadScene("ObjectFound");
                //StartCoroutine(SceneChanger.LoadLevel());

            }
            this.gameObject.SetActive(false);
        }


        Debug.Log("ordercounter: " + orderCounter);
        Debug.Log("vsm char counter: " + VSMScript.levelOrder.Count);
        
    }

}
