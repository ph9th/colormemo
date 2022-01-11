using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpScript : MonoBehaviour
{
    public Animator animRed;
    public Animator animYellow;
    public Animator animBlue;
    GameObject colorManager;
    SetColor taskColorScript;
    Color32 taskColor;

    // Start is called before the first frame update
    void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "VSMLevel" && sceneName != "VCMLevel") { 
            animRed = GameObject.Find("Red").GetComponent<Animator>();
            animYellow = GameObject.Find("Yellow").GetComponent<Animator>();
            animBlue = GameObject.Find("Blue").GetComponent<Animator>();
            colorManager = GameObject.Find("ColorManager");
            taskColorScript = colorManager.GetComponent<SetColor>();
            taskColor = colorManager.GetComponent<SetColor>().taskColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("help clicked");

        if (taskColorScript.ColorToString(taskColor) == "green")
            {
                animBlue.SetTrigger("Active");
                animYellow.SetTrigger("Active");

            }
            else if (taskColorScript.ColorToString(taskColor) == "orange")
            {
                animRed.SetTrigger("Active");
                animYellow.SetTrigger("Active");
            }
            if (taskColorScript.ColorToString(taskColor) == "purple")
            {
                animRed.SetTrigger("Active");
                animBlue.SetTrigger("Active");
            }
        }
}
