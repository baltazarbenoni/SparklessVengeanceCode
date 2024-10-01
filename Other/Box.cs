using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Animator anim;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
           
            gameObject.GetComponent<Animator>().enabled = true;
    
           
        }
    }
}
