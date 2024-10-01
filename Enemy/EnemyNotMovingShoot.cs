using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class EnemyNotMovingShoot : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform enemyGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float enemyShootInterval;
    public EnemyNotMoving enemyNotMoving;
    public bool timeToShoot;
    [SerializeField] private bool shootTimer;
    private bool enter;
    public Animator anim;


    void Start()
    {
        enemyNotMoving = GetComponent<EnemyNotMoving>();
    }
    void Update()
    {
        timeToShoot = enemyNotMoving.ItsTimeToShoot;
        if (!enter && timeToShoot)
        {
            StartCoroutine(WaitAndShoot());
            anim.SetBool("SentriShooting", true);
        }
        else
        {
            anim.SetBool("SentriShooting", false);
        }
       
    }

    IEnumerator WaitAndShoot()
    {
        enter = true;
        GameObject spawnedBullet = Instantiate(enemyBullet, enemyGunMuzzle.position, Quaternion.identity);
        spawnedBullet.GetComponent<EnemyBullet>().Enemy = gameObject;
        yield return new WaitForSeconds(enemyShootInterval);
        enter = false;
    }
}
