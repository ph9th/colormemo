using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public static bool success;

    // Start is called before the first frame update
    void Start()
    {
        success = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the object was correctly colored, replace sad character sprite with happy sprite
        if(success)
        {
            GameObject character = this.gameObject;
            string name = character.GetComponent<SpriteRenderer>().sprite.name;

            Sprite texture = Resources.Load<Sprite>("Prefabs/Children/" + name + "_happy");
            //Debug.Log("texture name: " + texture.name);

            // change texture
            //sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            //assign randomly chosen sprite to obj
            character.GetComponent<SpriteRenderer>().sprite = texture;
  
            success = false;
        }
    }


}
