using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class DialogDisplayer : MonologDisplayer
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
        Dialog dialog = DialogStorage.getCharachterDialog(npcName, npcDialogNumber);
        StartCoroutine(ShowDialog(dialog)); 

        charachterDialogPanel.SetSmallCloudTransforms(npcPosition, npcHeight);
        playerDialogPanel.gameObject.SetActive(true);


    }

    public IEnumerator ShowDialog(Dialog dialog)
    {  
        foreach(Replic replic in dialog.replics)
        { 
            TMP_Text currentSpeakerContainer=charachterTextContainter;
            if (replic.isPlayer) 
            { 
                currentSpeakerContainer = playerTextContainter;
                playerDialogPanel.SetBool("turnOn",true);
            }
            else
            {  
                charachterDialogPanel.gameObject.SetActive(true);
                yield return new WaitUntil(() => charachterDialogPanel.anim.GetCurrentAnimatorStateInfo(layerIndex:0).length==1);
            }

            SetReplic(currentSpeakerContainer, replic.text);

             
            yield return new WaitUntil(() => currentSpeakerContainer.text==replic.text&&UserInput.GetMouseClick()); 
        }

        onDialogEnded?.Invoke();
        playerDialogPanel.SetBool("turnOn",false);
        charachterDialogPanel.anim.SetTrigger("disapear");
         
        yield return null;
        yield return new WaitUntil(() => charachterDialogPanel.anim.GetCurrentAnimatorStateInfo(layerIndex: 0).normalizedTime>=1);
        charachterDialogPanel.gameObject.SetActive(false);


    }


    public void SetReplic(TMP_Text textContainer,string text)
    { 
        TMP_Text otherTextContainer = textContainer == playerTextContainter? charachterTextContainter : playerTextContainter;

        StartCoroutine(TMPutilities.SetTextWithDelay(textContainer, text, 0.01f));
        StartCoroutine(TMPutilities.SetTextWithDelay(otherTextContainer, ". . .", 0.0001f, pauses: false));
       

    }

     
}
