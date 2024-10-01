using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
//C 2024 Daniel Snapir alias Baltazar Benoni

public class IntroText : MonoBehaviour
{
    private Text textBoxContent; 
    [SerializeField] private TextAsset introLoreAsset;
    private string IntroLoreText;
    private string[] introLines;
    private string selectedLine;
    
       void Start()
    {
        textBoxContent = GetComponent<Text>();
        IntroLoreText = introLoreAsset.ToString();
        introLines = IntroLoreText.Split("\n");
        StartCoroutine(UpdateTextBoxContent());
    }

    void Update()
    {
        
    }

    void UpdateTextSelection()
    {
    }

    IEnumerator UpdateTextBoxContent()
    {
        yield return new WaitForSeconds(7.5f);
       for (int i = 0; i < introLines.Length -1; i++)
        {
            selectedLine = introLines[i];
            for(int j = 0; j < selectedLine.Length; j++)
            {
                if(selectedLine[0] == ' ')
                {
                    yield return new WaitForSeconds(0.71f);
                }
                textBoxContent.text += selectedLine[j].ToString();
                if(selectedLine[j] == ',' || selectedLine[j] == ':'
                    || (selectedLine[j] == '.' && selectedLine[j+1] != '.'))
                {
                    yield return new WaitForSeconds(0.75f);
                }
                else if(i > 31)
                {
                    yield return new WaitForSeconds(0.045f);
                }
                else
                {
                    yield return new WaitForSeconds(0.06f);
                }
            }
            if(i < 6)
            {
                yield return new WaitForSeconds(0.85f);
            }
            else if(i > 25 && i <= 28)
            {
                yield return new WaitForSeconds(0.67f);
            }
            else if(i > 28)
            {
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                yield return new WaitForSeconds(0.77f);
            }
            textBoxContent.text = "";
        }
    }
}
