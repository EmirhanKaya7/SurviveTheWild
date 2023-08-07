using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HydrationBar : MonoBehaviour
{
    Slider slider;
    public TextMeshProUGUI waterCounter;

    public GameObject playerState;

    private float currentWater, maxWater;


    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentWater = playerState.GetComponent<PlayerState>().currentHydration;
        maxWater = playerState.GetComponent<PlayerState>().maxHealth;

        float fillvalue = currentWater / maxWater;
        slider.value = fillvalue;

        waterCounter.text = currentWater + "%";
    }
}
