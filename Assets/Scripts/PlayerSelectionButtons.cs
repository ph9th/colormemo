using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSelectionButtons : MonoBehaviour
{
    SceneChange scenechanger;
    private void Start()
    {
       scenechanger = GameObject.Find("SceneManager").GetComponent<SceneChange>();
    }
    //Player Management
    public void SaveData()
    {
        DataManagerScript.AddSessionData();
    }

    public void SavePlayer()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SaveSystem.Save();
        SaveData();
    }

    public void AddPlayer()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        StartCoroutine(GameObject.Find("PlayerManager").GetComponent<PlayerManager>().CreatePlayer());
    }

    //Submit new player name
    public void Submit()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        PlayerManager.Submitted = true;

    }

    /// <summary>Confirms the player selection.</summary>
    public void PlayerSelectionDone()
    {
        scenechanger.InitialiseGame();
        FindObjectOfType<AudioManager>().Play("Click");

        if (PlayerSlot.playerCount == 1)
        {
            string yellow = PlayerSlot.yellowSlotName;

            for (int i = 0; i < 3; i++)
            {
                PlayerManager.Players[i] = new PlayerObject(yellow);
                SaveSystem.LoadSinglePlayer(yellow, i);
            }

            SceneManager.LoadScene("MenuScreen");
            DataManagerScript.AddHeadings();

        }
        else if (PlayerSlot.playerCount == 3)
        {
            string red = PlayerSlot.redSlotName;
            string yellow = PlayerSlot.yellowSlotName;
            string blue = PlayerSlot.blueSlotName;

            if (red != null && yellow != null && blue != null)
            {
                SelectPlayer(0, red);
                SelectPlayer(1, yellow);
                SelectPlayer(2, blue);

                SceneManager.LoadScene("MenuScreen");
                DataManagerScript.AddHeadings();
            }
            else
            {
                Debug.LogWarning("Select 3 Players.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerCount Error");
        }
    }

    public void SetOnePlayer()
    {
        PlayerSlot.playerCount = 1;
        GameObject.Find("Blue").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Red").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BlueSlot").GetComponent<Image>().enabled = false;
        GameObject.Find("RedSlot").GetComponent<Image>().enabled = false;

    }

    public void SetThreePlayers()
    {
        PlayerSlot.playerCount = 3;
        GameObject.Find("Blue").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Red").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("BlueSlot").GetComponent<Image>().enabled = true;
        GameObject.Find("RedSlot").GetComponent<Image>().enabled = true;
    }

    public void SelectPlayer(int playerArrayID, string name)
    {
        PlayerManager.Players[playerArrayID] = new PlayerObject(name);
        SaveSystem.LoadPlayer(name);
    }
}
