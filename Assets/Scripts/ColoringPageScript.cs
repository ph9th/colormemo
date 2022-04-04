using UnityEngine;

/// <summary>Handles the touch inputs for coloring pictures.</summary>
public class ColoringPageScript : MonoBehaviour
{
    SpriteRenderer obj;
    GameObject colorManager;
    Color32 penColor;

    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
    }

    void Update()
    {
        ColorThisObject(obj);
    }

    /// <summary>
    /// Colors object with pen color on touch input.
    /// </summary>
    /// <param name="obj">Object to color.</param>
    void ColorThisObject(SpriteRenderer obj)
    {
        penColor = colorManager.GetComponent<GetColor>().penColor;

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                    obj.color = penColor; //color object with pen color
            }
        }
    }

    //color object if there is mouse click
    void OnMouseDown()
    {
        obj.color = penColor; //color object with pen color
    }
}
