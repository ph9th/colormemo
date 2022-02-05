using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FramesScript : MonoBehaviour
{
    GameObject picFrame;
    Sprite texture;

    private void Awake()
    {
        //Display screenshot taken of each colored coloring page level

        
    }

    private void Start()
    {
        AssetDatabase.Refresh();

        picFrame = this.gameObject;
        string frameID = picFrame.name;

        texture = Resources.Load<Sprite>("Screenshots/" + SaveSystem.name + "/" + "ColoringPage" + frameID);

        if (texture == null)
        {
            Resources.Load<Sprite>("ColoringPages/" + frameID);
        }
        else
        {
            picFrame.GetComponent<Image>().sprite = texture;
        }
    }

    private void Update()
    {
   
    }


}
