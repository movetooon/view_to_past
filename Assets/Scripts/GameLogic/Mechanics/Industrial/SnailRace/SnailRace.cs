using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnailRace : MonoBehaviour
{
    [SerializeField] GameTask winRaceTask;
    //[SerializeField] Location back;
    [SerializeField] private UnityEvent onBackLocationReturn;
    [SerializeField] private Transform[] wayEnemy;
    [SerializeField] private Transform[] wayPlayer;
    [SerializeField] private Transform enemySnail;
    [SerializeField] private Transform playerSnail;

    [SerializeField] private float enemySpeed;
    [SerializeField] private float playerSpeed;

    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip looseSound;
    private AudioSource audioPlayer;
     
    private bool win;
    
    bool started;
    float tP = 0;
    float tE = 0;

    private void Start()
    { 
        audioPlayer=GetComponent<AudioSource>();
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
            MoveSnail(tP, wayPlayer, playerSnail);

            //playerSnail.transform.position = Vector3.Lerp(wayPlayer[0].position, wayPlayer[1].position, tP);
             
        }


        if ((tP > ((float)wayPlayer.Length - 1.1f) || tE > ((float)wayEnemy.Length - 1.1f))&&started==true)
        {
            EndRace(checkWin());
        }

    }

    public void EndRace(bool win)
    {
        started = false;
        onBackLocationReturn.Invoke();
        StopCoroutine(Race());
        if (win == true) 
        {
            audioPlayer.clip=winSound;
            audioPlayer.Play();
        }
        else
        {
            audioPlayer.clip = looseSound;
            audioPlayer.Play();
        }
    }

    public bool checkWin()
    {
        if (tP>(wayPlayer.Length-1.1f)&& tP > tE)
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
        playerSnail.transform.position = wayPlayer[0].position;
        enemySnail.transform.position = wayEnemy[0].position;
        started = true;

        while (enemySnail.transform.position != wayEnemy[wayEnemy.Length-1].position)
        {
            if (started == false) break;
            yield return new WaitForSeconds(0.3f);
            MoveSnail(tE, wayEnemy, enemySnail);
            
            //enemySnail.transform.position = Vector3.Lerp(wayEnemy[0].position, wayEnemy[1].position, tE);
            tE += Random.Range(enemySpeed+0.005f,enemySpeed-0.005f);
        }

         
    }

    public void MoveSnail(float t,Transform[] way, Transform snail)
    {
        int count = (int)(t);
        Debug.Log(count + " count " + snail.name);

        snail.transform.rotation = way[count].localRotation; 
        snail.transform.position = Vector3.Lerp(
            way[count].position,
            way[count + 1].position,
            t % 1);
        Debug.Log(t%1 + " lerp " + snail.name);

    }

    private void Win()
   {
        winRaceTask.Complete();
   }

}
