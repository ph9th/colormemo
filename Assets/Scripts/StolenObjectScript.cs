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

    public static int stolenObjId;

    void Start()
    {
        //Get smallest stolenObjId among all players
        stolenObjId = PlayerManager.players[0].stolenObjId;

        for (int i = 1; i < 3; i++)
        {
            if (PlayerManager.players[i].stolenObjId < stolenObjId)
            {
                stolenObjId = PlayerManager.players[i].stolenObjId;
            }
        }
        Debug.Log("stolenObjId: " + stolenObjId);

        DataManagerScript.completedIterations = DataManagerScript.completedIterations + 1;

        stolenObj = this.gameObject;
        textures = Resources.LoadAll("Prefabs/VFCMObjects", typeof(Texture2D));

        // change texture
        Texture2D texture = (Texture2D)textures[stolenObjId];

        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        //assign randomly chosen sprite to obj
        stolenObj.GetComponent<SpriteRenderer>().sprite = sprite;

        //store sprite for VFCM Level
        FoundObjectScript.stolenObjSprite = this.GetComponent<SpriteRenderer>().sprite;

        
    }



}
