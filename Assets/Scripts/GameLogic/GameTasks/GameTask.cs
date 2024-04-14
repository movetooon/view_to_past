using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GameTask:MonoBehaviour   
{
    public string taskName;
    public abstract void Init(Book book);
    public abstract bool CheckDone();
    public abstract void Complete();
     
}
 