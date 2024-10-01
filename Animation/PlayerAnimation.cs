using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    
    public Animator anim;
    private PlayerMovement playerMovement;
    private Rigidbody2D playerRigidbody;
    private Vector2 moveDirection;
    private float moveY;
    
    
    
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        
        
    }
   

    void ProcessInputs()
    {
        
        float moveX = Input.GetAxisRaw("Horizontal");


        if (playerRigidbody.bodyType == RigidbodyType2D.Kinematic)
        {

            moveY = 0;
            
        }
        else
        {
            moveY = playerRigidbody.velocity.y;
        }
        
            
        
       
        
       moveDirection = new Vector2(moveX, moveY);
    }

    void Animate()
    {
        
        anim.SetFloat("SideWays", moveDirection.x);
        anim.SetFloat("UpAndDown", moveDirection.y);
        anim.SetFloat("PlayerMoving", moveDirection.magnitude);
    }

    
    void Update()
    {
        Animate();
        ProcessInputs();
    }

    void FixedUpdate()
    {
        playerRigidbody = playerMovement.playerBody;
        
    }
}
