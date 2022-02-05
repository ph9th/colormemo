using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject Content;
    List<string> names;

    int x = 400;
    int y = 800;
    int counter = 0;

    public TMP_InputField inputField;
    [HideInInspector]
    public static bool submitted = false;
    public GameObject panel;




    // Start is called before the first frame update
    private void Awake()
    {
        names = new List<string>();
        names = ReadNames(names);

    }
    void Start()
    {
        panel.SetActive(false);
        DisplayNames(names);
    }

    void DisplayNames(List<string> names)
    {
        
        foreach(string name in names)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(Content.transform, false);
            button.transform.position = new Vector3(x, y, 0);
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
            
            y = y - 150;
            if(counter == 3)
            {
                x = x + 500;
                y = 800;
                counter = 0;
            }else
            {
                counter++;
            }
            
        }

        
    }

    List<string> ReadNames(List<string> names)
    {
        int counter = 0;

        // Read the file and display it line by line.  
        foreach (string line in System.IO.File.ReadLines("Assets/playerNames.txt"))
        {
            System.Console.WriteLine(line);

            names.Add(line);
            counter++;
        }

        // Suspend the screen.  
        System.Console.ReadLine();
        return names;
    }
    public IEnumerator CreatePlayer()
    {
        panel.SetActive(true);


        yield return new WaitForSeconds(2);

        while (!submitted)
        {
            yield return null;
        }


        submitted = false;
        string name = inputField.text;
        GameObject.Find("Panel").SetActive(false);




        try
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("Assets/playerNames.txt", true))
            {
                file.WriteLine(name);
            }
        }
        catch (Exception ex)
        {
            throw new System.ApplicationException("Data error:", ex);
        }

        GameObject button = Instantiate(buttonPrefab);
        button.transform.SetParent(Content.transform, false);
        button.transform.position = new Vector3(x, y, 0);
        button.GetComponentInChildren<TextMeshProUGUI>().text = name;

        y = y - 150;
        if (counter == 3)
        {
            x = x + 500;
            y = 800;
            counter = 0;
        }
        else
        {
            counter++;
        }

    }

 
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }

   
}
