using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ColorObject : MonoBehaviour
{
    SpriteRenderer obj;
    BoxCollider2D objCollider;
    GameObject colorManager;
    GetColor penColorScript;

    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        objCollider = GetComponent<BoxCollider2D>();
        colorManager = GameObject.Find("ColorManager");

    }

    // Update is called once per frame
    void Update()
    {
        colorThisObject(obj);
    }

    void colorThisObject (SpriteRenderer obj)
    {
        penColorScript = colorManager.GetComponent<GetColor>();

        if (Input.touchCount > 0)
        {
            Debug.Log("tapped");
            obj.color = penColorScript.penColor;
        }
    }

    void OnMouseDown()
    {
        //Debug.Log("Sprite Clicked");
        obj.color = penColorScript.penColor;
    }
}
