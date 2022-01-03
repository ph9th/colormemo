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

    // Start is called before the first frame update
    void Start()
    {
        AddCharacterToList();
        //displayCharacters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void AddCharacterToList()
    {
        //Instantiate(children[0]);
        //characters.Add(Instantiate(children[Random.Range(0, children.Length)], characterPos, Quaternion.identity));
        GameObject newCharacter = GameObject.Find("character");
        DontDestroyOnLoad(newCharacter);
        characters.Add(newCharacter);

        foreach (GameObject character in characters)
        {
            character.SetActive (false);
        }

        Debug.Log("characters List Length: " + characters.Count);

    }
    void displayCharacters ()
    {
        //Instantiate(CharacterManager.characters.First(), new Vector2((Screen.width-100)/ (CharacterManager.characters.Count + 1), Screen.height/2), Quaternion.identity);
    }
}
