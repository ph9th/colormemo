using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class StolenObjectScript : MonoBehaviour
{
    private Object[] textures;
    private GameObject stolenObj;
    public static Sprite sprite;

    public static string coloringPageId;

    void Start()
    {
        DataManagerScript.completedIterations = DataManagerScript.completedIterations + 1;

        stolenObj = this.gameObject;
        textures = Resources.LoadAll("Prefabs/VFCMObjects", typeof(Texture2D));
        int i = 0;
        // change texture
        Texture2D texture = (Texture2D)textures[i];

        while (i < 10) { 
            //if coloring page already exists, get new texture
            if (File.Exists(Application.dataPath + "/Resources/Screenshots/" + PlayerManager.players[0].name + "/" + "ColoringPage" + texture.name + ".jpg"))
            {
                Debug.Log("Texture already used.");
                i++;
                texture = (Texture2D)textures[i];
            }
            else
            {
                Debug.Log("Found unused texture.");
                break;
            }
        }

        coloringPageId = texture.name;
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        //assign randomly chosen sprite to obj
        stolenObj.GetComponent<SpriteRenderer>().sprite = sprite;

        //store sprite for VFCM Level
        FoundObjectScript.stolenObjSprite = this.GetComponent<SpriteRenderer>().sprite;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
