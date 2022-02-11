using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BearScript : MonoBehaviour
{
    float xPos;

    float moveX;

    Animator anim;

    GameObject bear;

    private bool walk; //set true until bear has reached original position

    char direction; 

    const float speed = 1;

    SpriteRenderer _renderer;

    private void Awake()
    {
       
        bear = this.gameObject;
        _renderer = bear.GetComponent<SpriteRenderer>();
        xPos = bear.transform.position.x;

        if(xPos < 960)
        {
            moveX = -800;
            direction = 'L'; //if L, bear walks in from the left; 
        } else
        {
            moveX = 800;
            direction = 'R'; //if R, bear walks in from the right
            _renderer.flipX = true;
        }
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
        if (walk)
        {

            switch (direction)
            {
                case 'L':
                    if (bear.transform.position.x >= xPos)
                    {
                        anim.ResetTrigger("Walk");
                        walk = false;
                    }
                    else
                    {
                        bear.transform.position += new Vector3(1 * speed, 0, 0);
                    }
                    break;

                case 'R':
                    if (bear.transform.position.x <= xPos)
                    {
                        anim.ResetTrigger("Walk");
                        walk = false;
                    }
                    else
                    {
                        bear.transform.position -= new Vector3(1 * speed, 0, 0);
                    }
                    break;
            }


        }
        
    }
}
 