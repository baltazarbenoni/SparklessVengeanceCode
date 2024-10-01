using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class IfPlayerDead : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private Health playerHealthScript;
    private Collider2D groundCollider;
    private PlayerMovement playerMovementScript;

    void Awake()
    {
        groundCollider = GetComponent<Collider2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        //If the player is dead, disable ground collider so that the player falls through the ground.
        playerHealthScript = Player.GetComponent<Health>();
        if(playerHealthScript.playerDead)
        {
            groundCollider.enabled = false;
        } 
    }
}
