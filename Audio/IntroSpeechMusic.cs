using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class IntroSpeechMusic : MonoBehaviour
{
    [SerializeField] private AudioSource introAudio;
    [SerializeField] private GameObject introSceneManager;

    void Start()
    {
        introAudio.Play();
    }

    void FixedUpdate()
    {
        if(introSceneManager.GetComponent<IntroSceneManager>().introEndingNow)
        {
            IntroMusicFadeOut();
        }
    }

    void IntroMusicFadeOut()
    {
        if(introAudio.volume > 0.01)
        {
            introAudio.volume -= 0.01f;
        }
        else
        {
            introAudio.Stop();
        }
    }
}
