using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    public bool isOpen;

    public Blueprint Axeblp = new Blueprint("Axe", 2 , "Stone", 3, "Wood" ,3);

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
        req2 = toolScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftBtn = toolScreenUI.transform.Find("Axe").transform.Find("Craft").GetComponent<Button>();

        craftBtn.onClick.AddListener(delegate { CraftAnyItem(Axeblp); });
    }

    private void CraftAnyItem(Blueprint blueprint)
    {
        
        InventorySystem.Instance.AddToInventory(blueprint.itemName);

        if (blueprint.numOfReq == 1)
        {
        InventorySystem.Instance.RemoveItem(blueprint.Req1,blueprint.Req1amount);
        }
        else if (blueprint.numOfReq == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprint.Req1, blueprint.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprint.Req2, blueprint.Req2amount);
        }




        StartCoroutine(calculate());

    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItems();
    }

    public void RefreshNeededItems()
    {
        int stone_count =0;
        int stick_count =0;

        inventoryItemList = InventorySystem.Instance.itemlist;

        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stone_count ++;
                    break;

                case "Wood":
                    stick_count++;
                    break;
                
            }
        }


        req1.text = "3 Stone[" + stone_count + "]";
        req2.text = "3 Wood[" + stick_count + "]";

        if (stone_count >= 3 && stick_count >= 3)
        {
            craftBtn.gameObject.SetActive(true);
        }
        else
        {
            craftBtn.gameObject.SetActive(false);
        }
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolScreenUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       // RefreshNeededItems();


        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
            FirstPersonController.Instance.cameraCanMove = false;
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolScreenUI.SetActive(false);
            if (!InventorySystem.Instance.isOpen)
            {
            FirstPersonController.Instance.cameraCanMove = true;

                Cursor.lockState = CursorLockMode.Locked;

            }

            isOpen = false;
        }
    }
}
