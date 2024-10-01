using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyHealth;
    //A variable to indicate how much damage the enemy takes when it is hit.
    [SerializeField] private int enemyDamage;
    private Rigidbody2D enemyBody;
    //A boolean to indicate if the enemy is dead/currently dying.
    public bool enemyDead;
    //A boolean value to block the entrance into the 'death'-coroutine more than once.
    private bool enter;
    //Enemy audio-components.
    [SerializeField] private AudioClip enemyDeathScream;
    [SerializeField] private AudioClip enemyHurtAudio;
    [SerializeField]private AudioSource enemyAudio;

    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemyAudio.clip = enemyHurtAudio;
        enter = false;
    }

    void Update()
    {
        if(!enter)
        {
            if(enemyDead)
            {
                enter = true;
                PlayDyingAudio();
                StartCoroutine(EnemyDeath());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D enemyHitCollision)
    {
        if(enemyHitCollision.CompareTag("PlayerBullet"))
        {
            enemyHealth -= enemyDamage;

            if(enemyHealth <= 0)
            {
                enemyDead = true;
                //If enemy is dead, freeze it.
                enemyBody.constraints = RigidbodyConstraints2D.FreezePosition;
                enemyBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            if(!enemyAudio.isPlaying)
            {
                enemyAudio.Play();
            }
        }
    }

//Wait until the 'death scream' has ended, then destroy the enemy.
    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(enemyAudio.clip.length);
        Destroy(gameObject);
    }
//Make the enemy scream only once when it dies.
    void PlayDyingAudio()
    {
        enemyAudio.clip = enemyDeathScream;
        enemyAudio.volume = 1;
        int screamCounter = 0;
        if (screamCounter < 1)
        {
            screamCounter++;
            enemyAudio.Play();
        }
    }
}
