using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static string name;
    public static readonly string PLAYER_FOLDER = Application.dataPath + "/Players/";

    public static void Init()
    {
        //check if folder exists
        if (!Directory.Exists(PLAYER_FOLDER))
        {
            //create folder
            Directory.CreateDirectory(PLAYER_FOLDER);
        }
    }

    public static void Save (string saveString)
    {
        File.WriteAllText(PLAYER_FOLDER + name + ".txt", saveString);
    }

    public static string Load ()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(PLAYER_FOLDER);
        FileInfo[] playerFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentFile = null;
        foreach(FileInfo fileInfo in playerFiles)
        {
            if(mostRecentFile == null)
            {
                mostRecentFile = fileInfo;
            }else
            {
                if(fileInfo.LastWriteTime > mostRecentFile.LastWriteTime)
                {
                    mostRecentFile = fileInfo;
                }
            }
        }
        if (mostRecentFile != null)
        {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            return saveString;
        }else
        {
            return null;
        }
    }
}
