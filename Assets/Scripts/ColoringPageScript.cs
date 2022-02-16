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



    void Start()
    {
        //Debug.Log("assigned to: " + colorTaskAssign);
        // 
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
      

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

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null)
                {
                    obj.color = penColor; //color object with pen color
                }
            }
        }
    }



    //color object if there is mouse click
    void OnMouseDown()
    {
        Debug.Log("obj clicked: " + obj.name);
        obj.color = penColor; //color object with pen color


    }
}
