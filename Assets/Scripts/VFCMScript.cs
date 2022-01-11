using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VFCMScript : MonoBehaviour
{
    public static Sprite stolenObjSprite;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sprite = stolenObjSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
