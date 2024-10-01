using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class RockScript : MonoBehaviour
{
    [SerializeField] private Transform rockTrigger;
    [SerializeField] private GameObject rockSupport;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Rigidbody2D rockBody;
    private bool freeRock;
    private bool rockReleased;

      void Start()
    {
        freeRock = false;
        rockReleased = false;
    }

    void Update()
    {
        if (!rockReleased) { RealeaseRockIfTriggered(); }
        if (transform.position.y < -30) { Destroy(gameObject); }
        if(rockReleased)
        {
            StartCoroutine(DestroyRock());
        }
    }
    IEnumerator DestroyRock()
    {
        yield return new WaitForSeconds(3.0f);
        if(rockBody.velocity.x < 0.1)
        {
            Destroy(gameObject);
        }
    }

    void RealeaseRockIfTriggered()
    {
        freeRock = playerPosition.position.x > rockTrigger.position.x;
        if(freeRock)
        {
            Destroy(rockSupport);  rockReleased = true;
            rockBody.AddForce(Vector2.down * 3, ForceMode2D.Impulse);
        }
    }
}
