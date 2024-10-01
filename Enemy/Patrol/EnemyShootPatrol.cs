using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class EnemyShootPatrol : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform enemyGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float enemyShootInterval;
    public EnemyMovement2 enemyMovement;
    public bool timeToShoot;
    [SerializeField] private bool shootTimer;
    private bool enter;
    private EnemyHealth enemyHealthScript;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement2>();
        enemyHealthScript = GetComponent<EnemyHealth>();
        InvokeRepeating("StopShootingIfDead", 2f, 0.2f);
    }
    void Update()
    {
        timeToShoot = enemyMovement.ItsTimeToShoot;

        ShootMethod();
    }
    void ShootMethod()
    {
        if (!enter && timeToShoot)
        {
            StartCoroutine(WaitAndShoot());
        }
    }

    void StopShootingIfDead()
    {
        if(enemyHealthScript.enemyDead) { this.GetComponent<EnemyShootPatrol>().enabled = false; }
    }

    IEnumerator WaitAndShoot()
    {
        enter = true;
        GameObject spawnedBullet = Instantiate(enemyBullet, enemyGunMuzzle.position, Quaternion.identity);
        spawnedBullet.GetComponent<EnemyBullet1>().Enemy = gameObject;
        spawnedBullet.GetComponent<Transform> ().rotation = this.transform.rotation;
        yield return new WaitForSeconds(enemyShootInterval);
        enter = false;
    }
}