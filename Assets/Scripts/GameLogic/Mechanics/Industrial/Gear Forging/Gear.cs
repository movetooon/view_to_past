using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;

public class Gear : MonoBehaviour
{
    [SerializeField] SpriteShapeController firstPeace;
    [SerializeField] SpriteShapeController finalPeace;
    float[] dotForgedCache;

    [SerializeField] private ParticleSystem knockEffect;
    SpriteShapeController self;
    [SerializeField] UnityEvent onForgingEnded;

     
     
    public void Init()
    {
        self = GetComponent<SpriteShapeController>();
        dotForgedCache = new float[self.spline.GetPointCount()];
    }
     
    public void CompleteForging()
    {
        onForgingEnded.Invoke();
    }

    public bool CheckCompleteForging()
    { 
        for (int i = 0; i < self.spline.GetPointCount(); i++)
        { 
            if (dotForgedCache[i] < 0.85f) return false;
        }
        return true;
    }

    public void Knock(Vector3 mousePosition, float intensity,float radius)
    {
        knockEffect.transform.position= mousePosition;
        knockEffect.Play();

        for (int i = 0; i < self.spline.GetPointCount(); i++)
        { 
            Vector3 dotPos= (transform.localRotation * self.spline.GetPosition(i) * transform.localScale.x*1.5f + transform.position); 
            bool inCircle = Vector3.Distance(dotPos, mousePosition) < radius;
            
              
            if (inCircle)
            { 
                dotForgedCache[i] += intensity;
                self.spline.SetPosition(i,
                Vector3.Lerp(
                    firstPeace.spline.GetPosition(i),
                    finalPeace.spline.GetPosition(i),
                    dotForgedCache[i])
                );

            }
             
            
        }
    }

    
}
