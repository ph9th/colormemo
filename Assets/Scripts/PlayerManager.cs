using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.IO;

/// <summary>Contains methods for managing players.</summary>
public class PlayerManager : MonoBehaviour
{
    GameObject buttonPrefab;
    GameObject Content;
    GameObject panel;
    TMP_InputField inputField;
    List<string> names;
    int x = 400;
    int y = 900;
    int counter = 0;
    [HideInInspector]
    public static bool Submitted { get; set; }

    /// <summary>Gets or sets the Players array.</summary>
    /// <value>The Players array.</value>
    public static PlayerObject[] Players { get; set; }  //index 0: red bear, index 1: yellow bear, index 2: blue bear

    private void Awake()
    {
        //load prefabs
        buttonPrefab = Resources.Load<GameObject>("Prefabs/NameButton");
        Content = GameObject.Find("Content");
        panel = GameObject.Find("Panel");
        inputField = panel.GetComponentInChildren<TMP_InputField>();
        names = new List<string>();
        names = ReadNames(names);
    }
    void Start()
    {
        Submitted = false;
        Players = new PlayerObject[3];
        panel.SetActive(false);
        DisplayNames(names);
    }

    /// <summary>Displays the existing player names.</summary>
    /// <param name="names">The names.</param>
    void DisplayNames(List<string> names)
    {
        
        foreach(string name in names)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(Content.transform, false);
            button.transform.position = new Vector3(x, y, -5);
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
            
            y -= 150;
            if(counter == 3)
            {
                x += 500;
                y = 800;
                counter = 0;
            }
            else
            {
                counter++;
            } 
        }
    }

    /// <summary>Reads the player names from the text file.</summary>
    /// <param name="names">The names.</param>
    /// <returns>
    ///   <br />
    /// </returns>
    /// <exception cref="System.ApplicationException">Data Error:</exception>
    List<string> ReadNames(List<string> names)
    {
        int counter = 0;
        try
        {
            if (!File.Exists(Application.persistentDataPath + "/playerNames.txt"))
            {
                File.CreateText(Application.persistentDataPath + "/playerNames.txt").Close(); 
            }
            // Read the file and display it line by line.  
            foreach (string line in System.IO.File.ReadLines(Application.persistentDataPath + "/playerNames.txt"))
            {
                System.Console.WriteLine(line);
                names.Add(line);
                counter++;
            }

            // Suspend the screen.  
            System.Console.ReadLine();
            return names;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }
    }

    /// <summary>Creates a new player.</summary>
    /// <exception cref="System.ApplicationException">Data Error:</exception>
    public IEnumerator CreatePlayer()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(2);
        while (!Submitted)
        {
            yield return null;
        }
        Submitted = false;
        string name = inputField.text;
        GameObject.Find("Panel").SetActive(false);

        try
        {
            using System.IO.StreamWriter file = new System.IO.StreamWriter(Application.persistentDataPath + "/playerNames.txt", true);
            file.WriteLine(name);
            file.Close();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Data Error:", ex);
        }

        GameObject button = Instantiate(buttonPrefab);
        button.transform.SetParent(Content.transform, false);
        button.transform.position = new Vector3(x, y, -5);
        button.GetComponentInChildren<TextMeshProUGUI>().text = name;

        // Adds the new name button to the current name button list.
        y -= 150;
        if (counter == 3)
        {
            x += 500;
            y = 800;
            counter = 0;
        }
        else
        {
            counter++;
        }
    }

    /// <summary>Calculates color error count (for green, orange, purple).</summary>
    /// <param name="assigned">The player who made the mistake.</param>
    /// <param name="color">The color.</param>
    /// <param name="counter">The counter.</param>
    /// <param name="Error">The error.</param>
    public static void IncreaseColorCount(int assigned, string color, int counter, int Error)
    {
        PlayerObject player = Players[assigned];

        switch (color)
        {
            case "green":
                player.green[0] = player.green[0] + counter;
                player.green[1] = player.green[1] + Error;
                player.green[2] = player.green[1] / player.green[0];
                break;
            case "orange":
                player.orange[0] = player.orange[0] + counter;
                player.orange[1] = player.orange[1] + Error;
                player.orange[2] = player.orange[1] / player.orange[0];
                break;
            case "purple":
                player.purple[0] = player.purple[0] + counter;
                player.purple[1] = player.purple[1] + Error;
                player.purple[2] = player.purple[1] / player.purple[0];
                break;
        }
    }
}
