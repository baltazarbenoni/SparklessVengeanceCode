using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class RainAudio : MonoBehaviour
{
    [SerializeField] private AudioSource rainSound;
    void Start()
    {
       rainSound = GetComponent<AudioSource>();
       rainSound.Play();
       StartCoroutine(DropAudioVolume());
    }
    IEnumerator DropAudioVolume()
    {
        yield return new WaitForSeconds(0.9f);
        float startVolume = rainSound.volume;
        while (startVolume > 0)
        {
            rainSound.volume -= startVolume * Time.deltaTime;
            yield return null;
        }
        rainSound.Stop();
        rainSound.volume = startVolume;

    }

    void Update()
    {
        
    }
}
