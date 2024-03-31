using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class GameTask:MonoBehaviour   
{
    public abstract void Init();
    public abstract bool CheckDone();
    public abstract void Complete();
     
}
 