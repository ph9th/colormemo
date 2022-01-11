using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VCMPlayScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("vcm obj clicked");
        if(this.gameObject.name == "stolenObject(Clone)")
        {
            SceneManager.LoadScene("ObjectFound");
        }
        else
        {

        }
    }
}
