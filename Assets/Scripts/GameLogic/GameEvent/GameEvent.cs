using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    public abstract void Invoke();
    public abstract void Init();
}
