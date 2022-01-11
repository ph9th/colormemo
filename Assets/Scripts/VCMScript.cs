using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VCMScript : MonoBehaviour
{
    public static List<GameObject> vcmObj = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        displayObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void displayObjects ()
    {
        GameObject stolenobj = new GameObject("stolenObject");
        RectTransform transform = stolenobj.AddComponent<RectTransform>();
        SpriteRenderer renderer = stolenobj.AddComponent<SpriteRenderer>();
        renderer.sprite = StolenObjectScript.sprite;
        stolenobj.transform.localScale = new Vector3(20, 20, 0);
        transform.pivot = renderer.bounds.center;


        vcmObj.Add(stolenobj);
         


        if (VSMScript.sandbox == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Objects/sandboxObj");
            vcmObj.Add(sprite);
        }
        if (VSMScript.garden == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Objects/gardenObj");
            vcmObj.Add(sprite);
        }
        if (VSMScript.amusement == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Objects/amusementObj");
            vcmObj.Add(sprite);
        }
        if (VSMScript.road == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Objects/roadObj");
            vcmObj.Add(sprite);
        }
        if (VSMScript.house == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Objects/houseObj");
            vcmObj.Add(sprite);
        }
        if (VSMScript.park == true)
        {
            var sprite = Resources.Load<GameObject>("Prefabs/Objects/parkObj");
            vcmObj.Add(sprite);
        }

        InstantiateObjects(getRandomElement(getObjects(vcmObj)));

    }

    List<GameObject> getObjects(List<GameObject> list)
    {

        List<GameObject> newList = new List<GameObject>();

        newList.Add(list[0]);
        int counter = 1; 
        while (counter <= 5 && list.Count != 1)
        {
            var randomNumber = Random.Range(1, list.Count);

            // add element in temporary list
            newList.Add(list[randomNumber]);
            Debug.Log("add newlist count" + newList.Count);

            //remove element from list
            list.RemoveAt(randomNumber);
            counter++;
        }
        Debug.Log("getobjects list: " + newList.Count);
        return newList;
    }

    List<GameObject> getRandomElement(List<GameObject> list)
    {

        // create a temporary list for storing
        // selected element
        List<GameObject> newList = new List<GameObject>();

        while (list.Count != 0)
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

    public void InstantiateObjects(List<GameObject> randomList)
    {
        int counter = 0;
        // number of max objects displayed
        int max;
        if (randomList.Count < 5) { max = randomList.Count; }
        else { max = 4; }
        
        while(counter < randomList.Count && counter < 4)
        {
            Vector2 vector = new Vector2((Screen.width / (max + 1)) * (counter + 1), Screen.height / 2);

            GameObject go = Instantiate(randomList[counter], vector, Quaternion.identity);

            go.AddComponent<BoxCollider2D>(); 
            go.AddComponent<VCMPlayScript>();
            
            counter++;
            

        }
        GameObject.Find("stolenObject").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("stolenObject(Clone)").transform.Translate(new Vector2(-100, -100)) ;
    }

    private void OnMouseDown()
    {

        //SceneManager.LoadScene("ObjectFound");
    }


}
