using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class EnemySentryShoots : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform sentryGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject sentryBullet;
    //The time the sentry gun waits between shots.
    [SerializeField] private int burstShotCount;
    [SerializeField] private float animationTimeDelay;
    [SerializeField] private float shootBurstDelay; 
    [SerializeField] private float sentryShootInterval;
    [SerializeField] private float sentryNoticeDistance;
    [SerializeField] private AudioSource machineGunFire;
    [SerializeField] private CapsuleCollider2D CapsuleCollider2D;
    
    //'shootAfterPlayerDisappearsDelay' is the variable for how long the gun should keep shooting after the player disappears.
    //'delay' is the current waiting time which is compared to the variable 'shootAfterPlayerDisappearsDelay'.
    [SerializeField] private float shootAfterPlayerDisappearsDelay;
    float delay;
    private LayerMask playerLayer;
    //The boolean telling if the gun should shoot or not.
    [SerializeField] private bool gunShouldShoot;
    //boolean to block entering the coroutine while it's still executing.
    private bool waitBeforeShooting;
    //Boolean to tell the bullet which direction it should fly to.
    [HideInInspector] public bool enemyFacingRight;
    public Animator anim;

    void Start()
    {
        //Initialize variables and invoke the method to operate the gun (every x seconds).
        enemyFacingRight = false;
        waitBeforeShooting = false;
        //InvokeRepeating("SentryGunOperation", 3f, 0.5f);
        playerLayer = LayerMask.GetMask("Player");
        delay = 0;
        machineGunFire.volume = 0.15f;
    }
    void Update()
    {
        SentryGunOperation();
    }

    void SentryGunOperation()
    {
        if (SentryScan())
        {
            if(!waitBeforeShooting)
            {
                anim.SetBool("SentriShooting", true);
                StartCoroutine(WaitAndShoot());
            }
        }
        else
        {
        anim.SetBool("SentriShooting", false);
        }

    }
    //Coroutine to fire bullets every certain time interval.
    IEnumerator WaitAndShoot()
    {
        waitBeforeShooting = true;
        yield return new WaitForSeconds(animationTimeDelay);
        machineGunFire.Play();
        for(int i = 0; i < burstShotCount; i++)
        {
            GameObject spawnedBullet = Instantiate(sentryBullet, sentryGunMuzzle.position, Quaternion.identity);
            spawnedBullet.GetComponent<SentryBullet>().Enemy = gameObject;
            spawnedBullet.GetComponent<Transform>().rotation = this.transform.rotation;
            yield return new WaitForSeconds(shootBurstDelay);
        }
        yield return new WaitForSeconds(sentryShootInterval);
        animationTimeDelay = 0;
        waitBeforeShooting = false;
    }
    //Method to scan if the player is near enough to shoot the bullet or not. If the player has entered
    //the gun's field of vision and disappears, the gun will continue shooting for the prescribed time.
    private bool SentryScan()
    {
        //'timeToShootRaw' is the raw value for whether the player is visible. 'gunShouldShoot' dictates if the gun should shoot or not.

        bool timeToShootRaw = Physics2D.Raycast(transform.position, Vector3.right * (-1.0f), sentryNoticeDistance, playerLayer); 
        
        //If the 'gunShouldShoot' variable already equals the raycast result, return 'delay' to zero. 
        if (gunShouldShoot == timeToShootRaw)
        { delay = 0; }

        //If the raycast returns 'true', that is, if the player is visible, start shooting immidiately.
        if (timeToShootRaw)
        {
            if(!gunShouldShoot)
            {
                anim.SetBool("PlayerSeen", true);
                gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
            }
            gunShouldShoot = timeToShootRaw;
        }


        //If the raycast returns 'false' but 'gunShouldShoot' is true, keep shooting for a certain time
        //and only thereafter make 'gunShouldShoot' equal to the raycast.
        if(!timeToShootRaw && gunShouldShoot)
        {
            delay += Time.deltaTime * 10;
            if(delay > shootAfterPlayerDisappearsDelay)
            {
                gunShouldShoot = timeToShootRaw;
                anim.SetBool("PlayerSeen", false);
            }
        }
        return gunShouldShoot;
    }
}
