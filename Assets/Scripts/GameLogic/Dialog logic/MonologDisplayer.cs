using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MonologDisplayer : MonoBehaviour
{
    [SerializeField] protected TMP_Text playerTextContainter;
    [SerializeField] private Animator playerDialogPanel;

    Action onMonologEnded;
    Action onMonologStarted;

    private void Start()
    {
        onMonologStarted += FindObjectOfType<Player>().EnterIn<InactionState>;
        onMonologEnded += FindObjectOfType<Player>().EnterIn<IdleState>;
    }

    public virtual void StartShowingMonolog(Monolog monolog)
    {
        StartCoroutine(ShowMonolog(monolog, 0.01f));
        //fix that shit, please add delay
    }

    protected  IEnumerator ShowMonolog(Monolog monolog,float delay, float delayBetweenReplics=0)
    {
        onMonologStarted?.Invoke();
        EneblePlayerPanel();
        

        foreach(string replic in monolog.replics)
        {
            ShowReplic(replic, delay);
            yield return new WaitUntil(() => canMoveToNextReplic(replic));
            yield return new WaitForSeconds(delayBetweenReplics);
        }

        DisablePlayerPanel();
        onMonologEnded?.Invoke();

    }

    public virtual bool canMoveToNextReplic(string replic)
    {
        return playerTextContainter.text == replic && UserInput.GetMouseClick();
    }

    private void EneblePlayerPanel()
    {
        playerDialogPanel.gameObject.SetActive(true);
        playerDialogPanel.SetBool("turnOn", true);
    }
    private void DisablePlayerPanel()
    {
        playerDialogPanel.SetBool("turnOn", false); 
        //while(!(playerDialogPanel.GetCurrentAnimatorStateInfo(layerIndex:0).normalizedTime>=1)) await Task.Delay(10);

        //playerDialogPanel.gameObject.SetActive(false);
        
    }

    private void ShowReplic(string text,float delay)
    {
        StartCoroutine(TMPutilities.SetTextWithDelay(playerTextContainter, text, delay));
    }
}
