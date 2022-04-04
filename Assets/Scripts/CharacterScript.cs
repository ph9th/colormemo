using UnityEngine;

/// <summary>Handles the animation of characters in the game.</summary>
public class CharacterScript : MonoBehaviour
{
    public static bool Success { get; set; }
    private bool lvldone;

    // Start is called before the first frame update
    void Start()
    {
        Success = false;
        lvldone = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if the object was correctly colored, replace sad character sprite with happy sprite
        if(Success && !lvldone)
        {
            if(SceneChange.ThemeID == 0 || SceneChange.ThemeID == 1 || SceneChange.ThemeID == 2)
            {
                GameObject character = this.gameObject;
                string name = character.GetComponent<SpriteRenderer>().sprite.name;
                Sprite texture = Resources.Load<Sprite>("Prefabs/Happy/" + name + "_happy");
                character.GetComponent<SpriteRenderer>().sprite = texture;
                lvldone = true;
            }
            else
            {
                lvldone = true;
            }
        }
    }
}
