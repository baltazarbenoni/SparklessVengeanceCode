using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class PlayerHealthAudio : MonoBehaviour
{
    [SerializeField] private AudioClip uuYeah;
    [SerializeField] private AudioClip thatsWhatImmaTalking;
    [SerializeField] private AudioClip mmHmm;
    [SerializeField] private AudioClip thatsANiceFish;
    [SerializeField] private AudioSource healthAudioSource;
    int i;

    void Start()
    {
        i = 2;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("HealthItem"))
        {
            PlayHealthAudio(ChooseAudioNumber());
            healthAudioSource.volume = 0.75f;
            healthAudioSource.Play();

        }
    }
    void PlayHealthAudio(int i)
    {
        switch(i) 
        {
            case 1:
                healthAudioSource.clip = uuYeah;
                break;
            case 2:
                healthAudioSource.clip = thatsWhatImmaTalking;
                break;
            case 3:
                healthAudioSource.clip = mmHmm;
                break;
            case 4:
                healthAudioSource.clip = thatsANiceFish;
                break;
        }
    }
    int ChooseAudioNumber()
    {
        if(i > 12) { i = 2; }
        i++;
        if(i % 4 == 0) { return 3; }
        if(i % 3 == 0) { return 2; }
        if(i % 5 == 0) { return 4;}
        else { return 1; }
    }
}
