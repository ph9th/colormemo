using UnityEngine;
using UnityEngine.SceneManagement;


public class ColorObject : MonoBehaviour
{
    SpriteRenderer obj;
    GameObject colorManager;
    Color32 penColor;
    SetColor taskColorScript;
    Color32 curTaskColor;
    SceneChange SceneChanger;
    float timer;
    Color32 lastTryColor = new Color(0,0,0,255);
    public static int ColorTaskAssign { get; set; }
    int tryCounter;
    bool witchGone = false;

    void Start()
    {
        obj = GetComponent<SpriteRenderer>();
        colorManager = GameObject.Find("ColorManager");
        SceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();   
        taskColorScript = colorManager.GetComponent<SetColor>();
        curTaskColor = taskColorScript.TaskColor;
        tryCounter = 0;
    }

    void Update()
    {
        ColorThisObject(obj);
        timer += Time.deltaTime;
    }


    /// <summary>
    /// Colors object on touch input.
    /// </summary>
    /// <param name="obj"></param>
    void ColorThisObject(SpriteRenderer obj)
    {
        penColor = colorManager.GetComponent<GetColor>().penColor;
    
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider != null)
            {
                FindObjectOfType<AudioManager>().Play("Magic");
                obj.color = penColor; //color object with pen color

                //Increase try counter for every new coloring try
                if (!CompareColors(penColor, lastTryColor))
                {
                    tryCounter++;
                }
                lastTryColor = obj.color; //store used color to check if equal to the color of next touch input
                if (SceneManager.GetActiveScene().name == "ObjectFound")
                {
                    CheckObjFoundColor();
                }
                else
                {
                    CheckColor();
                }   
            }  
        }
    }


    /// <summary>
    /// Compare 2 colors.
    /// </summary>
    /// <param name="color1"></param>
    /// <param name="color2"></param>
    /// <returns>true if the same colors, false if not the same colors</returns>
    bool CompareColors(Color color1, Color color2)
    {
        if (color1.Equals(color2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Checks only for Scene 'Object Found' (VFCM task) if the color is correct.
    /// </summary>
    /// <returns></returns>
    void CheckObjFoundColor()
    {
        if (CompareColors(penColor, StoredColors.stolenObj))
        {
            FindObjectOfType<AudioManager>().PlayNoOverlay("WellDone");

            //store data
            float timeSuccess = timer;
            int MaxLevel = SceneChange.MaxLevel;
            bool hint = ButtonScript.Hint;
            DataManagerScript.AddVFCData(VFCMScript.vfcmTaskAssign, tryCounter, taskColorScript.ColorToString(StoredColors.stolenObj), timeSuccess, MaxLevel, hint);

            //assign next vfcm task to next player
            if (VFCMScript.vfcmTaskAssign < 2)
            {
                VFCMScript.vfcmTaskAssign++;
            }
            else
            {
                VFCMScript.vfcmTaskAssign = 0;
            }
            PlayerManager.Players[VFCMScript.vfcmTaskAssign].MaxLevel = MaxLevel;
            GameObject.Find("witch").GetComponent<Animator>().ResetTrigger("Fly");
            if (!witchGone)
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("WitchBeBack");
            }
            GameObject.Find("witch").GetComponent<Animator>().SetTrigger("Correct");


            //if no Error was made in both vsm and vfcm level, number of levels will increase in the next iteration
            if (!SceneChange.Error)
            {
                SceneChange.MaxLevel++;
                StolenObjectScript.StolenObjId++;

                //Update values for each player
                for(int i = 0; i<3; i++)
                {
                    if(StolenObjectScript.StolenObjId > PlayerManager.Players[i].StolenObjId)
                    {
                        PlayerManager.Players[i].StolenObjId = StolenObjectScript.StolenObjId;
                    }

                }

                if (tryCounter <= 1)
                {
                    StartCoroutine(SceneChanger.LoadDelay("Rewards", 3));
                }
            }
            else
            {
                if (SceneChange.MaxLevel > 2)
                {
                    SceneChange.MaxLevel--; 
                }
                StartCoroutine(SceneChanger.LoadDelay("ThemeSelection", 3));
            }
        }
        //decrease number of levels if error made
        else
        {
            SceneChange.Error = true;
            FindObjectOfType<AudioManager>().PlayNoOverlay("Wrong");
        }
    }


    /// <summary>
    /// Checks if the color is correct. If yes, records data in DataManager and loads next level.
    /// </summary>
    /// <returns></returns>
    void CheckColor()
    {
        //correct color
        if (CompareColors(penColor, taskColorScript.TaskColor))
        {
            if (!CharacterScript.Success)
            {
                FindObjectOfType<AudioManager>().PlayNoOverlay("Super");
                SceneChange.LevelCount++;
                SceneChange.SetOrder();
                CharacterScript.Success = true;
                bool hint = ButtonScript.Hint;

                //record color data in Data manager
                float timeSuccess = timer;
                DataManagerScript.AddColorData(ColorTaskAssign, tryCounter, taskColorScript.ColorToString(curTaskColor), timeSuccess, hint);

                //assign next level to next player
                if (ColorTaskAssign < 2)
                {
                    ColorTaskAssign++;
                }
                else
                {
                    ColorTaskAssign = 0;
                }
                PlayerManager.IncreaseColorCount(ColorTaskAssign, taskColorScript.ColorToString(curTaskColor), 1, 0);
                //load next level
                StartCoroutine(SceneChanger.LoadLevel());
                //StartCoroutine( SceneChanger.LoadDelay("TransitionScene", 1));
            }
        }
        else
        {
            PlayerManager.IncreaseColorCount(ColorTaskAssign, taskColorScript.ColorToString(curTaskColor), 1, 1);
            FindObjectOfType<AudioManager>().PlayNoOverlay("Wrong");
        }
    }

    //color object if there is mouse click
    void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("Magic");
        obj.color = penColor; //color object with pen color
        if (!CompareColors(penColor, lastTryColor))
        {
            tryCounter++;
        }
        lastTryColor = obj.color; //store used color to check if equal to the color of next touch input
                                  //Increase try counter for every new coloring try


        if (SceneManager.GetActiveScene().name == "ObjectFound")
        {
            CheckObjFoundColor();
        }
        else
        {
            CheckColor();
        }

    }
}
