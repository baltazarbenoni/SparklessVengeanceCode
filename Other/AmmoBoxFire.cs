using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxFire : MonoBehaviour
{
   
    private GameObject Player;
    void Start()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject;
            Player.GetComponent<PlayerShoots>().GotAmmoBox = true;
            Destroy(gameObject);
        } 
    }
}
