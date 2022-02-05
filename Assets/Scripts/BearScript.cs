using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearScript : MonoBehaviour
{
    float xPos;

    float moveX = -800;

    Animator anim;

    GameObject bear;

    private bool walk;

    float speed = 5;

    private void Awake()
    {
        bear = this.gameObject;
        xPos = bear.transform.position.x;
        Debug.Log(bear.name);
        Debug.Log("position: " + xPos);
        walk = true;

        
    }

    private void Start()
    {
        bear.transform.Translate(new Vector3(moveX, 0, 0));
        anim = bear.GetComponent<Animator>();
        anim.SetTrigger("Walk");
    }

    private void Update()
    {
        Debug.Log(bear.name);
        Debug.Log(bear.transform.position);
        if (walk)
        {
            if (bear.transform.position.x >= xPos)
            {
                Debug.Log("Reset" + bear.name);
                anim.ResetTrigger("Walk");
                walk = false;
            }
            else
            {
                bear.transform.position += new Vector3(1 * speed, 0, 0);
            }
        }
        
    }
}
 