using UnityEngine;
using System.IO;
using System;

public static class DataManagerScript
{
    public static int completedIterations { get; set; }
    /// <summary>Adds the headings for LOG files.</summary>
    /// <exception cref="System.ApplicationException">Data Error:</exception>
    public static void AddHeadings()
    {
        try
        {
            if (!File.Exists(Application.persistentDataPath + "/data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/data.txt").Close();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/data.txt", true))
            {
                file.WriteLine("\r\n" + "Date & Time: " + System.DateTime.Now);
                for (int i = 0; i< 3; i++)
                {
                    file.WriteLine(PlayerManager.Players[i].name);
                }
                file.WriteLine("MaxLevel" + ";" + "completedIterations");
            }

            if (!File.Exists(Application.persistentDataPath + "/color_data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/color_data.txt").Close();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath +  "/color_data.txt", true))
            {
                file.WriteLine("\r\n" +"Date & Time: " + System.DateTime.Now);
                for (int i = 0; i < 3; i++)
                {
                    file.WriteLine(PlayerManager.Players[i].name);
                }
                file.WriteLine("AssignedTo" + ";" + "tryCounter" + ";" + "TaskColor" + ";" + "timeSuccess" + ";" + "hintUsed");
            }

            if (!File.Exists(Application.persistentDataPath + "/vfc_data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/vfc_data.txt").Close();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vfc_data.txt", true))
            {
                file.WriteLine("\r\n" + "Date & Time: " + System.DateTime.Now);
                for (int i = 0; i < 3; i++)
                {
                    file.WriteLine(PlayerManager.Players[i].name);
                }
                file.WriteLine("AssignedTo" + ";" + "tryCounter" + ";" + "TaskColor" + ";" + "timeSuccess" + ";" + "LevelCount" + ";" + "hintUsed");
            }

            if (!File.Exists(Application.persistentDataPath + "/vsm_data.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/vsm_data.txt").Close();
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vsm_data.txt", true))
            {
                file.WriteLine("\r\n" + "Date & Time: " + System.DateTime.Now);
                for (int i = 0; i < 3; i++)
                {
                    file.WriteLine(PlayerManager.Players[i].name);
                }
                file.WriteLine("AssignedTo" + ";" + "ErrorCounter" + ";" + "timeSuccess" + ";" + "LevelCount" + ";" + "hintUsed");
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }
    }
    
    public static void AddSessionData()
    {
        int MaxLevel = SceneChange.MaxLevel;
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath +  "/data.txt", true))
            {
                file.WriteLine(MaxLevel + ";" + completedIterations);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }
    }


    /// <summary>
    /// Records data of each completed coloring task.
    /// </summary>
    /// <param name="assigned"></param>
    /// <param name="tryCounter"></param>
    /// <param name="TaskColor"></param>
    /// <param name="timeSuccess"></param>
    /// <param name="hint"></param>
    public static void AddColorData(int assigned, int tryCounter, string TaskColor, float timeSuccess, bool hint)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/color_data.txt", true))
            {
                file.WriteLine(assigned + ";" + tryCounter + ";" + TaskColor + ";" + timeSuccess + ";" + hint);
            }

        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }
    }

    /// <summary>
    /// Records data of each completed VFCM task.
    /// </summary>
    /// <param name="assigned"></param>
    /// <param name="tryCounter"></param>
    /// <param name="TaskColor"></param>
    /// <param name="timeSuccess"></param>
    /// <param name="LevelCount"></param>
    /// <param name="hint"></param>
    public static void AddVFCData(int assigned, int tryCounter, string TaskColor, float timeSuccess, int LevelCount, bool hint)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vfc_data.txt", true))
            {
                file.WriteLine(assigned + ";" + tryCounter + ";" + TaskColor + ";" + timeSuccess + ";" + LevelCount + ";" + hint);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }
    }

    /// <summary>
    /// Records data of each completed VSM task.
    /// </summary>
    /// <param name="assigned"></param>
    /// <param name="ErrorCounter"></param>
    /// <param name="timeSuccess"></param>
    /// <param name="LevelCount"></param>
    /// <param name="hint"></param>
    public static void AddVSMData(int assigned, int ErrorCounter, float timeSuccess, int LevelCount, bool hint)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/vsm_data.txt", true))
            {
                file.WriteLine(assigned + ";" + ErrorCounter + ";" + timeSuccess + ";" + LevelCount + ";" + hint);
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }
    }

}
