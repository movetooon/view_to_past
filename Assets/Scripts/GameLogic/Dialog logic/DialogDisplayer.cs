using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine; 

public class DialogDisplayer : MonologDisplayer
{ 
    [SerializeField] private TMP_Text charachterTextContainter;  
    [SerializeField] private DialogCloud characterPanel;

    [SerializeField] private AudioSource soundPlayer;
    

    ITalkable currentSpeaker = null;

    Action  onDialogEnded;
    Action  onDialogStarted;
    Action<List<string>> onTaskAddRequested;
   

     

    public void Init(ArrowsManager arrowsManager, Book book,Player player)
    {
        onDialogStarted += arrowsManager.DisableAllArrows;
        onDialogStarted += book.DisableDiaryButton;
        onTaskAddRequested += book.AddTask;

        onDialogEnded += player.EnterIn<IdleState>;
        onDialogEnded += arrowsManager.ReUpdateArrows;
        onDialogEnded += book.EnableDiaryButton;
    }

    public void StartDialog(Dialog dialog,EventHandler eventHandler, ITalkable speaker,NPCSound npcSound)
    {  
        StartCoroutine(ShowDialog(dialog, eventHandler,speaker, npcSound));   
    }

    public IEnumerator ShowDialog(Dialog dialog,EventHandler eventHandler, ITalkable speaker,NPCSound npcSound)
    {
        currentSpeaker = speaker;
        onDialogStarted?.Invoke();

        try
        { 
            onTaskAddRequested?.Invoke(dialog.taskNames);
        }
        catch (Exception e) { Debug.Log(e); }
        
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
                if (replic.emotion != null)
                {
                    soundPlayer.clip = npcSound.GetSoundByName(replic.emotion);
                    soundPlayer.Play();
                }
                else
                {
                    soundPlayer.clip = npcSound.default_sounds[UnityEngine.Random.Range(0, npcSound.default_sounds.Length)];
                    soundPlayer.Play();
                }
                EnebleCharacterPanel();
                characterPanel.SetEmotion(dialog.emotion);
            }
             

            
            

            ShowReplic(currentSpeakerContainer, replic.text);

            bool invoked = false;
            
            if (replic.eventsCount > 0 && replic.onEnd==false)
            { 
                eventHandler?.InvokeEvents(replic.eventsCount);
                invoked = true;

            }

            yield return new WaitUntil(() => CanMoveToNextReplic(replic.text,currentSpeakerContainer));

            if (invoked==false)
            {
                eventHandler?.InvokeEvents(replic.eventsCount);
            }
        }


        EndDialog();

    }

     

    public void EndDialog()
    {
        StartCoroutine(DisableCharacterPanel());
        StartCoroutine(DisablePlayerPanel());
        currentSpeaker.EndTalking();

        currentSpeaker = null;
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
