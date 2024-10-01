using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpriteChange : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer currentSprite;

    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeBossSprite(int a)
    {
        currentSprite.sprite = sprites[a];


    }
    private void GetSprite()
    {

    }
}
