using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogDisplayer : MonologDisplayer
{ 
    [SerializeField] private TMP_Text charachterTextContainter;  
    [SerializeField] private DialogCloud characterPanel;
     
    Action  onDialogEnded;
    Action  onDialogStarted;

    private void Start()
    {  
        onDialogStarted += FindObjectOfType<ArrowsManager>().DisableAllArrows;

        onDialogEnded += FindObjectOfType<Player>().EnterIn<IdleState>;
        onDialogEnded += FindObjectOfType<ArrowsManager>().ReUpdateArrows;
    }

    public void StartDialog(Dialog dialog)
    {  
        StartCoroutine(ShowDialog(dialog));   
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        onDialogStarted?.Invoke();
        EneblePlayerPanel();

        foreach (Replic replic in dialog.replics)
        { 
            TMP_Text currentSpeakerContainer=charachterTextContainter;

            if (replic.isPlayer) 
            { 
                currentSpeakerContainer = playerTextContainter; 
            }
            else
            {
                EnebleCharacterPanel(); 
            }

            ShowReplic(currentSpeakerContainer, replic.text); 
            yield return new WaitUntil(() => CanMoveToNextReplic(replic.text,currentSpeakerContainer));
        }
         
        StartCoroutine(DisableCharacterPanel()); 
        StartCoroutine(DisablePlayerPanel());
        onDialogEnded?.Invoke();

    }

    private bool CanMoveToNextReplic(string replic,TMP_Text currentTextContainer)
    {
        return currentTextContainer.text == replic && UserInput.GetMouseClick();
    }

    private void EnebleCharacterPanel()
    {
        characterPanel.gameObject.SetActive(true);
        characterPanel.anim.SetTrigger("start");
    }

    private IEnumerator DisableCharacterPanel()
    {
        characterPanel.anim.SetTrigger("disapear");
        yield return null;
        yield return new WaitUntil(() => characterPanel.anim.GetCurrentAnimatorStateInfo(layerIndex: 0).length==1);
        characterPanel.gameObject.SetActive(false);
    }


    public void ShowReplic(TMP_Text textContainer,string text)
    { 
        TMP_Text otherTextContainer = textContainer == playerTextContainter? charachterTextContainter : playerTextContainter;

        StartCoroutine(TMPutilities.SetTextWithDelay(textContainer, text, 0.01f));
        StartCoroutine(TMPutilities.SetTextWithDelay(otherTextContainer, ". . .", 0.0001f, pauses: false));
       

    }

     
}
