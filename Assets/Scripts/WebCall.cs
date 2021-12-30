using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class WebCall : MonoBehaviour
{
    public Text colorText;
    public InputField newColor;

    readonly string getURL = "http://192.168.178.21/GetColor.php";
    readonly string postURL = "http://192.168.178.21/PostColor.php";

    // Start is called before the first frame update
    private void Start()
    {
        colorText.text = "Press button to send color";
    }

    public void OnButtonGetColor()
    {
        colorText.text = "Downloading data...";
        StartCoroutine(SimpleGetRequest());
    }

    IEnumerator SimpleGetRequest()
    {
        UnityWebRequest www = UnityWebRequest.Get(getURL);
        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            colorText.text = www.downloadHandler.text;
        }
    }

    public void OnButtonSendColor()
    {
        if (newColor.text == string.Empty)
        {
            colorText.text = "Error: no new color. Enter a value in the input field.";
        }
        else
        {
            colorText.text = "Sending data...";
            StartCoroutine(SimplePostRequest(newColor.text));
        }
    }

    IEnumerator SimplePostRequest(string newColor)
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("curColorKey", newColor));

        UnityWebRequest www = UnityWebRequest.Post(postURL, wwwForm);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            colorText.text = www.downloadHandler.text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
