using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class ITalkable  
{
    private TMP_Text textContainer;

    public abstract void SetText(string text, float delay);
     
     
    
}
