using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class AmmoCountScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private PlayerShoots playerShootingScript;
    private int ammoCount;
    private Text ammoCountOnScreen;
    
    void Start()
    {
        playerShootingScript = Player.GetComponent<PlayerShoots>();
        ammoCountOnScreen = GetComponent<Text>();
        InvokeRepeating("AmmoCountToTextBox", 0.5f, 0.1f);
    }
    void Update()
    {
    }

    void AmmoCountToTextBox()
    {
        ammoCount = playerShootingScript.AmmoCount;
        ammoCountOnScreen.text = ammoCount.ToString(); 

    }
}
