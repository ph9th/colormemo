using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveSystem
{
    public static readonly string PLAYER_FOLDER = Application.persistentDataPath + "/Players/";



    public static void Init()
    {
        //check if folder exists
        if (!Directory.Exists(PLAYER_FOLDER))
        {
            //create folder
            Directory.CreateDirectory(PLAYER_FOLDER);
        }
    }

    public static void Save ()
    {
        try
        {
            for (int i = 0; i < 3; i++)
            {
                PlayerObject playerObj = new PlayerObject(PlayerManager.players[i].name)
                {
                    name = PlayerManager.players[i].name,
                    maxLevel = PlayerManager.players[i].maxLevel,
                    green = PlayerManager.players[i].green,
                    orange = PlayerManager.players[i].orange,
                    purple = PlayerManager.players[i].purple,


                };

                string json = JsonUtility.ToJson(playerObj);
                //Debug.Log("json: " + json);
                //SaveSystem.Save(json);

                File.WriteAllText(PLAYER_FOLDER + PlayerManager.players[i].name + ".txt", json);
            }
        }
        catch(Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
        

        
    }

    public static void LoadPlayer(string name)
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + "/Players/" + name + ".txt"))
            {
                string saveString = File.ReadAllText(Application.persistentDataPath + "/Players/" + name + ".txt");
                PlayerObject playerObj;

                //if file empty create new player object
                if (saveString != "")
                {

                    playerObj = JsonUtility.FromJson<PlayerObject>(saveString);
                }
                else
                {
                    Debug.LogError("player file empty");
                    playerObj = new PlayerObject(name);

                }

                int i = Array.FindIndex(PlayerManager.players, item => item.name == name);

                if (name == PlayerManager.players[i].name)
                {
                    PlayerManager.players[i].maxLevel = playerObj.maxLevel;
                    PlayerManager.players[i].green = playerObj.green;
                    PlayerManager.players[i].orange = playerObj.orange;
                    PlayerManager.players[i].purple = playerObj.purple;

                }
            }
            else
            {
                if (!Directory.Exists(Application.persistentDataPath + "/Players/"))
                {
                    //create folder
                    Directory.CreateDirectory(Application.persistentDataPath + "/Players/");
                }
                Debug.LogWarning("Player File doesn't exist");
                File.CreateText(Application.persistentDataPath + "/Players/" + name + ".txt");

            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
        
            
    }


}
