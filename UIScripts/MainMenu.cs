using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Graphic blackBackground;
    [SerializeField] private AudioSource mainMenuAudio;
    [SerializeField] private GameObject howToPlayImage;
    private bool onHowToPlayScreen;

    void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        onHowToPlayScreen = false;
        InvokeRepeating("ExitHowToPlayScreen", 2f, 0.5f);

    }
    public void PlayGame()
    {
        StartCoroutine(FadeOut());
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        howToPlayImage.SetActive(true);
        onHowToPlayScreen = true;
    }

    private void ExitHowToPlayScreen()
    {
        if (onHowToPlayScreen && Input.GetKey(KeyCode.Escape))
        {
            howToPlayImage.SetActive(false);
        }
    }
    public void ClickToExitHowToPlay()
    {
        howToPlayImage.SetActive(false);
    }
    
    public IEnumerator FadeOut()
    {

        while (blackBackground.color.a < 1)
        {
            blackBackground.color += new Color(0f, 0f, 0f, 0.06f);
            mainMenuAudio.volume -= 0.05f;
            yield return new WaitForSeconds(0.02f);
        }
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
    }
}
