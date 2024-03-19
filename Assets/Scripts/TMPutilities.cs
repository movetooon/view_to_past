using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TMPutilities 
{
    public static IEnumerator SetTextWithDelay(TMP_Text textContainer, string text, float delay, bool pauses = true)
    {
        textContainer.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '<')
            {
                while (text[i] != '>')
                {
                    textContainer.text += text[i];
                    i++;
                }
            }


            textContainer.text += text[i];

            if (pauses)
                switch (text[i])
                {
                    case '.':
                        yield return new WaitForSeconds(0.75f);
                        break;
                    case ',':
                        yield return new WaitForSeconds(0.15f);
                        break;

                    default:
                        yield return new WaitForSeconds(delay);

                        break;

                }
            else yield return new WaitForSeconds(delay);
             
        }  
    }
}
