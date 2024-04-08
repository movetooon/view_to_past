using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{ 

    public void EnterScene(int newSceneIndex)
    {
        SceneManager.LoadScene(newSceneIndex);
    }
}
