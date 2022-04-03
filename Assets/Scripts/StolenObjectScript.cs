using UnityEngine;

/// <summary>Contains method to choose and replace the sprite for the stolen object.</summary>
public class StolenObjectScript : MonoBehaviour
{
    public static Sprite sprite;
    /// <summary>The ID of the stolen object (= number of rewards collected).</summary>
    /// <value>The stolen object ID.</value>
    public static int StolenObjId { get; set; }

    /// <summary>Sets the sprite for the stolen object in the beginning scene of every iteration.</summary>
    void Start()
    {
        //Get smallest StolenObjId among all Players
        StolenObjId = PlayerManager.Players[0].StolenObjId;

        for (int i = 1; i < 3; i++)
        {
            if (PlayerManager.Players[i].StolenObjId < StolenObjId)
            {
                StolenObjId = PlayerManager.Players[i].StolenObjId;
            }
        }

        DataManagerScript.completedIterations = DataManagerScript.completedIterations + 1;

        GameObject stolenObj = this.gameObject;
        Object[] textures = Resources.LoadAll("Prefabs/VFCMObjects", typeof(Texture2D));

        // change texture
        Texture2D texture = (Texture2D)textures[StolenObjId];

        sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        //assign randomly chosen sprite to obj
        stolenObj.GetComponent<SpriteRenderer>().sprite = sprite;

        //store sprite for VFCM Level
        FoundObjectScript.StolenObjSprite = this.GetComponent<SpriteRenderer>().sprite;

        
    }



}
