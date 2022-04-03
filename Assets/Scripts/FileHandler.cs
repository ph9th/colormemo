using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileHandler: MonoBehaviour
{
    /// <summary>Deletes all game files.</summary>
    public static void DeleteGameFiles()
    {
       string path = Application.persistentDataPath;

        System.IO.DirectoryInfo dirInfo = new DirectoryInfo(path);

        foreach (FileInfo file in dirInfo.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in dirInfo.GetDirectories())
        {
            dir.Delete(true);
        }
    
    }
}
