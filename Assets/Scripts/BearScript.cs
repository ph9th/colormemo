using UnityEngine;

/// <summary>Handles the walking animation of a bear avatar.</summary>
public class BearScript : MonoBehaviour
{
    GameObject bear;
    /// <summary>The x position
    /// of the walking animation ending.</summary>
    private float xPos;
    /// <summary>X distance to move the bear GameObject out of frame.</summary>
    private float moveX;
    Animator anim;
    private bool walk; //set true until bear has reached original position
    char direction; 
    const float speed = 600;
    SpriteRenderer _renderer;

    /// <summary>Determines the position of the out of frame bear GameObject .</summary>
    private void Awake()
    {
        bear = this.gameObject;
        _renderer = bear.GetComponent<SpriteRenderer>();
        xPos = bear.transform.position.x;

        if(xPos < 960)
        {
            moveX = -800;
            direction = 'L'; //L means bear walks in from the left
        } else
        {
            moveX = 800;
            direction = 'R'; //R means bear walks in from the right
            _renderer.flipX = true;
        }
        walk = true;
    }

    /// <summary>Sets the trigger for walking animation start.</summary>
    private void Start()
    {
            bear.transform.Translate(new Vector3(moveX, 0, 0));
            anim = bear.GetComponent<Animator>();
            anim.SetTrigger("Walk");
    }

    /// <summary>Moves the bear GameObject until final x position is reached.</summary>
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
                        bear.transform.Translate(speed * Time.deltaTime * Vector3.right);
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
                        bear.transform.Translate(speed * Time.deltaTime * Vector3.left);
                    }
                    break;
            }
        } 
    }
}
 