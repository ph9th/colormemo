using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class ColoringPageScript : MonoBehaviour
{
    SpriteRenderer obj;
    GameObject colorManager;
    Color32 penColor;
    SceneChange SceneChanger;



    void Start()
    {
        //Debug.Log("assigned to: " + colorTaskAssign);
        // 
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
        SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();

    }

    void Update()
    {
        ColorThisObject(obj);


    }

    /// <summary>
    /// color object if there is touch input
    /// </summary>
    /// <param name="obj"></param>
    void ColorThisObject(SpriteRenderer obj)
    {
        penColor = colorManager.GetComponent<GetColor>().penColor;

        if (Input.touchCount > 0)

        {
            obj.color = penColor; //color object with pen color

        }
    }


    //color object if there is mouse click
    void OnMouseDown()
    {
        Debug.Log("obj clicked: " + obj.name);
        obj.color = penColor; //color object with pen color


    }
}
