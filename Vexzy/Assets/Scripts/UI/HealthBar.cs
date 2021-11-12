using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Transform cam;
    //[SerializeField] private Canvas interactingCanvasUi;
    public static HealthBar Instance;
    void Awake()
    {
        Instance = this;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactingCanvasUi.transform.LookAt(Camera.main.transform);
        }
    }*/
}
