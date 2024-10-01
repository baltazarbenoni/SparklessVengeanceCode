using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject PauseWindow;
    void Start()
    {
    }

    void Update()
    {
        
    }
    public void PauseGame()
    {
        Time.timeScale = 0.00001f;
        PauseWindow.SetActive(true);
    }
}
