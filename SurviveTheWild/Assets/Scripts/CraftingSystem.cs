using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;
    public GameObject craftingScreenUI;
    public GameObject toolScreenUI;

    public List<string> inventoryItemList = new List<string>();

    Button toolsBtn;

    Button craftBtn;

    TextMeshProUGUI req1, req2;

    bool isOpen;


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
        isOpen = false;
        toolsBtn  = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory(); });

        req1 = toolScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>(); 
        req2 = toolScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();

        craftBtn = toolScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftBtn.onClick.AddListener(delegate { CraftAnyItem(); });
    }

    private void CraftAnyItem()
    {
        
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            FirstPersonController.Instance.cameraCanMove = false;
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            FirstPersonController.Instance.cameraCanMove = true;
            craftingScreenUI.SetActive(false);
            toolScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            isOpen = false;
        }
    }
}
