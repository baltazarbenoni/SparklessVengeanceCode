using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossHealth : MonoBehaviour
{
    //Make boss' status public so that other scripts can access these variables.
    public bool bossHit;
    public bool bossDead;
    //Boss' health and the delay for the time he is taking damage.
    [SerializeField] private int bossHealth;
    [SerializeField] private float bossDamageDelay;

    //Audio to be played while taking damage or dying.
    [SerializeField] private AudioSource bossAudioSource;
    private AudioClip bossHurtGroan;
    [SerializeField] private AudioClip bossDeathScream;

    void Start()
    {
        bossHurtGroan = bossAudioSource.clip;
    }
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerBullet"))
        {
            if(!bossHit)
            {
                bossHit = true;
                bossAudioSource.Play();
                bossHealth--;
                if(bossHealth <= 0) { StartCoroutine(BossDeath()); }
                else { StartCoroutine(BossTakingDamageDelay()); }
            }
        }
    }

    IEnumerator BossTakingDamageDelay()
    {
        yield return new WaitForSeconds(bossDamageDelay);
        bossHit = false;
    }

    IEnumerator BossDeath()
    {
        bossAudioSource.clip = bossDeathScream;
        bossAudioSource.Play();
        yield return new WaitForSeconds(bossDeathScream.length);
        Destroy(gameObject);
    }

}
