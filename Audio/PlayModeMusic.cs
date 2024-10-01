using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class PlayModeMusic : MonoBehaviour
{
    [SerializeField] AudioSource PlayModeMusicSource;
    [SerializeField] private AudioClip peliBiisi1;
    [SerializeField] private AudioClip gameOverEffect;
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip cockGunSound;
    [SerializeField] private GameObject Player;
    [SerializeField] private float musicStartingDelay;
    private Health playerHealthScript;
    private bool playerDead;
    private bool enter;

    void Awake()
    {
        PlayModeMusicSource.clip = cockGunSound;
        enter = false;
        playerHealthScript = Player.GetComponent<Health>();
    }
    void Start()
    {
        PlayModeMusicSource.Play();
        StartCoroutine(PlaySongWithDelay()); 

    }
    void Update()
    {
        playerDead = playerHealthScript.playerDead;
        if (playerDead && !enter)
        {
            StartCoroutine(GameOverSound());
        }
    }

    IEnumerator PlaySongWithDelay()
    {
        yield return new WaitForSeconds(musicStartingDelay);
        PlayModeMusicSource.clip = peliBiisi1;
        PlayModeMusicSource.volume = 1f;
        PlayModeMusicSource.Play();
        PlayModeMusicSource.loop = true;
    }
    IEnumerator GameOverSound()
    {
        PlayModeMusicSource.Stop();
        PlayModeMusicSource.clip = gameOverEffect;
        PlayModeMusicSource.loop = false;
        PlayModeMusicSource.Play();
        yield return new WaitForSeconds(3);
        PlayModeMusicSource.Stop();
        PlayModeMusicSource.clip = mainMenuMusic;
        PlayModeMusicSource.Play();
        PlayModeMusicSource.loop = true;
    }
}
