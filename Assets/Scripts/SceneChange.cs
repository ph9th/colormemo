using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(2);
        // load the nextlevel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public IEnumerator Start()
    {
        if (SceneManager.GetActiveScene().name == "ObjectStolen")
        {
            Debug.Log("Level objectstolen");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("SandBoxLevel");
        }
    }

}