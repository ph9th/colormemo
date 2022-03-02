using System.Collections;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class StolenObjectScript : MonoBehaviour
{
    private Object[] textures;
    private GameObject stolenObj;
    public static Sprite sprite;

    public static string coloringPageId;
    private object n;

    void Start()
    {
        DataManagerScript.completedIterations = DataManagerScript.completedIterations + 1;

        stolenObj = this.gameObject;
        textures = Resources.LoadAll("Prefabs/VFCMObjects", typeof(Texture2D));

        int i = 0;
        // change texture
        Texture2D texture = (Texture2D)textures[i];

        for(int j = 0; j< textures.Length; j++)
        {
            Debug.Log("textures item " + j + " name: " + textures[j].name);
        }
        

        while (i < 10) {
            //if coloring page already exists, get new texture
            if (File.Exists(Application.persistentDataPath + "/Screenshots/" + PlayerManager.players[0].name + "/" + "ColoringPage" + texture.name + ".jpg"))
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
        Debug.Log("texture name: " + coloringPageId);
        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        //assign randomly chosen sprite to obj
        stolenObj.GetComponent<SpriteRenderer>().sprite = sprite;

        //store sprite for VFCM Level
        FoundObjectScript.stolenObjSprite = this.GetComponent<SpriteRenderer>().sprite;

        
    }



}
