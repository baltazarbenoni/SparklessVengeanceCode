using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitVerification : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuObject;
    void Start()
    {

    }
    public void PauseMenuQuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }
    public void DoNotQuitGame()
    {
       PauseMenuObject.SetActive(true);
       this.gameObject.SetActive(false); 
    }
}
