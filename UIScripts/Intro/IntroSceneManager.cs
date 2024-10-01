using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class IntroSceneManager : MonoBehaviour
{
    //Game title, visible only after the intro speech.
    [SerializeField] private GameObject gameTitle;
    //Indicates in seconds for how long the title is visible before fading out.
    [SerializeField] private float titleBeforeFadeOut;
    [SerializeField] public float introFadeOutWait;
    [SerializeField] private Graphic introBlackBackground;
    public bool introEndingNow;
    void Start()
    {
        StartCoroutine(IntroFadeOut());
        introEndingNow = false;
    }

//Set the title unactive. Then wait until the introspeech has finished and make title visible.
//Then, slowly make the black background less and less transparent.
//Finally, load the next scene.
    IEnumerator IntroFadeOut()
    {
        gameTitle.SetActive(false);
        yield return new WaitForSeconds(introFadeOutWait - titleBeforeFadeOut);
        gameTitle.SetActive(true);
        yield return new WaitForSeconds(titleBeforeFadeOut);
        introEndingNow = true;
        while(introBlackBackground.color.a < 1.0f)
        {
            introBlackBackground.color += new Color(0, 0, 0, 0.6f);
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    void Update()
    {
        
    }
}
