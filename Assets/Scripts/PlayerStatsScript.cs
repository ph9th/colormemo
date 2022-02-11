using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsScript : MonoBehaviour
{

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;

    public TextMeshProUGUI playername;
    public TextMeshProUGUI maxLevel;
    public TextMeshProUGUI greenRate;
    public TextMeshProUGUI orangeRate;
    public TextMeshProUGUI purpleRate;

    // Start is called before the first frame update
    void Start()
    {

        GetStats(Player1, 0);
        GetStats(Player2, 1);
        GetStats(Player3, 2);


        //playername.text = PlayerManager.players[0].name;
        //maxLevel.text = PlayerManager.players[0].maxLevel.ToString();
        //greenRate.text = PlayerManager.players[0].green[2].ToString();
        //orangeRate.text = PlayerManager.players[0].orange[2].ToString();
        //purpleRate.text = PlayerManager.players[0].purple[2].ToString();
    }


    private void GetStats(GameObject player, int playerID)
    {
        Transform name = player.transform.Find("Name");
        name.GetComponent<TextMeshProUGUI>().text = PlayerManager.players[playerID].name;

        Transform maxLevel = player.transform.Find("MaxLevel");
        maxLevel.GetComponent<TextMeshProUGUI>().text = PlayerManager.players[playerID].maxLevel.ToString();

        Transform greenRate = player.transform.Find("Green");
        greenRate.GetComponent<TextMeshProUGUI>().text = PlayerManager.players[playerID].green[2].ToString();

        Transform orangeRate = player.transform.Find("Orange");
        orangeRate.GetComponent<TextMeshProUGUI>().text = PlayerManager.players[playerID].orange[2].ToString();

        Transform purpleRate = player.transform.Find("Purple");
        purpleRate.GetComponent<TextMeshProUGUI>().text = PlayerManager.players[playerID].purple[2].ToString();
    }
}
