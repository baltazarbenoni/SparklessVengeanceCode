using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class GameOverMenu : MonoBehaviour
{
    void Start()
    {
    
    }
    public void TryAgain()
    {
        Debug.Log("TryAgain");
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

    }
    public void Quit()
    {
        Debug.Log("Quit");
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

    }
}
