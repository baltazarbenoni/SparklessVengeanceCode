using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] AudioSource MainMenuBiisi;

    void Start()
    {
       MainMenuBiisi = GetComponent<AudioSource>();
       MainMenuBiisi.loop = true; 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
