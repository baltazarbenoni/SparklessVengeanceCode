using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class SkipIntro : MonoBehaviour
{
    //Boolean to indicate if the key is pressed and another to block multiple entrances.
    private bool spaceKeyPressed;
    private bool spaceKeyNotYetPressed;
    //A public boolean to share with other classes.
    public bool skippingIntro;
    [SerializeField] private Graphic blackBackground;
    [SerializeField] private AudioSource introMusic;

    void Start()
    {
        spaceKeyNotYetPressed = true;
        spaceKeyPressed = Input.GetKeyDown(KeyCode.Space);
    }

    void Update()
    {
        spaceKeyPressed = Input.GetKeyDown(KeyCode.Space);
        //If space is pressed for the first time, start coroutine.
        if(spaceKeyPressed && spaceKeyNotYetPressed)
        {
            skippingIntro = true;
            StartCoroutine(FadeOutMusicAndImageAndSkip());
            spaceKeyNotYetPressed = false;
        }
        
    }

//Slowly fade out the music and make the black background less transparent.
//Then, load the next scene.
    IEnumerator FadeOutMusicAndImageAndSkip()
    {
        while(blackBackground.color.a < 1.0f || introMusic.volume > 0f)
        {
            introMusic.volume -= 0.1f;
            blackBackground.color += new Color(0f, 0f, 0f, 0.1f);
            yield return null;
        }
        SceneManager.LoadSceneAsync("PlayMode", LoadSceneMode.Single);
    }
}
