using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class MusicManagerBaseLevel : MonoBehaviour
{
    [SerializeField] private AudioSource GameMusic;
    bool MusicPlaying;
    [SerializeField] Graphic musicButtonSprite;
    
    void Start()
    {
        MusicPlaying = true;
    }        

    void Update()
    {
        
    }

    public void ToggleMusicOnAndOff()
    {
        if (MusicPlaying)
        {
            GameMusic.Pause();
            MusicPlaying = false;
            musicButtonSprite.color = Color.blue;
        }
        else
        {
            GameMusic.Play();
            MusicPlaying = true;
            musicButtonSprite.color = Color.white;
        }
    }
   /* public void ToggleAllSoundsOnAndOff()
    {
        if(SoundsOn)
        {
            MainMixer.SetFloat("Volume", 0.0f);
            SoundsOn = false;
            GameMusic.Pause();
            soundsButtonSprite.color = Color.blue;
        }
        else
        {
            MainMixer.SetFloat("Volume", MainStartingVolume);
            SoundsOn = true;
            GameMusic.Play();
            soundsButtonSprite.color = Color.white;
        }
    } */
}
