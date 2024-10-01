using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAmmo : MonoBehaviour
{
    [SerializeField] GameObject FireBullet;
    [SerializeField] GameObject IceBullet;

    public GameObject ActiveAmmunation;
    private PlayerShoots playerShootsScript;
   void Start()
   {
        playerShootsScript = GetComponent<PlayerShoots>();
        ActiveAmmunation = FireBullet;
   }

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("AmmoBoxFire"))
        {
            ActiveAmmunation = FireBullet;
            playerShootsScript.GotAmmoBox = true;
        }

        if (collision.gameObject.CompareTag("AmmoBoxIce"))
        {
            ActiveAmmunation = IceBullet;
            playerShootsScript.GotAmmoBox = true;
        }
    }

}
