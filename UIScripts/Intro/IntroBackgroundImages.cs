using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class IntroBackgroundImages : MonoBehaviour
{
    [SerializeField] private Sprite image1;
    [SerializeField] private Sprite image2;
    [SerializeField] private Sprite image3;
    [SerializeField] private Image backGroundImage;
    [SerializeField] private Graphic blackBackground;
    private float changeImageStarter;
    private bool coroutineStarted;
    int counter;

    void Start()
    {
        backGroundImage.sprite = image1;
        coroutineStarted = false;
        counter = 1;
    }

    void Update()
    {
        changeImageStarter += Time.deltaTime;

        if(changeImageStarter > 29.5f)
        {
            if(!coroutineStarted)
            {
                coroutineStarted = true;
                StartCoroutine(IntroImageManager());
            }
        }
    }
    IEnumerator IntroImageManager()
    {
        while(blackBackground.color.a < 1.5)
        {
            FadeOut();
            yield return null;
        }
        backGroundImage.sprite = ImageSelector(counter);
        yield return new WaitForSeconds(0.7f);
        while(blackBackground.color.a > 0)
        {
            FadeIn();
            yield return null;
        }
        yield return new WaitForSeconds(51);
        counter++;
        coroutineStarted = false;
        if(counter > 2) { coroutineStarted = true; }
    } 

    private Sprite ImageSelector(int counter)
    {
        if(counter == 1) {return image2;}
        else {return image3;}
    }

    void FadeIn()
    {
        blackBackground.color -= new Color(0, 0, 0, 0.01f);
    }
    void FadeOut()
    {
        blackBackground.color += new Color(0, 0, 0, 0.005f);
        Debug.Log(blackBackground.color.a);
    }
}
