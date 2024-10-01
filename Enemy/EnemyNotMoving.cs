using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class EnemyNotMoving : MonoBehaviour
{
   
    [SerializeField] private Transform enemyPosition;
   
    private Vector2 enemyPosition2D;
    private Vector3 enemyRotationDirection;
    public Animator anim;
    [HideInInspector] public bool enemyFacingLeft;
    //Variables needed to make enemies notice if the player is behind them and to shoot, if player is near enough.
    [HideInInspector] public bool ItsTimeToShoot;
    [SerializeField] private float enemyShootRadius;
    private LayerMask playerLayer;
    [SerializeField] private float enemyNoticeRadius;
    [SerializeField] private float enemyNoticeInterval;
    private bool noticePlayer;
    private bool enter;


    void Awake()
    {
        playerLayer = LayerMask.GetMask("Player");
        ItsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
    }

    void Start()
    {

        //Initialize the variables to check if the player is near.
        enemyPosition2D = enemyPosition.position;
        InvokeRepeating("NoticePlayer", 0.5f, 0.1f);
    }

    void Update()
    {
       
        EnemyShootCheck();
        enemyPosition2D = enemyPosition.position;
    }

    void RotateEnemy()
    {
        transform.rotation *= Quaternion.FromToRotation(transform.right, enemyRotationDirection);
    }

    //Coroutine to introduce delay into the rotation of the enemy if it notices the player behind it.
    IEnumerator TurningDelay(float waitTime)
    {
        enter = true;
        yield return new WaitForSeconds(waitTime);
        RotateEnemy();
        enter = false;
    }

    //Method to check if player is behing the enemy.
    void NoticePlayer()
    {
        noticePlayer = Physics2D.Raycast(enemyPosition2D, transform.right * (-1), enemyNoticeRadius, playerLayer);
        if (!enter && noticePlayer)
        {
            StartCoroutine(TurningDelay(enemyNoticeInterval));
            anim.SetBool("PlayerSeen", true);
        }
        if (!enter && !noticePlayer)
        {
            StopCoroutine(TurningDelay(enemyNoticeInterval));
            anim.SetBool("PlayerSeen", false);
        }
    }


    //Method to check if enemy is near enough for the enemy to start shooting.
    void EnemyShootCheck()
    {
        ItsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
    }
}
