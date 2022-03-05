using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SetUrlScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadURL();
    }

    void ReadURL()
    {
        try
        {
            if (!File.Exists(Application.persistentDataPath + "/URL.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/URL.txt");
            }

            // Read the file and display it line by line.  
            string url = File.ReadAllText(Application.persistentDataPath + "/URL.txt");

            GetColor.getURL = url;
            Debug.Log("GetURL: "+  GetColor.getURL);

            // Suspend the screen.  
            System.Console.ReadLine();
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }
    }

}
