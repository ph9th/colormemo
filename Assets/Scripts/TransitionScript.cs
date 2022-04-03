using UnityEngine;

/// <summary>Contains the animation handling for the transition scene.</summary>
public class TransitionScript : MonoBehaviour
{
    Animator anim;
    GameObject bear;
    private bool walk; //set true until bear has reached original position
    const float speed = 2000;
    private void Awake()
    {
        bear = this.gameObject;
        walk = true;
    }

    private void Start()
    {
        SceneChange SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();
        GameObject witch = GameObject.Find("witch");
        witch.GetComponent<Animator>().SetTrigger("Fly");
        anim = bear.GetComponent<Animator>();
        anim.SetTrigger("Walk");
        StartCoroutine(SceneChanger.LoadLevel());
    }

    private void Update()
    {
        if (walk)
        {
            if (bear.transform.position.x >= Screen.width+400)
            {

                anim.ResetTrigger("Walk");
                walk = false;
                
            }
            else
            {
                bear.transform.Translate(speed * Time.deltaTime * Vector3.right);
            }
        }
        
    }
}
