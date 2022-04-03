using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsScript : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;

    // Start is called before the first frame update
    void Start()
    {
        GetStats(Player1, 0);
        GetStats(Player2, 1);
        GetStats(Player3, 2);
    }


    /// <summary>Displays player statistics (highest level count, color error rates).</summary>
    /// <param name="player">The player gameobject.</param>
    /// <param name="playerID">The player ID.</param>
    private void GetStats(GameObject player, int playerID)
    {
        Transform name = player.transform.Find("Name");
        name.GetComponent<TextMeshProUGUI>().text = PlayerManager.Players[playerID].name;

        Transform MaxLevel = player.transform.Find("MaxLevel");
        MaxLevel.GetComponent<TextMeshProUGUI>().text = PlayerManager.Players[playerID].MaxLevel.ToString();

        Transform greenRate = player.transform.Find("Green");
        greenRate.GetComponent<TextMeshProUGUI>().text = PlayerManager.Players[playerID].green[2].ToString();

        Transform orangeRate = player.transform.Find("Orange");
        orangeRate.GetComponent<TextMeshProUGUI>().text = PlayerManager.Players[playerID].orange[2].ToString();

        Transform purpleRate = player.transform.Find("Purple");
        purpleRate.GetComponent<TextMeshProUGUI>().text = PlayerManager.Players[playerID].purple[2].ToString();
    }
}
