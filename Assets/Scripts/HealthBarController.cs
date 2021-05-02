using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{

    [Header("Object References")]
    [Tooltip("Slider of the health value")]
    [SerializeField] Slider slider;

    public void SetMaxHealth (int health)
    {
        slider.maxValue = health;
        //slider.value = health;

        slider.gameObject.SetActive(false);
    }

    public void SetHealth (int health)
    {
        slider.value = health;

        if (slider.value < slider.maxValue)
        {
            slider.gameObject.SetActive(true);
        }

    }

}
