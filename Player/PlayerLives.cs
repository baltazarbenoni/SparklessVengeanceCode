using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class PlayerLives : MonoBehaviour
{
    private Health playerHealth;
    [SerializeField] List<GameObject> playerLives = new List<GameObject>();
    private int playerLivesCount;
    //Updated life count, obtained from the Health-script.
    [SerializeField] private int playerLivesUpdate;
    private int maxLives;

    private void Awake()
    {
    }
    void Start()
    {
       //Get access to the Health-script.
        playerHealth = GetComponent<Health>();
        //Initialize variables.
        playerLivesUpdate = playerHealth.playerLives;
        playerLivesCount = playerLivesUpdate;
        maxLives = playerLivesUpdate;
        
    }

    void Update()
    {
        if(playerLivesCount > 0)
        {
            playerLivesUpdate = playerHealth.playerLives;

            //If the updated lives are less than the current value, make one heart disappear.
            if(playerLivesCount > playerLivesUpdate)
            {
                playerLives[playerLivesCount-1].SetActive(false);
                playerLivesCount--;
            }
            //If life count is not full and the updated lives are more than the currect value, make one heart more visible.
            if(playerLivesCount < maxLives && playerLivesCount < playerLivesUpdate)
            {
                playerLives[playerLivesCount].SetActive(true);
                playerLivesCount++;
            }
        } 
    }
}