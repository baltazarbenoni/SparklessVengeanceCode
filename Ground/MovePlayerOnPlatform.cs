using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class MovePlayerOnPlatform : MonoBehaviour
{
    public bool playerIsOnPlatform;
    [SerializeField] GameObject Player;
    private Rigidbody2D playerBody;
    [SerializeField] private CapsuleCollider2D triggerCollider;
    private Transform playerPosition;
    private PlayerMovement playerMovementScript;

    void Start()
    {
        playerBody = Player.GetComponent<Rigidbody2D>();
        playerPosition = Player.GetComponent<Transform>();
        triggerCollider = GetComponent<CapsuleCollider2D>();
        playerMovementScript = Player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if(playerIsOnPlatform)
        {
            CheckIfPlayerJumps();
        }
    }
    void CheckIfPlayerJumps()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            triggerCollider.isTrigger = false;
            StartCoroutine(JumpAndTriggerDelay());
        }
    }

    IEnumerator JumpAndTriggerDelay()
    {
        playerBody.isKinematic = false;
        yield return null;
        if(playerBody.velocity.y < 5)
        {
            playerBody.AddForce(Vector2.up * playerMovementScript.jumpForce, ForceMode2D.Impulse);
        }
        yield return null;
        yield return null;
        triggerCollider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            playerPosition.parent = this.transform;
            playerBody.isKinematic=true;
            playerIsOnPlatform = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlayerFeet"))
        {
            PlayerNoMoreOnPlatform();
        }
    }
    void PlayerNoMoreOnPlatform()
    {
        triggerCollider.isTrigger = true;
        playerPosition.parent = null;
        playerBody.isKinematic = false;
        playerIsOnPlatform = false;
    }
}
