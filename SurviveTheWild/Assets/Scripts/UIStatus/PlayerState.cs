using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance;

    public float currentHealth;
    public float maxHealth;

    public float currentCalories;
    public float maxCalories;

    float distanceTravelled = 0;
    Vector3 lastPos;

    public GameObject player;



    public float currentHydration;
    public float maxHydration;

    public bool isHydrationActive;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        currentHealth = maxHealth;    
        currentCalories = maxCalories;
        currentHydration = maxHydration;
        StartCoroutine(DecreaseHydration());
    }

    private IEnumerator DecreaseHydration()
    {
        while (true)
        {
            currentHydration--;
            yield return new WaitForSeconds(15);
        }

    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += Vector3.Distance(player.transform.position, lastPos);
        lastPos = player.transform.position;
        if (distanceTravelled >= 5)
        {
            distanceTravelled = 0;
            currentCalories--; 
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentHealth-=10;
        }
    }
}
