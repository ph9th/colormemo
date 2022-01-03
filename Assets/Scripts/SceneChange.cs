using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public static bool colorSuccess;
    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(10);
        // load the nextlevel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public IEnumerator Start()
    {
        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            CharacterManager.characters.Clear();
            yield return new WaitForSeconds(6);
            SceneManager.LoadScene("SandBoxLevel");
        }

        colorSuccess = false;
    }

    private void Update()
    {
        if(colorSuccess == true)
        {
            StartCoroutine(LoadYourAsyncScene());
        }
        
    }

    IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("VSMLevel");

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(GameObject.Find("character"), SceneManager.GetSceneByName("VSMLevel"));

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}