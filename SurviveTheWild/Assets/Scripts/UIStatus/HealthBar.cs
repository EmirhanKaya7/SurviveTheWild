using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    Slider slider;
    public TextMeshProUGUI healthCounter;

    public GameObject playerState;

    private float currentHealth, maxHealth;


    // Start is called before the first frame update
    void Awake()
    {
     slider = GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerState.GetComponent<PlayerState>().currentHealth;
        maxHealth = playerState.GetComponent<PlayerState>().maxHealth;

        float fillvalue = currentHealth / maxHealth;
        slider.value = fillvalue;

        healthCounter.text = currentHealth + "/" + maxHealth; 
    }
}
