using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMachine : MonoBehaviour
{
    public levelStage currentLevelStage;

}

public struct levelStage
{
    public string levelName;
    public string stageName;
}