using System.Collections.Generic;
using UnityEngine;

public class VSMScript : MonoBehaviour
{
    public static List<string> levelOrder { get; set; }
    public static List<GameObject> characters { get; set; }
    string prefabFolder;
    public static int ErrorCounter { get; set; }
    public static int vsmTaskAssign { get; set; }

    // Start is called before the first frame update

    private void Awake()
    {
        //Specify folder for prefabs based on theme
        switch (SceneChange.ThemeID)
        {
            case 0:
                prefabFolder = "Children";
                break;
            case 1:
                prefabFolder = "Forest";
                break;
            case 2:
                prefabFolder = "Water";
                break;
            case 3:
                prefabFolder = "Space";
                break;
            case 4:
                prefabFolder = "Arctic";
                break;
            default:
                Debug.LogWarning("Unknown theme ID");
                break;
        }
    }
    void Start()
    {
        ErrorCounter = 0;

        GameObject.Find("Finger").GetComponent<SpriteRenderer>().enabled = false;

        DisplayCharacters(levelOrder);

        if (vsmTaskAssign == 0)
        {
            FindObjectOfType<AudioManager>().Play("RedBear");
        }
        else if (vsmTaskAssign == 1)
        {
            FindObjectOfType<AudioManager>().Play("YellowBear");
        }
        else if (vsmTaskAssign == 2)
        {
            FindObjectOfType<AudioManager>().Play("BlueBear");
        }
        StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("YourTurn", 1));
        StartCoroutine(FindObjectOfType<AudioManager>().PlayDelay("WhoFirst", 2));


    }

    /// <summary>Displays the characters for VSM task.</summary>
    /// <param name="array">The array.</param>
    void DisplayCharacters (List<string> array)
    {
        for (int i = 0; i< array.Count; i++)
        {
            //Get Prefab names by removing 'Level'(= last 5 characters) from Level Name
            var characterName = array[i].Substring(0, array[i].Length - 5);
            var sprite = Resources.Load<GameObject>("Prefabs/" + prefabFolder + "/" + characterName);

            characters.Add(sprite);
        }
        InstantiateCharacters(getRandomElement(characters));

    }


    /// <summary>Reorders the elements ina list in a random order.</summary>
    /// <param name="list">The list to choose from.</param>
    /// <returns>The reordered list.<br /></returns>
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

    /// <summary>Instantiates the characters.</summary>
    /// <param name="randomList">The randomly reordered list.</param>
    public void InstantiateCharacters (List<GameObject> randomList)
    {
        int counter = 1;
        

        foreach (GameObject character in randomList)
        {
            Instantiate(character, new Vector2(((Screen.width) / (randomList.Count + 1)) * counter, Screen.height / 2), Quaternion.identity);
 
            counter++;
            
        }
    }

}
