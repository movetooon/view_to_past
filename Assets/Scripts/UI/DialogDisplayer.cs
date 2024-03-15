using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DialogDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text playerTextContainter;
    [SerializeField] private TMP_Text charachterTextContainter;


    public void StartDialog(Dialog dialog)
    {
        StartCoroutine(ShowDialog(dialog));
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        foreach(Replic replic in dialog.replics)
        {
            if (replic.isPlayer)
            {
                SetTextWithDelay(playerTextContainter, replic.text, 0.01f);
                yield return new WaitUntil(() => UserInput.GetMouseClick());
            }
            else
            {
                SetTextWithDelay(charachterTextContainter, replic.text, 0.01f);
                yield return new WaitUntil(() => UserInput.GetMouseClick());
            }
        }
    }

    async void SetTextWithDelay(TMP_Text textContainer, string text, float delay )
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
            if (text[i]!=' ')
            await Task.Delay((int)(delay * 1000f));
        }

        
    }
}
