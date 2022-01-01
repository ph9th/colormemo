using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GetColor : MonoBehaviour
{
    public Color penColor;

    readonly string getURL = "http://192.168.178.21/GetColor.php";

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

        if (www.isNetworkError || www.isHttpError)
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
            return new Color(1, 0, 0, 1);
            Debug.Log("color is red");
        }
        else if (colortext.CompareTo("blue") == 0)
        {
            return new Color(0, 0, 1, 1);
        }
        else
        {
            return new Color(0, 0, 0, 0);
            Debug.Log("other color " + colortext);
        }
    }

    void checkForUpdate ()
    {
        Debug.Log("Check for updates");
        StartCoroutine(SimpleGetRequest());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
