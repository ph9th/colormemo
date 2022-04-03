using System.Collections;
using UnityEngine;

/// <summary>Handles animations and sprites of the VFCM task.</summary>
public class FoundObjectScript : MonoBehaviour
{
    public static Sprite StolenObjSprite { get; set; }

    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().sprite = StolenObjSprite;
    }

    
    IEnumerator Start()
    {
        //Play animation for evil character.
        if(this.name == "objectFound")
        {
            ResetCollider(this.gameObject);
            GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Challenge");
            yield return new WaitForSeconds(1);
            GameObject.Find("witch").GetComponent<Animator>().ResetTrigger("Challenge");
            GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Idle");
        }
    }

    void ResetCollider(GameObject obj)
    {
        DestroyImmediate(obj.GetComponent<BoxCollider2D>());
        obj.AddComponent<BoxCollider2D>();
    }
}
