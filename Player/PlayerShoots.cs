using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class PlayerShoots : MonoBehaviour
{
    //This is the location where the bullet/projectile is created.
    [SerializeField] private Transform playerGunMuzzle;
    //The prefab gameobject to be initialized into the scene.
    [SerializeField] private GameObject playerBullet;
    
    //Check if shoot-key is pressed.
    [SerializeField] private bool playerShoots;

    //The amount of ammo player has.
    public int AmmoCount
    {
        get { return ammoCount; }
        set { ammoCount = value; }
    }
    private int ammoCount;
    public bool GotAmmoBox;
    [SerializeField] private int maxAmmo;
    [SerializeField] private AudioSource shootingAudio;
    [SerializeField] private AudioSource ammoAudio;

    void Start()
    {
        ammoCount = maxAmmo;
        shootingAudio.volume = 0.016f;
        ammoAudio.volume = 0.3f;
    }
    void Update()
    {
        Active();
        CreatePlayerBullet();
    }

    //Function to initialize the bullet prefab into the scene.
    void CreatePlayerBullet()
    {
        playerShoots = Input.GetKeyDown(KeyCode.V);
        //If shoot-key is pressed, initialize the bullet prefab into the specified location.
        //Also, create a reference to this gameobject into the 'PlayerBullet'-script of the initialized gameobject.
        //This is done in order to assign to the bullet the direction where the player is facing upon shooting.
        if(playerShoots && ammoCount > 0)
        {
            GameObject spawnedBullet = Instantiate(playerBullet, playerGunMuzzle.position, Quaternion.identity);

            spawnedBullet.GetComponent<PlayerBullet>().Player = gameObject;
            ammoCount--;
            shootingAudio.Play();
        }
        if(ammoCount < 0) { ammoCount = 0; }
    }
    
    void Active()
    {
        if(GotAmmoBox)
        {
            ammoAudio.Play();
            ammoCount = maxAmmo;
            GotAmmoBox = false;
        }
    }
}
