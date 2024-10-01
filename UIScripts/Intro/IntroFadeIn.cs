using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class IntroFadeIn : MonoBehaviour
{
    //The blackness which blocks the view of the scene.
    [SerializeField] private Graphic blackBackground;
    //A boolean to indicate if fading out is taking place.
    private SkipIntro skippingScript;
    //The coroutine to fade out of the scene.
    private Coroutine fadeOutRoutine;

    void Start()
    {
        skippingScript = GetComponent<SkipIntro>();
        StartCoroutine(FadeIn());
    }

//Make the black background slowly more transparent. If intro has is skipped
//(this is verified from the 'skippingScript'), break out of the coroutine.
    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2.5f);

        while (blackBackground.color.a > 0)
        {
            blackBackground.color -= new Color(0f, 0f, 0f, 0.01f);
            yield return null; 
            yield return null; 
            if(skippingScript.skippingIntro) { yield break; }
        }
    }
}