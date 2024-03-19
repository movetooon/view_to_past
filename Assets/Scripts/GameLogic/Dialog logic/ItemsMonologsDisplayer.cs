using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsMonologsDisplayer : MonologDisplayer
{ 
    public void ShowItemInfo(ItemData item)
    {
        StartShowingMonolog(item.PlayerDescription());
         
    }

    public override void StartShowingMonolog(Monolog monolog)
    {
        StartCoroutine(ShowMonolog(monolog, 0.01f, 1f));
    }

    public override bool canMoveToNextReplic(string replic)
    {
        return playerTextContainter.text == replic;
    }


}
