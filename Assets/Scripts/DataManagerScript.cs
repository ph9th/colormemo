using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class DataManagerScript
{
    public static int completedIterations { get; set; }

    public static void AddHeadings()
    {
        try
        {
            if (!File.Exists(Application.persistentDataPath + "/data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/data.txt");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/data.txt", true))
            {
                file.WriteLine("\r\n" + "Date & Time: " + System.DateTime.Now);
                for (int i = 0; i< 3; i++)
                {
                    file.WriteLine(PlayerManager.players[i].name);
                }
                
                file.WriteLine("maxLevel" + ";" + "completedIterations");
            }

            if (!File.Exists(Application.persistentDataPath + "/color_data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/color_data.txt");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath +  "/color_data.txt", true))
            {
                file.WriteLine("\r\n" +"Date & Time: " + System.DateTime.Now);
                for (int i = 0; i < 3; i++)
                {
                    file.WriteLine(PlayerManager.players[i].name);
                }
                file.WriteLine("AssignedTo" + ";" + "tryCounter" + ";" + "taskColor" + ";" + "timeSuccess" + ";" + "hintUsed");
            }

            if (!File.Exists(Application.persistentDataPath + "/vfc_data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/vfc_data.txt");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vfc_data.txt", true))
            {
                file.WriteLine("\r\n" + "Date & Time: " + System.DateTime.Now);
                for (int i = 0; i < 3; i++)
                {
                    file.WriteLine(PlayerManager.players[i].name);
                }
                file.WriteLine("AssignedTo" + ";" + "tryCounter" + ";" + "taskColor" + ";" + "timeSuccess" + ";" + "levelCount" + ";" + "hintUsed");
            }

            if (!File.Exists(Application.persistentDataPath + "/vsm_data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/vsm_data.txt");
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vsm_data.txt", true))
            {
                file.WriteLine("\r\n" + "Date & Time: " + System.DateTime.Now);
                for (int i = 0; i < 3; i++)
                {
                    file.WriteLine(PlayerManager.players[i].name);
                }
                file.WriteLine("AssignedTo" + ";" + "errorCounter" + ";" + "timeSuccess" + ";" + "levelCount" + ";" + "hintUsed");
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }
    
    public static void AddSessionData()
    {
        int maxLevel = SceneChange.maxLevel;
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath +  "/data.txt", true))
            {
                file.WriteLine(maxLevel + ";" + completedIterations);
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }


    /// <summary>
    /// Record data of each completed color level
    /// </summary>
    /// <param name="levelID"></param>
    /// <param name="tryCounter"></param>
    /// <param name="taskColor"></param>
    /// <param name="timeSuccess"></param>
    /// <returns></returns>
    public static void AddColorData(int assigned, int tryCounter, string taskColor, float timeSuccess, bool hint)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/color_data.txt", true))
            {
                file.WriteLine(assigned + ";" + tryCounter + ";" + taskColor + ";" + timeSuccess + ";" + hint);
            }

        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }

    public static void AddVFCData(int assigned, int tryCounter, string taskColor, float timeSuccess, int levelCount, bool hint)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vfc_data.txt", true))
            {
                file.WriteLine(assigned + ";" + tryCounter + ";" + taskColor + ";" + timeSuccess + ";" + levelCount + ";" + hint);
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }

    public static void AddVSMData(int assigned, int errorCounter, float timeSuccess, int levelCount, bool hint)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vsm_data.txt", true))
            {
                file.WriteLine(assigned + ";" + errorCounter + ";" + timeSuccess + ";" + levelCount + ";" + hint);
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }

}
