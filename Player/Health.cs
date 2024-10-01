using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class Health : MonoBehaviour
{
    //The time the playersprite flashes red when hit.
    [SerializeField] private float redTime;
    [HideInInspector] public bool playerDead;
    [SerializeField] private int playerMaxLives;
    //[HideInInspector]
    public int playerLives;
    private Rigidbody2D playerBody;
    private Transform playerPosition;
    //Where to spawn the player. Select betwrrn two options depending how far the player has advanced in the level.
    [SerializeField] private Transform spawnLocation;
    [SerializeField] private Transform spawnLocation2;
    //The location to choose between the two positions. 
    private Transform playerSpawnLocation;
    private SpriteRenderer playerSprite;
    private PlayerMovement playerMovement;
    //A boolean value to indicate whether the player is CURRENTLY losing a life.
    private bool playerLoseLife;
    private Collider2D playerCollider;
    [SerializeField] private GameObject gameOverWindow;
    [SerializeField] private AudioSource playerHurtAudio;

    void Awake()
    {
        //Fetching the necessary components.
        playerBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        playerPosition = GetComponent<Transform>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<Collider2D>();
        playerLives = playerMaxLives;
    }

    void Start()
    {
        gameOverWindow.SetActive(false);
        playerLoseLife = false;
        playerDead = false;
        playerHurtAudio.volume = 0.3f;
    }
    void Update()
    {
        playerDead = playerLives <= 0 ? true : false;
        //If the player falls for too long, execute the 'PlayerFallToDeath' function.
        if(playerPosition.position.y <= - 50f)
        {
            playerLives--;
            if(playerDead) { OutOfLives(); }
            else { StartCoroutine(PlayerFallToDeath()); }
        }
    }
    
    void OnTriggerEnter2D(Collider2D playerHitCollision)
    {
        //If the player is hit by 'EnemyBullet' and currently not losing a life (that is, flashing red), make him lose a life.
        if(!playerLoseLife && playerHitCollision.CompareTag("EnemyBullet"))
        {
            playerLives--;
            playerLoseLife = true;
            playerHurtAudio.Play();
            if(playerLives <= 0) { OutOfLives(); return; }
            else { StartCoroutine(PlayerLoseLifeGraphics()); }
        }

        //If player touches a health item, add 1 to health.
        if(playerLives < playerMaxLives && playerHitCollision.CompareTag("HealthItem"))
        {
            playerLives++;   
        }
    }

    IEnumerator PlayerLoseLifeGraphics()
    {
        //The player sprite turns red and returns to normal. Happens twice.
        for (int i = 0; i < 2; i++)
        {
            playerSprite.color = Color.red;
            yield return new WaitForSeconds(redTime);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(redTime);
        }
        playerLoseLife = false;
    }
    IEnumerator PlayerFallToDeath()
    {
    //Disable first the playerMovement-script. Then, transfer the gameObject to the spawn location and freeze its position
    //for a while. Player-sprite flashes on and off 2 times, then is dropped and the constraints of the rigidbody removed.
        playerCollider.enabled = true;
        FreezePlayer();
        playerSprite.color = Color.white;

        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.3f);
            playerSprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSprite.enabled = true;
        }
        UnFreezePlayer();
        playerBody.AddForce(Vector3.down * 3, ForceMode2D.Impulse);
    }
    //Disable first the playerMovement-script. Then, transfer the gameObject to the spawn location and freeze its position
    //for a while.
    void FreezePlayer()
    {
        playerMovement.enabled = false;
        playerPosition.position = PlayerSpawnPlace().position;
        playerBody.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    void UnFreezePlayer()
    {
        playerBody.constraints = RigidbodyConstraints2D.None;
        playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerMovement.enabled = true;
    }
    void OutOfLives()
    {
        Time.timeScale = 0.2f;
        playerMovement.enabled = false;
        gameOverWindow.SetActive(true);
        playerSprite.color = Color.red;
    }
    //Method to determine where to spawn the player.
    private Transform PlayerSpawnPlace()
    {
        playerSpawnLocation = playerPosition.position.x > spawnLocation2.position.x ?
                                spawnLocation2 : spawnLocation;
        return playerSpawnLocation;
    }
}
