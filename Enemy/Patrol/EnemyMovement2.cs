using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class EnemyMovement2 : MonoBehaviour
{
    //This is a second enemyMovement-script for the enemies who patrol between two blocks.

    [SerializeField] private float enemySpeed;
    private float enemyDirection;
    private bool enemyNeedsToTurnAround;
    [SerializeField] private Transform enemyPosition;
    private Vector2 enemyPosition2D;

    //Variables necessary to check if enemy is on the edge of the block, and those for rotating it if necessary.
    [HideInInspector] public bool enemyFacingRight;
    private LayerMask groundLayer;
    private Vector3 enemyRotationDirection;

    //Variables needed to make enemies notice if the player is behind them and to shoot, if player is near enough.
    [HideInInspector] public bool ItsTimeToShoot;
    [SerializeField] private float enemyShootRadius;
    private LayerMask playerLayer;
    [SerializeField] private float enemyNoticeRadius;
    private bool noticePlayer;
    [SerializeField] private float enemyNoticeInterval;
    [SerializeField] private GameObject enemyBack;
    private EnemyShotInTheBack enemyBackShotScript;
    private bool enemyShotInTheBack;
    private bool enter;
    [SerializeField] private AudioClip enemyNoticePlayerSound;
    private int enemyNoticePlayerCounter;
    [SerializeField] private AudioSource enemyAudioSource;
    public Animator anim;
    private EnemyHealth enemyHealthScript;

    void Awake()
    {
        groundLayer = LayerMask.GetMask("Ground");
        playerLayer = LayerMask.GetMask("Player");        
        enemyBackShotScript = enemyBack.GetComponent<EnemyShotInTheBack>();
        enemyHealthScript = GetComponent<EnemyHealth>();
    }
    
    void Start()
    {
        //Initialize the variables to check if the player is near.
        enemyPosition2D = enemyPosition.position;
        ItsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
        InvokeRepeating("NoticePlayer", 0.5f, 0.1f);
        InvokeRepeating("StopMovingIfDead", 0.5f, 0.2f);

    }

    void Update()
    {
        EnemyFacingRight();
        EnemyShootCheck();
        enemyPosition2D = enemyPosition.position;
        Animate();
    }
    void FixedUpdate()
    {
        if(!ItsTimeToShoot)
        {
            MoveEnemy();
        }
    }

    void EnemyFacingRight()
    {
        enemyFacingRight  = (enemyPosition.right == Vector3.right) ? true : false;
        //Initialize the rotation direction.
        enemyRotationDirection = enemyFacingRight ? Vector3.left : Vector3.right;
    }

    void StopMovingIfDead()
    {
        if(enemyHealthScript.enemyDead) { this.GetComponent<EnemyMovement2>().enabled = false; }
    }
    void EnemyOnEdge()
    {
        //Enemy needs to turn around when the Raycast returns 'true', meaning there is a wall in front of it.
        enemyNeedsToTurnAround = (Physics2D.Raycast(enemyPosition.position, transform.right, 1f, groundLayer));

        if(enemyNeedsToTurnAround)
        { RotateEnemy(); }
    }

    void RotateEnemy()
    {
        transform.rotation *= Quaternion.FromToRotation(transform.right, enemyRotationDirection);
    }

    void MoveEnemy()
    {
        EnemyOnEdge();
        enemyDirection = enemyFacingRight ? 1.0f : -1.0f;
        enemyPosition.position += new Vector3(enemyDirection*enemySpeed*Time.deltaTime, 0, 0);
    }

    //Coroutine to introduce delay into the rotation of the enemy if it notices the player behind it.
    IEnumerator TurningDelay(float waitTime)
    {
        enter = true;
        bool enemyCurrectDirection = enemyFacingRight;
        yield return new WaitForSeconds(waitTime);
        if(enemyFacingRight != enemyCurrectDirection)
        {yield break;}
        else { RotateEnemy(); }
        enter = false;
    }

    //Method to check if player is behing the enemy.
    void NoticePlayer()
    {
        if (enemyShotInTheBack) { RotateEnemy(); enemyShotInTheBack = false; }
        noticePlayer = Physics2D.Raycast(enemyPosition2D, transform.right * (-1), enemyNoticeRadius, playerLayer);
        if (!enter && noticePlayer)
        {
            StartCoroutine(TurningDelay(enemyNoticeInterval));
        }
        if (!enter && !noticePlayer)
        {
            StopCoroutine(TurningDelay(enemyNoticeInterval));
        }
    }

    //Method to check if enemy is near enough for the enemy to start shooting.
    void EnemyShootCheck()
    {
        ItsTimeToShoot = Physics2D.Raycast(enemyPosition2D, transform.right, enemyShootRadius, playerLayer);
    }

    void Animate()
    {
        if (ItsTimeToShoot)
        {
            anim.SetBool("Shoot", true);
        }
        else
        {
            anim.SetBool("Shoot", false);
        }
    }
}