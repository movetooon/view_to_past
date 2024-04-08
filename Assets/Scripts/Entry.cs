using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entry : MonoBehaviour
{

    public UnityEvent enterscene;

    void Start()
    {
        enterscene.Invoke();
    }

     
}
