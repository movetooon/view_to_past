using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snailRace : MonoBehaviour
{
    [SerializeField] GameTask winRaceTask;
    [SerializeField] Location back;
    [SerializeField] private Transform[] wayEnemy;
    [SerializeField] private Transform[] wayPlayer;
    [SerializeField] private Transform enemySnail;
    [SerializeField] private Transform playerSnail;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float playerSpeed;
     
    private bool win;
    
    bool started;
    float tP = 0;
    float tE = 0;

    private void Start()
    { 
        //StartRace();
    }

    public void StartRace()
    {
        StartCoroutine(Race()); 
    }

    private void Update()
    {
        

        if (started == true &&UserInput.GetMouseClick())
        {
            tP += playerSpeed;
            playerSnail.transform.position = Vector3.Lerp(wayPlayer[0].position, wayPlayer[1].position, tP);
             
        }


        if (tP > 0.9f || tE > 0.9f)
        {
            back.Select();
        }

    }

    public bool checkWin()
    {
        if (tP>0.9f&& tP > tE)
        {
            win = true;
            return win;

        }
        else
        {
            return false; 
        }
    }

    public IEnumerator Race()
    {
        tP = 0; 
        tE = 0;
        started = true;

        while (enemySnail.transform.position != wayEnemy[1].position)
        { 
            yield return new WaitForSeconds(0.3f);
            enemySnail.transform.position = Vector3.Lerp(wayEnemy[0].position, wayEnemy[1].position, tE);
            tE += 0.02f;
        }

         
    }

   private void Win()
   {
        winRaceTask.Complete();
   }

}
