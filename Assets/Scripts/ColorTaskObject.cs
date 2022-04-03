using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorTaskObject : MonoBehaviour
{
    Color32 TaskColor;

    void Start()
    {
        SpriteRenderer obj = GetComponent<SpriteRenderer>();
        GameObject colorManager = GameObject.Find("ColorManager");
        TaskColor = colorManager.GetComponent<SetColor>().TaskColor;
        ColorObj(obj);

        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            //Get the color the players had to memorize.
            StoredColors.stolenObj = TaskColor;
        }
    }

    /// <summary>
    /// Colors the object in the given task color.
    /// </summary>
    /// <param name="obj"> Object to be colored.</param>
    /// <returns></returns>
    void ColorObj(SpriteRenderer obj)
    {
        obj.color = TaskColor;
    }

}
