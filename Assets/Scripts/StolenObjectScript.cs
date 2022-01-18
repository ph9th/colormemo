using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StolenObjectScript : MonoBehaviour
{
    private Object[] textures;
    private GameObject stolenObj;
    public static Sprite sprite;

    void Start()
    {
        DataManagerScript.completedIterations = DataManagerScript.completedIterations + 1;

        stolenObj = this.gameObject;
        textures = Resources.LoadAll("Prefabs/VFCMObjects", typeof(Texture2D));

        // change texture
        Texture2D texture = (Texture2D)textures[Random.Range(0, textures.Length)];
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        //assign randomly chosen sprite to obj
        stolenObj.GetComponent<SpriteRenderer>().sprite = sprite;

        //store sprite for VFCM Level
        FoundObjectScript.stolenObjSprite = this.GetComponent<SpriteRenderer>().sprite;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
