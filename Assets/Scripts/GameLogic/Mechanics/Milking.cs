using System.Collections; 
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Milking : MonoBehaviour
{
    [SerializeField] private Transform[] bucketPositions;
    [SerializeField] private Transform bucket ;
    [SerializeField] private Transform[] splashPositions;
    [SerializeField] private Animator milkSplash;
    [SerializeField] private ParticleSystem splash; 
    [SerializeField] private ParticleSystem splashEnd; 

    [SerializeField] private TMP_Text percentsText;
    private float percents;
     
    [SerializeField] private UnityEvent onEndedEvent;
    AudioSource audioPlayer;

    bool started;
    int bucketPosIndex;

    private void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    public void Init()
    {
        bucket.position = bucketPositions[0].position;
        audioPlayer.Play();
        bucket.gameObject.SetActive(true);
        percents = 0;
        percentsText.text = "0%";
    }

    public void End()
    { 
        bucket.gameObject.SetActive(false);
        audioPlayer.Stop();
    }

    public void StartMilking()
    {
        Init();
        StartCoroutine(MilkingProcess());
    }

    private void Update()
    {
        if (started == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (bucketPosIndex < (bucketPositions.Length-1)) bucketPosIndex++; 
                else bucketPosIndex = 0;

                bucket.position = bucketPositions[bucketPosIndex].position;
            }
        }
    }

    public IEnumerator MilkingProcess()
    {
        started = true;

        while (percents != 100)
        { 
            int random = Random.Range(0, splashPositions.Length);
            SetPositions(random);

            yield return new WaitForSeconds(0.25f); 
            milkSplash.SetBool("milking", true);

            yield return new WaitForSeconds(1f);
            splashEnd.Play();

            yield return new WaitForSeconds(0.5f);
            milkSplash.SetBool("milking", false);
           

            if (bucketPosIndex == random)
            {
                percents += 10;
                percentsText.text = percents + "%";
            }
            yield return new WaitForSeconds(0.5f);

            
        }

        End();
        onEndedEvent?.Invoke();
        started = false;

    }

    private void SetPositions(int random)
    {
        splash.transform.position = splashPositions[random].transform.position;
        splash.transform.rotation = splashPositions[random].transform.localRotation;
        splash.Play();

        milkSplash.transform.position = splashPositions[random].transform.position;
        milkSplash.transform.rotation = splashPositions[random].transform.localRotation;

        splashEnd.transform.position = splashPositions[random].transform.position;
        splashEnd.transform.rotation = splashPositions[random].transform.localRotation;
    }

    
}
