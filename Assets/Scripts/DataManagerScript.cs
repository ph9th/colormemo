using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class DataManagerScript
{
    public static int completedIterations { get; set; }

    static void Main (string [] args)
    {

    }
    /*public static void AddRecord()
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/data.txt", true))
            {
                file.WriteLine(levelID + "\t" + curTaskColor + "\t" + tryCounter + "\t" + timeSuccess.ToString("0.00"));
            }
        }
        catch(Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }*/

    public static void AddSessionData()
    {
        int maxLevel = SceneChange.maxLevel;
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/data.txt", true))
            {
                file.WriteLine(maxLevel + "\t" + completedIterations);
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
    public static void AddColorData(int levelID, int tryCounter, string taskColor, float timeSuccess)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/color_data.txt", true))
            {
                file.WriteLine(levelID + "\t" + tryCounter + "\t" + taskColor + "\t" + timeSuccess);
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }

    public static void AddVFCData(int tryCounter, string taskColor, float timeSuccess, int levelCount)
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/vfc_data.txt", true))
            {
                file.WriteLine(tryCounter + "\t" + taskColor + "\t" + timeSuccess + "\t" + levelCount);
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }

}
