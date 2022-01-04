using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static int levelCounter = 0;
    private string sceneName;
    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(5);
        // load the nextlevel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public IEnumerator Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "ObjectStolen" && sceneName != "ObjectFound" && sceneName != "VSMLevel")
        {
            SetOrder();
        }
        //ObjectStolen level only has animation, no task
        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            VSMScript.characters.Clear();
            InitaliseBools();
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene("SandBoxLevel");
        }
        //SetBools();

        
        
    }

    private void Update()
    {
        
    }

    void InitaliseBools()
    {
        VSMScript.sandbox = false;
        VSMScript.amusement = false;
        VSMScript.garden = false;
        VSMScript.road = false;
        VSMScript.park = false;
        VSMScript.house = false;
    }

    /*void SetBools()
    {
        if(SceneManager.GetActiveScene().name == "SandboxLevel")
        {
            VSMScript.sandbox = true;
        }
        if (SceneManager.GetActiveScene().name == "AmusementParkLevel")
        {
            VSMScript.amusement = true;
        }
        if (SceneManager.GetActiveScene().name == "GardenLevel")
        {
            VSMScript.garden = true;
        }
        if (SceneManager.GetActiveScene().name == "RoadLevel")
        {
            VSMScript.road = true;
        }
        if (SceneManager.GetActiveScene().name == "ParkLevel")
        {
            VSMScript.park = true;
        }
        if(SceneManager.GetActiveScene().name == "HouseLevel")
        {
            VSMScript.house = true;
        }

    }*/

    void SetOrder ()
    {
        VSMScript.levelOrder.Add(SceneManager.GetActiveScene().name);
        levelCounter++;
        Debug.Log("levelcounter: " + levelCounter);
    }


}