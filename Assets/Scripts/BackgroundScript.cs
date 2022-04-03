using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject bg = this.gameObject;
        ChangeBackground(bg);

    }

    /// <summary>Loads the scene background based on the selected theme.</summary>
    /// <param name="obj">The background GameObject in the current scene.</param>
    void ChangeBackground(GameObject obj)
    {
        Sprite newbg;

        if (SceneChange.ThemeID == 0)
        {
            newbg = Resources.Load<Sprite>("Prefabs/BG/park_bg");
        }
        else if (SceneChange.ThemeID == 1)
        {
            newbg = Resources.Load<Sprite>("Prefabs/BG/forest_bg");
        }
        else if (SceneChange.ThemeID == 2)
        {
            newbg = Resources.Load<Sprite>("Prefabs/BG/water_bg");
        }
        else if (SceneChange.ThemeID == 3)
        {
            newbg = Resources.Load<Sprite>("Prefabs/BG/space_bg");
        }
        else if (SceneChange.ThemeID == 4)
        {
            newbg = Resources.Load<Sprite>("Prefabs/BG/arctic_bg");
        }
        else
        {
            newbg = Resources.Load<Sprite>("Prefabs/BG/field_bg");
        }

        obj.GetComponent<SpriteRenderer>().sprite = newbg;
    }
}
