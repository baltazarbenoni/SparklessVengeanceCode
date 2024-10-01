using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class EnemyJumpedToDeath : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private AudioSource enemyDeathScream;
    private EnemyHealth healthScript;
    
    void Start()
    {
        healthScript = GetComponent<EnemyHealth>();
    }

    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerFeet") && !healthScript.enemyDead)
        {
            healthScript.enemyDead = true;
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        enemyDeathScream.Play();
        while(enemyDeathScream.isPlaying)
        { yield return null; }
        Destroy(Enemy);
    }

}
