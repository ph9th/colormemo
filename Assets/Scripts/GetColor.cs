using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GetColor : MonoBehaviour
{
    public Color penColor;

    public static string getURL { get; set; }

    readonly Color[] colors = new Color[6];

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
        InvokeRepeating("CheckForUpdate", 1.0f, 0.5f);
    }

    IEnumerator SimpleGetRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(getURL))
        {
            yield return www.SendWebRequest();

            if ((www.result == UnityWebRequest.Result.ConnectionError) || (www.result == UnityWebRequest.Result.ProtocolError))
            {
                Debug.LogError(www.error);
                penColor = StringToColor("");
            }
            else
            {
                penColor = StringToColor(www.downloadHandler.text);
            }
        }
    }

    /// <summary>Converts color name string to a Color.</summary>
    /// <param name="colortext">The color name string.</param>
    /// <returns>
    ///   <br />
    /// </returns>
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
            return new Color(0, 0, 0, 255);
        }
    }

    void CheckForUpdate ()
    {
        StartCoroutine(SimpleGetRequest());
    }

}
