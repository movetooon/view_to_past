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
            /*
         textContainer.ForceMeshUpdate();
         Mesh cahcedMesh = textContainer.mesh;
         Vector3[] vertices= cahcedMesh.vertices;
         Color[] colors= cahcedMesh.colors;
         float t = 0;
         while (t < 1f)
         {
             for (int j = cahcedMesh.vertices.Length-4; j < cahcedMesh.vertices.Length; j++)
             {

                 colors[j] = Color.Lerp(Color.clear,Color.black,t);
                 vertices[j] = Vector3.Lerp(cahcedMesh.vertices[j] + Vector3.one * 10 * Mathf.Sin(j), cahcedMesh.vertices[j],t);
                 cahcedMesh.vertices = vertices;
                 cahcedMesh.colors = colors;
                 textContainer.canvasRenderer.SetMesh(cahcedMesh);


             }
             t += 0.2f;
             yield return null;
         }


        */




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
