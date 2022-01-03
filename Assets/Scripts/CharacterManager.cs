using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{

    public static List<GameObject> characters = new List<GameObject>();
    //public GameObject[] children;
    Vector2 characterPos;

    void Awake()
    {
        //children = Resources.LoadAll<GameObject>("Prefabs/Children");
        //Debug.Log(children[0]);
    }
    // Start is called before the first frame update
    void Start()
    {
        //characterPos = GetComponent<Transform>().position;
        //AddCharacter();

    }

    // Update is called once per frame
    void Update()
    {

    }

   
}

