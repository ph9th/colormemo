using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoundObjectScript : MonoBehaviour
{
    public static Sprite stolenObjSprite;


    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = stolenObjSprite;
        
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        ResetCollider(this.gameObject);
        //GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Challenge");
        GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Challenge");
        yield return new WaitForSeconds(1);
        GameObject.Find("witch").GetComponent<Animator>().ResetTrigger("Challenge");
        GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Idle");

        
    }

    void ResetCollider(GameObject obj)
    {
        //if (obj.GetComponent<BoxCollider2D>().enabled)
        DestroyImmediate(obj.GetComponent<BoxCollider2D>());

        obj.AddComponent<BoxCollider2D>();
        //obj.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
