using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class VSMScript : MonoBehaviour
{
    public static int vsmTaskAssign = 0;
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

        if (vsmTaskAssign < 2)
        {
            vsmTaskAssign++;
        }
        else
        {
            vsmTaskAssign = 0;
        }

        DisplayCharacters(levelOrder);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void DisplayCharacters(List<string> array)
    {
        for (int i = 0; i< array.Count; i++)
        {
            var characterName = array[i].Substring(0, array[i].Length - 5);
            var sprite = Resources.Load<GameObject>("Prefabs/Children/" + characterName);

            characters.Add(sprite);
        }
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


        return newList;
    }


    public void InstantiateCharacters (List<GameObject> randomList)
    {
        int counter = 1;
        

        foreach (GameObject character in randomList)
        {
            Instantiate(character, new Vector2(((Screen.width - 100) / (randomList.Count + 1)) * counter, Screen.height / 2), Quaternion.identity);
            counter++;
            
        }
    }

}
