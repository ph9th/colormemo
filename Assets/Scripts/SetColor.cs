using UnityEngine;
using UnityEngine.SceneManagement;

public class SetColor : MonoBehaviour
{
    public Color TaskColor;
    readonly Color[] colors = new Color[6];

    private void Awake()
    {
        colors[0] = new Color32(235, 30, 30, 255); //red
        colors[1] = new Color32(255, 247, 0, 255); //yellow
        colors[2] = new Color32(2, 78, 219, 255); //blue 
        colors[3] = new Color32(24, 196, 8, 255); //green
        colors[4] = new Color32(255, 154, 23, 255); //orange
        colors[5] = new Color32(181, 27, 242, 255); //purple

        setObjColor();
    }

    void setObjColor()
    {
        TaskColor = randomColor();
    }

    /// <summary>Chooses random color.</summary>
    /// <returns>The chosen color.<br /></returns>
    Color randomColor()
    {
        // choose colors based on who the task is assigned to
        // red can only paint red, orange, purple
        // yellow only yellow, green, orange
        Color[] colorList = new Color[3];

        //Color[] colorList = new Color[4]; //with brown

        float[] prob = new float[3];
        prob[0] = 0.2f;
        prob[1] = 0.4f;
        prob[2] = 0.4f;

        //Set colors according to which player's turn it is
        int assignedTo;

        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            assignedTo = VFCMScript.vfcmTaskAssign;
        }
        else
        {
            assignedTo = ColorObject.ColorTaskAssign;
        }

        if (assignedTo == 0)
        {
            colorList[0] = colors[0];
            colorList[1] = colors[4];
            colorList[2] = colors[5];

        }
        else if (assignedTo == 1)
        {
            colorList[0] = colors[1];
            colorList[1] = colors[3];
            colorList[2] = colors[4];
        }
        else if (assignedTo == 2)
        {
            colorList[0] = colors[2];
            colorList[1] = colors[3];
            colorList[2] = colors[5];
        }

        //choose color based on probability
        int c = (int)Choose(prob);
        return colorList[c];
    }

    float Choose(float[] probs)
    {
        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    /// <summary>Return color as color name string.</summary>
    /// <param name="color">The color.</param>
    /// <returns>The color name as a string.</returns>
    public string ColorToString(Color color)
    {
        if (color.Equals(colors[0]))
        {
            return "red";
        }
        else if (color.Equals(colors[1]))
        {
            return "yellow";
        }
        else if (color.Equals(colors[2]))
        {
            return "blue";
        }
        else if (color.Equals(colors[3]))
        {
            return "green";
        }
        else if (color.Equals(colors[4]))
        {
            return"orange";
        }
        else if (color.Equals(colors[5]))
        {
            return "purple";
        }
        else if (color.Equals(colors[6]))
        {
            return "brown";
        }
        else
        {
            return "other color";
        }
    }
}