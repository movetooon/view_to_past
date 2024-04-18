using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TMPutilities 
{
    public static IEnumerator SetTextWithDelay(TMP_Text textContainer, string text, float delay, bool pauses = true)
    {
        textContainer.text = "";


        textContainer.ForceMeshUpdate();

        Mesh cachedMesh = textContainer.mesh;
        Vector3[] newVertices = cachedMesh.vertices;
        Color[] newColors = cachedMesh.colors;

        for (int i = 0; i < text.Length; i++)
        {
            TMP_CharacterInfo charInfo = textContainer.textInfo.characterInfo[i];
            charInfo.color = Color.clear;
        }
        cachedMesh.vertices = newVertices;
        textContainer.canvasRenderer.SetMesh(cachedMesh);

        for (int i = 0; i < text.Length; i++)
        {

            TMP_CharacterInfo charInfo = textContainer.textInfo.characterInfo[i];

            float timer = 0.2f;
            while (timer > 0)
            {
                charInfo.color = Color.Lerp(Color.clear, Color.white, (1 - timer * 5f));
                timer -= Time.fixedDeltaTime;
                yield return null;
            }
            /*

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
            */
            cachedMesh.vertices = newVertices;
        textContainer.canvasRenderer.SetMesh(cachedMesh);
             

        }

    }
}
