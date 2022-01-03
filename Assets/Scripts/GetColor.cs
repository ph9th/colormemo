using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GetColor : MonoBehaviour
{
    public Color penColor;

    readonly string getURL = "http://192.168.178.21/GetColor.php";

    Color[] colors = new Color[9];

    

    private void Awake()
    {
        colors[0] = new Color32(235, 30, 30, 255); //red
        colors[1] = new Color32(255, 247, 0, 255); //yellow
        colors[2] = new Color32(2, 78, 219, 255); //blue 
        colors[3] = new Color32(24, 196, 8, 255); //green
        colors[4] = new Color32(255, 154, 23, 255); //orange
        colors[5] = new Color32(181, 27, 242, 255); //purple
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SimpleGetRequest());
        InvokeRepeating("checkForUpdate", 5.0f, 5.0f);
    }

    IEnumerator SimpleGetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(getURL);
        yield return www.SendWebRequest();

        if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError)) 
        {
            Debug.LogError(www.error);
        }
        else
        {
            penColor = StringToColor(www.downloadHandler.text);
        }
    }

    Color StringToColor (string colortext)
    {
       if (colortext.CompareTo("red") == 0)
        {
            return colors[0];
        }
        else if (colortext.CompareTo("yellow") == 0)
        {
            return colors[1];
        }
        else if (colortext.CompareTo("blue") == 0)
        {
            return colors[2];
        }
        else if (colortext.CompareTo("green") == 0)
        {
            return colors[3];
        }
        else if (colortext.CompareTo("orange") == 0)
        {
            return colors[4];
        }
        else if (colortext.CompareTo("purple") == 0)
        {
            return colors[5];
        }
        else
        {
            //Debug.Log("other color " + colortext);
            return new Color(0, 0, 0, 0);
            
        }
    }

    

    void checkForUpdate ()
    {
        //Debug.Log("Check for updates");
        StartCoroutine(SimpleGetRequest());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
