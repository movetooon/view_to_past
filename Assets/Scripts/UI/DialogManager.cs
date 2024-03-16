using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private TMP_Text playerTextContainter;
    [SerializeField] private TMP_Text charachterTextContainter; 
    
    [SerializeField] private Animator playerDialogPanel;
    [SerializeField] private DialogCloud charachterDialogPanel;

    Action  onDialogEnded;

    private void Start()
    {
        onDialogEnded += FindObjectOfType<Player>().EnterIn<IdleState>;
        onDialogEnded += FindObjectOfType<ArrowsManager>().ReUpdateArrows;
    }

    public void StartDialog(string npcName,int npcDialogNumber,Vector3 npcPosition, float npcHeight)
    {
        playerDialogPanel.gameObject.SetActive(true);
        charachterDialogPanel.gameObject.SetActive(true);
        charachterDialogPanel.SetSmallCloudTransforms(npcPosition, npcHeight);
        StartCoroutine(ShowDialog(npcName, npcDialogNumber)); 

    }

    public IEnumerator ShowDialog(string NPCname,int NPCdialogNumber)
    {
        Dialog dialog = DialogStorage.getCharachterDialog(NPCname, NPCdialogNumber);

        foreach(Replic replic in dialog.replics)
        { 
            TMP_Text currentSpeakerContainer=charachterTextContainter; 
            if (replic.isPlayer) currentSpeakerContainer = playerTextContainter; 
            SetReplic(currentSpeakerContainer, replic.text);

            yield return new WaitUntil(() => currentSpeakerContainer.text == replic.text && UserInput.GetMouseClick());
        }

        onDialogEnded?.Invoke();
        playerDialogPanel.SetBool("turnOn",false);
    }
    

    public void SetReplic(TMP_Text textContainer,string text)
    {
        TMP_Text otherTextContainer = textContainer == playerTextContainter? charachterTextContainter : playerTextContainter;
         
        SetTextWithDelay(textContainer, text, 0.01f);
        SetTextWithDelay(otherTextContainer, ". . .", 0.01f);
       

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
            
            switch (text[i])
            {
                case '.':
                    await Task.Delay((int)((0.5f) * 1000f)); 
                    break;
                case ',':
                    await Task.Delay((int)((0.15f) * 1000f));
                    break;
                
                default:
                    await Task.Delay((int)((delay) * 1000f));

                    break;

            }

            
        }

        
    }
}
