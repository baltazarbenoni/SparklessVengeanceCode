using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject QuitGameVerification;


   void Start()
   {
          QuitGameVerification.SetActive(false);
   }

   public void ResumeGame()
   {
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
   }
   public void QuitGame()
   {
        QuitGameVerification.SetActive(true);
        this.gameObject.SetActive(false);
   }
}

