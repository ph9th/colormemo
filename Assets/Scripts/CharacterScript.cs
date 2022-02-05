using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public static bool success;
    private bool lvldone;

    // Start is called before the first frame update
    void Start()
    {
        success = false;
        lvldone = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the object was correctly colored, replace sad character sprite with happy sprite
        if(success && !lvldone)
        {
            GameObject character = this.gameObject;
            Debug.Log("character" + character);
            string name = character.GetComponent<SpriteRenderer>().sprite.name;

            Sprite texture = Resources.Load<Sprite>("Prefabs/Happy/" + name + "_happy");

            // change texture
            //sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            character.GetComponent<SpriteRenderer>().sprite = texture;
            lvldone = true;
  
        }
    }

}
