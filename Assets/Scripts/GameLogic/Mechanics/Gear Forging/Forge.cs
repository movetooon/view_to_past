using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class Forge : MonoBehaviour
{
    private Gear currentGear;
    private int currentGearIndex;
    [SerializeField] private List<Gear> gearsToForge;
 
    [SerializeField] private float radius;
 
   
    [SerializeField] private float maxIntensity;
    [SerializeField] private float intensity;
    [SerializeField] float speedColding;
    [SerializeField] Material gearMaterial;
    [SerializeField] Material coalMaterial;
    [SerializeField] float bloomIntensity;

    [SerializeField] Transform gearAnvilPos;   
    [SerializeField] Transform gearMechPos;   
    

    [SerializeField] private Animator mech;
    [SerializeField] private UnityEvent onEnterforgeLoc;
    [SerializeField] private UnityEvent onEnded;


    private void Start()
    {
        Init();
    }

    void Init()
    {
        foreach (Gear gear in gearsToForge)
        {
            //gear.gameObject.SetActive(true);
            gear.Init();
            
            //gear.gameObject.SetActive(false);
        }
    }

    public void StartForging()
    {
        currentGear = gearsToForge[currentGearIndex];
        currentGear.gameObject.SetActive(true);
        if (intensity>0)
        {
            StartCoroutine(Forging());
        }
    }

    public void StartHeating()
    {
        Debug.Log("heating");
        StartCoroutine(Heating());
        
    }

    void UpdateColors()
    {
        float coal = intensity * bloomIntensity * 24f;
        coalMaterial.SetColor("_EmissionColor", new Vector4(coal, coal, coal, coal));
        float gear = coal/3f;
        gearMaterial.SetColor("_EmissionColor", new Vector4(gear, gear, gear, gear));
    }

    public IEnumerator Heating()
    {
        currentGear = gearsToForge[currentGearIndex];
        currentGear.gameObject.SetActive(true);
        currentGear.transform.position=gearMechPos.position;
        currentGear.transform.rotation=gearMechPos.rotation;
        while (intensity < maxIntensity)
        {
            if (Input.GetMouseButtonDown(0))
            {
                intensity += 0.05f;
                mech.SetTrigger("blow"); 
                yield return new WaitForSeconds(0.5f);
            }

            UpdateColors();

            intensity -= speedColding/3f;
            yield return null;
        }
        onEnterforgeLoc.Invoke();
    }

    public IEnumerator Forging()
    {

        currentGear.transform.position = gearAnvilPos.position;
        currentGear.transform.rotation = gearAnvilPos.rotation;

        while (intensity > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentGear.Knock(UserInput.GetMouseHitOnScreen().point, intensity, radius);
            }

            intensity -= speedColding; 
            UpdateColors();

            if (currentGear.CheckCompleteForging())
            {
                 
                currentGear.CompleteForging();

                if (currentGearIndex == (gearsToForge.Count-1))
                {
                    onEnded.Invoke();
                }
                currentGearIndex++;
                break;
            }

            yield return null;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(UserInput.GetMouseHitOnScreen().point, radius);
    }

}
