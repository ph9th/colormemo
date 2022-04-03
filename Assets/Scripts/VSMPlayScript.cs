using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Contains methods handling the VSM task.</summary>
public class VSMPlayScript : MonoBehaviour
{
    public static int orderCounter { get; set; }
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        orderCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    /// <summary>
    /// Removes last x characters from given string.
    /// </summary>
    /// <param name="stringToTrim">String to be trimmed.</param>
    /// <param name="x"> Number of characters to remove.</param>
    /// <returns>Trimmed string.</returns>
    public string TrimString (string stringToTrim, int x)
    {
        return stringToTrim.Substring(0, stringToTrim.Length - x);
    }

    //select sprite on mouse click
    void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("Magic");
        if (TrimString(this.gameObject.name, 7 ) == TrimString(VSMScript.levelOrder[orderCounter], 5))
        { 
            GameObject.Find("Finger").GetComponent<SpriteRenderer>().enabled = false;
            FindObjectOfType<AudioManager>().PlayNoOverlay("RightAnswer");
            orderCounter = orderCounter + 1;
            this.gameObject.SetActive(false);
            bool hint = ButtonScript.Hint;

            if (orderCounter == VSMScript.levelOrder.Count)
            {
                DataManagerScript.AddVSMData(VSMScript.vsmTaskAssign, VSMScript.ErrorCounter, timer, SceneChange.MaxLevel, hint);
                //assign next task to next color
                if (VSMScript.vsmTaskAssign < 2)
                {
                    VSMScript.vsmTaskAssign++;
                }
                else
                {
                    VSMScript.vsmTaskAssign = 0;
                }
                SceneManager.LoadScene("ObjectFound");
            }
            else
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("WhoNext");
            }   
        }
        else
        {
            VSMScript.ErrorCounter++;
            SceneChange.Error = true;
            FindObjectOfType<AudioManager>().PlayNoOverlay("SomeoneElse");
        }
    }
}
