using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class DataManagerScript
{
    public static int levelID { get; set; }
    public static int tryCounter { get; set; }
    public static float timeSuccess { get; set; }
    public static string curTaskColor { get; set; }

    static void Main (string [] args)
    {

    }
    public static void addRecord()
    {
        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/data.txt", true))
            {
                file.WriteLine(levelID + "," + curTaskColor + "," + tryCounter + "," + timeSuccess.ToString("0.00"));
            }
        }
        catch(Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }
}
