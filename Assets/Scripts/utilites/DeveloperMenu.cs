using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperMenu : MonoBehaviour
{
    [SerializeField] Button before,after, ww1;
    [SerializeField] Slider timeSlider;

    [SerializeField] RectTransform panel;
   

    private void Start()
    {
        DontDestroyOnLoad(this);
        before.onClick.AddListener(EnterBefore);
        after.onClick.AddListener(EnterAfter);
        ww1.onClick.AddListener(EnterWW1);
    }

    private void FixedUpdate()
    {
        bool canEnable= Input.GetKey(KeyCode.LeftShift)&& Input.GetKey(KeyCode.X)&& Input.GetKey(KeyCode.Z)&& Input.GetKey(KeyCode.V);
        Time.timeScale= timeSlider.value;

        if (canEnable)
        {
            panel.gameObject.SetActive(true);
        }
    }

    public void EnterBefore()
    {
        GameSceneManager.instance.EnterScene(2);
    }

    public void EnterAfter()
    {
        GameSceneManager.instance.EnterScene(3);
    }

    public void EnterWW1()
    {
        GameSceneManager.instance.EnterScene(5);
    }

    void Update()
    {
        
    }
}
