using System.Collections;
using System.Collections.Generic;
using TMPro; 
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

public class Newspaper : MonoBehaviour
{
    [SerializeField] private TMP_Text[] blocksText;
    [TextArea(2,3),SerializeField] private string[] information;
    [SerializeField] private RectTransform[] blocks;
    [SerializeField] private int[] order;

    [SerializeField] private Animator anim;

    [SerializeField] private UnityEvent onEndEvent;
    [SerializeField] private UnityEvent onStartEvent;



   

    public async void StartMechanic()
    {
        for (int i = 0; i < blocks.Length; i++)
        {
            blocksText[i].text = information[(order[i] - 1)];
        }
        await Task.Delay(1000);
        onStartEvent.Invoke();
    }

    public async void EndMechanic()
    {
        anim.SetTrigger("disapear");
        await Task.Delay(1000);
        onEndEvent.Invoke();
        gameObject.SetActive(false);
    }

    public bool CheckCorrectness()
    {
        for(int i = 1; i < order.Length; i++)
        {
            Debug.Log(order[i]);
            if ((order[i] - order[i - 1]) != 1) return false;
            
        }
        return true;
    }
     
    public void TryToPass()
    {
        if (CheckCorrectness() == true)
        {
            EndMechanic();
        }
    }

    public void ChangeSiblingDown(Transform sibling)
    { 
        int index=sibling.GetSiblingIndex();

        if (index != (blocksText.Length - 1))
        {
            string cahce = blocksText[index + 1].text;
            blocksText[index + 1].text = blocksText[index].text;
            blocksText[index].text = cahce;

            int cahceOrder = order[index + 1];
            order[index+1]=order[index];
            order[index] = cahceOrder;
     
        }
        else
        {
            string cahce = blocksText[0].text;
            blocksText[0].text = blocksText[index].text;
            blocksText[index].text = cahce;

            int cahceOrder = order[0];
            order[0] = order[index];
            order[index] = cahceOrder;
           
        }
    }

    public void ChangeSiblingUp(Transform sibling)
    { 
        int index = sibling.GetSiblingIndex();

        if (index != 0)
        {
            string cahce = blocksText[index - 1].text;
            blocksText[index - 1].text = blocksText[index].text;
            blocksText[index].text = cahce;

            int cahceOrder = order[index - 1];
            order[index - 1] = order[index];
            order[index] = cahceOrder; 
        }
        else
        {
            string cahce = blocksText[blocksText.Length-1].text;
            blocksText[blocksText.Length - 1].text = blocksText[index].text;
            blocksText[index].text = cahce;

            int cahceOrder = order[blocksText.Length - 1];
            order[blocksText.Length - 1] = order[index];
            order[index] = cahceOrder; 
        }
    }


}
