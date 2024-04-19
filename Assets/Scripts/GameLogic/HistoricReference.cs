using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HistoricReference : MonoBehaviour
{
    [SerializeField] private Location[] locsToObserve;
    [SerializeField, TextArea(5, 20)] private string[] information;
    [SerializeField] float delay;
    [SerializeField] float startDelay;

    [SerializeField] Player player;
    [SerializeField] private TMP_Text textContainer;
    [SerializeField] private Animator textAnimator;

     

    private void Start()
    {
        player.Init();
        player.EnterIn<IdleState>();
       // StartCoroutine(ShowReference()); 

        foreach(Location loc in locsToObserve)
        {
            loc.Init(player);
        }

        StartCoroutine(ShowReference());
    }


    
    IEnumerator ShowReference()
    { 
        yield return new WaitForSeconds(startDelay);

        for(int i = 0; i < locsToObserve.Length; i++)
        {
            Debug.Log(i);
            textAnimator.SetTrigger("change");
            locsToObserve[i].Select();
            yield return new WaitForSeconds(1f);
            textContainer.text= information[i];
            yield return new WaitForSeconds(2f);



            yield return new WaitUntil(() => Input.GetMouseButton(0));
            

        }

        
    }
}
