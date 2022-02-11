using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FramesScript : MonoBehaviour
{
    GameObject picFrame;
    private Sprite texture;

    private void Awake()
    {
        //Display screenshot taken of each colored coloring page level

        
    }

    private void Start()
    {
        AssetDatabase.Refresh();

        picFrame = this.gameObject;
        string frameID = picFrame.name;

        Debug.Log("path: " + Application.dataPath + "/Resources/Screenshots/" + PlayerManager.players[0].name + "/" + "ColoringPage" + frameID + ".jpg");

        if (File.Exists(Application.dataPath + "/Resources/Screenshots/" + PlayerManager.players[0].name + "/" + "ColoringPage" + frameID + ".jpg"))
        {
            
            texture = Resources.Load<Sprite>("Screenshots/" + PlayerManager.players[0].name + "/" + "ColoringPage" + frameID);
            Debug.Log("texture: " + texture);
        } else
        {
            Debug.Log("No screenshot exists.");
            texture = Resources.Load<Sprite>("ColoringPages/empty");
        }

        picFrame.GetComponent<Image>().sprite = texture;


    }

    private void Update()
    {
   
    }


}
