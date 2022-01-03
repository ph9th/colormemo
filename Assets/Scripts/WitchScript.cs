using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchScript : MonoBehaviour
{
    Animator anim;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GameObject.Find("witch").GetComponent<Animator>();

        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            anim.SetTrigger("Steal");
        }
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
