using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        DialogStorage.setDialogsForCurrentLevel("BeforeRevolution");
    }
}
