using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Newspaper : MonoBehaviour
{
    public TMP_Text[] blocksText;
    public RectTransform[] blocks;
    public int[] order;


    private void Start()
    { 
        
        for(int i=0;i<blocks.Length;i++)
        {
             // blocks[i].SetSiblingIndex(order[i]);
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
        }
        else
        {
            string cahce = blocksText[0].text;
            blocksText[0].text = blocksText[index].text;
            blocksText[index].text = cahce;
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
        }
        else
        {
            string cahce = blocksText[blocksText.Length-1].text;
            blocksText[blocksText.Length - 1].text = blocksText[index].text;
            blocksText[index].text = cahce;
        }
    }


}
