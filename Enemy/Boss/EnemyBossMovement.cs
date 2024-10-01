using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossMovement : MonoBehaviour
{
    //Variables concerning the positions of the enemyboss.
    [SerializeField] private Transform playerPosition;
    private float bossAbovePlayerHeight;
    private float bossOnLevelWithPlayerHeight;
    [SerializeField] private float bossSpeed;
    private bool bossShooting;

    //Access to the boss-health -script.
    private bool bossHurt;
    private EnemyBossHealth bossHealthScript;
    
    // Variables to time the bossMovement.
    private float bossMovementTimer;
    [SerializeField] private float bossMovementInterval;
    private bool bossShouldMove;
    private float bossHurtDelay;

    void Start()
    {
        bossHealthScript = GetComponent<EnemyBossHealth>();
    }
    void Update()
    {
        bossHurt = bossHealthScript.bossHit;
        if(bossShouldMove && !bossHurt)
        {
            MoveEnemyBoss();
        }
    }
    
    void MoveEnemyBoss()
    {

    }

    void BossUpDownTransition()
    {

    }

    void BossBombDroppingMovement()
    {

    }



    void BossMovementTimer()
    {

    }
}
