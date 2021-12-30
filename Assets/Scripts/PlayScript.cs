using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayScript : MonoBehaviour
{
    SpriteRenderer volleyball;
    CircleCollider2D volleyballCollider;
    GameObject colorTextObj;
    ReadColorScript colorscript;

    // Start is called before the first frame update
    void Start()
    {
        volleyball = GetComponent<SpriteRenderer>();
        volleyballCollider = GetComponent<CircleCollider2D>();
        colorTextObj = GameObject.Find("ColorText");

    }

    // Update is called once per frame
    void Update()
    {
        colorscript = colorTextObj.GetComponent<ReadColorScript>();

        if (Input.touchCount > 0) {
            Debug.Log("tapped");
            volleyball.color = colorscript.colorText;
        }
    }


    void OnMouseDown()
    {
        //Debug.Log("Sprite Clicked");
        volleyball.color = colorscript.colorText;
    }
}
