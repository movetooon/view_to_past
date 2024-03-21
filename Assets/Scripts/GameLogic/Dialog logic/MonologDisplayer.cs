using System;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class MonologDisplayer : MonoBehaviour
{
    [SerializeField] protected TMP_Text playerTextContainter;
    [SerializeField] protected Animator playerPanel;

    Action onMonologEnded;
    Action onMonologStarted;

    private void Start()
    {
        onMonologStarted += FindObjectOfType<Player>().EnterIn<InactionState>;
        onMonologStarted += FindObjectOfType<ArrowsManager>().DisableAllArrows;
        onMonologEnded += FindObjectOfType<Player>().EnterIn<IdleState>;
        onMonologEnded += FindObjectOfType<ArrowsManager>().ReUpdateArrows;
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
        yield return null;
        yield return new WaitUntil(() => playerPanel.GetCurrentAnimatorStateInfo(layerIndex: 0).normalizedTime>=1);

        foreach (string replic in monolog.replics)
        {
            ShowReplic(replic, delay);
            yield return new WaitUntil(() => CanMoveToNextReplic(replic));
            yield return new WaitForSeconds(delayBetweenReplics);
        }

        StartCoroutine(DisablePlayerPanel());
        onMonologEnded?.Invoke();

    }

    public virtual bool CanMoveToNextReplic(string replic)
    {
        return playerTextContainter.text == replic && UserInput.GetMouseClick();
    }

    protected void EneblePlayerPanel()
    {
        playerPanel.gameObject.SetActive(true);
        playerTextContainter.text = ""; 
        playerPanel.SetBool("turnOn", true);
    }
    protected IEnumerator DisablePlayerPanel()
    {
        
        playerPanel.SetBool("turnOn", false); 
        yield return new WaitUntil(() => playerPanel.GetCurrentAnimatorStateInfo(layerIndex: 0).normalizedTime<=1);
        playerPanel.gameObject.SetActive(false);

    }

    protected void ShowReplic(string text,float delay)
    {
        StartCoroutine(TMPutilities.SetTextWithDelay(playerTextContainter, text, delay));
    }
}
