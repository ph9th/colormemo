using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class VSMScript : MonoBehaviour
{
    public static List<GameObject> characters = new List<GameObject>();
    //public GameObject[] children;
    Vector2 characterPos;

    //levels
    public static bool sandbox;
    public static bool garden;
    public static bool amusement;
    public static bool road;
    public static bool house;
    public static bool park;

    public static List<string> levelOrder = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        setLevelTrue();
        displayCharacters();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    void displayCharacters ()
    {
        
        if (sandbox == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Children/sandbox");
            characters.Add(sprite);
        }
        if (garden == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Children/garden");
            characters.Add(sprite);
        }
        if (amusement == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Children/amusement");
            characters.Add(sprite);
        }
        if (road == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Children/road");
            characters.Add(sprite);
        }
        if (house == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Children/house");
            characters.Add(sprite);
        }
        if (park == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Children/park");
            characters.Add(sprite);
            Debug.Log("park true");
        }
        Debug.Log("character list count: " + characters.Count);
        InstantiateCharacters(getRandomElement(characters));
        
    }

    public List<GameObject> getRandomElement(List<GameObject> list)
    {

        // create a temporary list for storing
        // selected element
        List<GameObject> newList = new List<GameObject>();

        while(list.Count != 0)
        {
            var randomNumber = Random.Range(0, list.Count);

            // add element in temporary list
            newList.Add(list[randomNumber]);

            //remove element from list
            list.RemoveAt(randomNumber);
        }

        Debug.Log("New list count: " + newList.Count);
        return newList;
    }


    void InstantiateCharacters (List<GameObject> randomList)
    {
        int counter = 1;
        

        foreach (GameObject character in randomList)
        {
            Instantiate(character, new Vector2(((Screen.width - 100) / (randomList.Count + 1)) * counter, Screen.height / 2), Quaternion.identity);
            counter++;
            
        }
    }

    void setLevelTrue ()
    {
        for(int i = 0; i < levelOrder.Count; i++)
        {
            if(levelOrder[i] == "SandboxLevel") { sandbox = true; }
            if (levelOrder[i] == "GardenLevel") { garden = true; }
            if (levelOrder[i] == "ParkLevel") { park = true; }
            if (levelOrder[i] == "RoadLevel") { road = true; }
            if (levelOrder[i] == "AmusementParkLevel") { amusement = true; }
            if (levelOrder[i] == "HouseLevel") { house = true; }

        }
    }
}
