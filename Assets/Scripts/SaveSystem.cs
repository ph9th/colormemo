using UnityEngine;
using System.IO;
using System;

public static class SaveSystem
{
    public static readonly string PLAYER_FOLDER = Application.persistentDataPath + "/Players/";

    /// <summary>Checks if folder with player name exists and creates if none exist.</summary>
    public static void Init()
    {
        //check if folder exists
        if (!Directory.Exists(PLAYER_FOLDER))
        {
            //create folder
            Directory.CreateDirectory(PLAYER_FOLDER);
        } 
    }

    /// <summary>Saves the player data as JSON string to a text file.</summary>
    /// <exception cref="System.ApplicationException">Data Error:</exception>
    public static void Save ()
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerObject playerObj = new PlayerObject(PlayerManager.Players[i].name)
                {
                    name = PlayerManager.Players[i].name,
                    MaxLevel = PlayerManager.Players[i].MaxLevel,
                    green = PlayerManager.Players[i].green,
                    orange = PlayerManager.Players[i].orange,
                    purple = PlayerManager.Players[i].purple,
                    StolenObjId = PlayerManager.Players[i].StolenObjId
                };

                if (PlayerSlot.playerCount == 1)
                {
                    playerObj.StolenObjId = PlayerManager.Players[0].StolenObjId;
                    for (int j = 1; j < 3; j++)
                    {
                        if (PlayerManager.Players[j].StolenObjId < playerObj.StolenObjId)
                        {
                            playerObj.StolenObjId = PlayerManager.Players[j].StolenObjId;
                        }
                    }
            }

                string json = JsonUtility.ToJson(playerObj);
                //SaveSystem.Save(json);

                File.WriteAllText(PLAYER_FOLDER + PlayerManager.Players[i].name + ".txt", json);
            }
            }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }
    }

    /// <summary>Loads the player data from a JSON string into a PlayerObject.</summary>
    /// <param name="name">The palyer's name.</param>
    /// <exception cref="System.ApplicationException">Data Error:</exception>
    public static void LoadPlayer(string name)
    {
        Init();
        try
        {
            if (!File.Exists(Application.persistentDataPath + "/Players/" + name + ".txt"))
            {
                File.CreateText(Application.persistentDataPath + "/Players/" + name + ".txt").Close();
            }
            string saveString = File.ReadAllText(Application.persistentDataPath + "/Players/" + name + ".txt");
            PlayerObject playerObj;

            //if file empty create new player object
            if (saveString != "")
            {

                playerObj = JsonUtility.FromJson<PlayerObject>(saveString);
            }
            else
            {
                playerObj = new PlayerObject(name);
            }

            int i = Array.FindIndex(PlayerManager.Players, item => item.name == name);

            if (name == PlayerManager.Players[i].name)
            {
                PlayerManager.Players[i].MaxLevel = playerObj.MaxLevel;
                PlayerManager.Players[i].green = playerObj.green;
                PlayerManager.Players[i].orange = playerObj.orange;
                PlayerManager.Players[i].purple = playerObj.purple;
                PlayerManager.Players[i].StolenObjId = playerObj.StolenObjId;
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }  
    }

    public static void LoadSinglePlayer(string name, int i)
    {
        Init();
        try
        {
            if (!File.Exists(Application.persistentDataPath + "/Players/" + name + ".txt"))
            {
                File.CreateText(Application.persistentDataPath + "/Players/" + name + ".txt").Close();
            }

            string saveString = File.ReadAllText(Application.persistentDataPath + "/Players/" + name + ".txt");
            PlayerObject playerObj;

            //if file empty create new player object
            if (saveString != "")
            {
                playerObj = JsonUtility.FromJson<PlayerObject>(saveString);
            }
            else
            {
                playerObj = new PlayerObject(name);
            }
            PlayerManager.Players[i].MaxLevel = playerObj.MaxLevel;
            PlayerManager.Players[i].green = playerObj.green;
            PlayerManager.Players[i].orange = playerObj.orange;
            PlayerManager.Players[i].purple = playerObj.purple;
            PlayerManager.Players[i].StolenObjId = playerObj.StolenObjId;
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data Error:", ex);
        }
    }
}
